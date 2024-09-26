<template>
  <div class="navbar">
    <div class="brand">
      <NuxtLink to="/" class="nav-link">Logo</NuxtLink>
    </div>
    <div class="nav-links">
      <NuxtLink to="/movies" class="nav-link">Movies</NuxtLink>
      <NuxtLink to="/games" class="nav-link">Games</NuxtLink>
      <NuxtLink to="/music" class="nav-link">Music</NuxtLink>
      <NuxtLink to="/books" class="nav-link">Books</NuxtLink>
    </div>
    <div class="search-bar">
      <SearchBar />
    </div>
  </div>
</template>

<style scoped>
.navbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 15px;
  background-color: #f8f8f8;
  box-shadow: 0px 2px 5px rgba(0, 0, 0, 0.1);
}

.brand {
  font-size: 20px;
  font-weight: bold;
}

.nav-links {
  display: flex;
  gap: 20px;
}

.nav-link {
  font-size: 25px;
  text-decoration: none;
  color: #333;
  font-weight: bold;
  transition: color 0.3s ease;
}

.nav-link:hover {
  color: #007BFF;
}

.search-bar {
  margin-top: 5px;
}
</style>
