<template>
  <div class="recommendations-container">
    <div v-for="media in recommendations.results.slice(0, 5)" :key="media.id" class="media-item">
      <NuxtLink :to="`/movies/details/${media.media_type}/${media.id}/${media.title || media.name}`" class="media-item-link">
        <div class="media-item-content">
          <div v-if="media.poster_path" class="media-image-wrapper">
            <img :src="`https://image.tmdb.org/t/p/w200/${media.poster_path}`" :alt="media.title || media.name" class="media-image" />
          </div>
          <div class="media-details">
            <h2 v-if="media.title || media.name" class="media-title">{{ media.title || media.name }}</h2>
            <div v-if="media.vote_average" class="media-rating">Rating: {{ media.vote_average }}</div>
          </div>
        </div>
      </NuxtLink>
    </div>
    <NuxtLink :to="'#recommendations'" v-if="recommendations.results.length > 5">View More</NuxtLink>
  </div>
</template>

<script setup>
const props = defineProps({
    recommendations: Object,
})

const imageUrl = computed(() => {
  return props.recommendations.results.poster_path ? `https://image.tmdb.org/t/p/w200${props.recommendations.results.poster_path}` : '';
});
</script>

<style scoped>
.media-item {
  display: flex;
  align-items: flex-start;
  background-color: #fff;
  border: 1px solid #ddd;
  border-radius: 8px;
  overflow: hidden;
  margin-bottom: 20px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.media-image-wrapper {
  flex-shrink: 0;
  width: 100px;
  height: 150px;
  background-color: #f4f4f4;
  display: flex;
  align-items: center;
  justify-content: center;
}

.media-image {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.media-details {
  padding: 20px;
  flex-grow: 1;
}

.media-title {
  margin: 0;
  margin-bottom: 10px;
  font-size: 1.2em;
  font-weight: bold;
}

.media-rating {
  font-weight: bold;
  color: #ff9800;
}

.media-item-content {
  display: flex;
  align-items: flex-start;
}

.media-item-link {
  color: black;
  text-decoration: none;
}
</style>