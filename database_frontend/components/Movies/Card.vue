
import type { NuxtLink } from '#build/components';
<template>
  <NuxtLink :to="`/movies/details/${mediaType}/${id}/${title}`" class="movie-card-link">
    <div class="movie-card">
      <div v-if="image" class="movie-image-wrapper">
        <img :src="imageUrl" :alt="title" class="movie-image" />
      </div>
      <div class="movie-details">
        <h2 class="movie-title">{{ title }}</h2>
        <p v-if="overview" class="movie-overview">{{ overview }}</p>
        <div v-if="voteAverage" class="movie-rating">Rating: {{ voteAverage }}</div>
      </div>
    </div>
  </NuxtLink>
</template>

<script setup>
const props = defineProps({
  id: Number,
  mediaType: String,
  title: String,
  image: String,
  overview: String,
  voteAverage: Number
});

const imageUrl = computed(() => {
  return props.image ? `https://image.tmdb.org/t/p/w200${props.image}` : '';
});
</script>

<style>
.movie-card {
  display: flex;
  align-items: flex-start;
  background-color: #fff;
  border: 1px solid #ddd;
  border-radius: 8px;
  overflow: hidden;
  margin-bottom: 20px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.movie-image-wrapper {
  flex-shrink: 0;
  width: 200px;
  height: 300px;
  background-color: #f4f4f4;
  display: flex;
  align-items: center;
  justify-content: center;
}

.movie-image {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.movie-details {
  padding: 20px;
  flex-grow: 1;
}

.movie-title {
  margin: 0;
  margin-bottom: 10px;
  font-size: 1.2em;
  font-weight: bold;
}

.movie-overview {
  margin: 0;
  margin-bottom: 10px;
  font-size: 0.9em;
}

.movie-rating {
  font-weight: bold;
  color: #ff9800;
}
</style>
