<template>
  <div class="seasons-container">
    <div v-for="season in seasons" :key="season.id" class="seasons-item">
      <img :src="getImageUrl(season.poster_path)" alt="season" class="season-image" />
      <div class="season-info">
        <NuxtLink :to="`${$route.path}/season/${season.season_number}`" class="movie-card-link">
          <div class="season-name">{{ season.name }}</div>
        </NuxtLink>
        <div v-if="season.vote_average" class="season-overview">Rating: {{ season.vote_average }}</div>
        <div v-if="season.overview" class="season-overview">{{ season.overview }}</div>
      </div>
    </div>
  </div>
</template>

<script setup>
const props = defineProps({
  seasons: Object,
});

const getImageUrl = (poster_path) => {
    return poster_path
    ? `https://image.tmdb.org/t/p/w200${poster_path}`
    : 'https://via.placeholder.com/200';
}
</script>

<style scoped>
.seasons-item {
  display: flex;
  margin-bottom: 20px;
}

.season-image {
  width: 50px; /* Adjust the image size as needed */
  height: 50px;
  object-fit: cover;
  margin-right: 10px;
}

.season-info {
  display: flex;
  flex-direction: column;
}

.season-name {
  font-weight: bold;
}

.season-overview {
  margin-top: 5px;
}
</style>
