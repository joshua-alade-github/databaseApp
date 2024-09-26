<template>
  <div class="crew-container">
    <div v-for="person in crew" :key="person.id" class="crew-item">
      <img :src="getImageUrl(person.profile_path)" alt="Person" class="person-image" />
      <div class="person-info">
        <div class="person-name">{{ person.name }}</div>
        <div class="person-character">{{ person.job }}</div>
      </div>
    </div>
  </div>
</template>

<script setup>
const props = defineProps({
  crew: Object,
});

const getImageUrl = (profilePath) => {
    return profilePath
    ? `https://image.tmdb.org/t/p/w200${profilePath}`
    : 'https://via.placeholder.com/200';
}
</script>

<style scoped>
.crew-item {
  display: flex;
  margin-bottom: 20px;
}

.person-image {
  width: 50px; /* Adjust the image size as needed */
  height: 50px;
  object-fit: cover;
  margin-right: 10px;
}

.person-info {
  display: flex;
  flex-direction: column;
}

.person-name {
  font-weight: bold;
}

.person-character {
  margin-top: 5px;
}
</style>
