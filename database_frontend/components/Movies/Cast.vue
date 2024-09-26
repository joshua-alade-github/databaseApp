<template>
  <div class="cast-container">
    <div v-for="actor in cast" :key="actor.id" class="cast-item">
      <img :src="getImageUrl(actor.profile_path)" alt="Actor" class="actor-image" />
      <div class="actor-info">
        <div class="actor-name">{{ actor.name }}</div>
        <div class="actor-character">{{ actor.character }}</div>
      </div>
    </div>
  </div>
</template>

<script setup>
const props = defineProps({
  cast: Object,
});

const getImageUrl = (profilePath) => {
    return profilePath
    ? `https://image.tmdb.org/t/p/w200${profilePath}`
    : 'https://via.placeholder.com/200';
}
</script>

<style scoped>
.cast-item {
  display: flex;
  margin-bottom: 20px;
}

.actor-image {
  width: 50px; /* Adjust the image size as needed */
  height: 50px;
  object-fit: cover;
  margin-right: 10px;
}

.actor-info {
  display: flex;
  flex-direction: column;
}

.actor-name {
  font-weight: bold;
}

.actor-character {
  margin-top: 5px;
}
</style>
