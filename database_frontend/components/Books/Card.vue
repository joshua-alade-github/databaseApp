
import type { NuxtLink } from '#build/components';
<template>
  <NuxtLink :to="`/books/details/${mediaType}/${id}/${title}`" class="book-card-link">
    <div class="book-card">
      <div v-if="image" class="book-image-wrapper">
        <img :src="imageUrl" :alt="title" class="book-image" />
      </div>
      <div class="book-details">
        <h2 class="book-title">{{ title }}</h2>
        <p v-if="overview" class="book-overview">{{ overview }}</p>
        <div v-if="voteAverage" class="book-rating">Rating: {{ voteAverage }}</div>
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
  return props.image ? `${props.image}` : '';
});
</script>

<style>
.book-card {
  display: flex;
  align-items: flex-start;
  background-color: #fff;
  border: 1px solid #ddd;
  border-radius: 8px;
  overflow: hidden;
  margin-bottom: 20px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.book-image-wrapper {
  flex-shrink: 0;
  width: 200px;
  height: 300px;
  background-color: #f4f4f4;
  display: flex;
  align-items: center;
  justify-content: center;
}

.book-image {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.book-details {
  padding: 20px;
  flex-grow: 1;
}

.book-title {
  margin: 0;
  margin-bottom: 10px;
  font-size: 1.2em;
  font-weight: bold;
}

.book-overview {
  margin: 0;
  margin-bottom: 10px;
  font-size: 0.9em;
}

.book-rating {
  font-weight: bold;
  color: #ff9800;
}
</style>
