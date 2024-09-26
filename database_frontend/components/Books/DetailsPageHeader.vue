<template>
  <div class="details-page-header">
    <div v-if="backdropUrl" class="backdrop-image">
      <img :src="backdropUrl" :alt="media.volumeInfo.title" class="backdrop" />
    </div>
    <div v-if="imageUrl" class="image-wrapper">
      <img :src="imageUrl" :alt="media.volumeInfo.title" class="image" />
    </div>
    <div class="movie-details">
      <h2 class="movie-title">{{ media.volumeInfo.title }} ({{ releaseDate }})</h2>
      <p v-if="media.volumeInfo.categories">
        <template 
          v-for="(genre, index) in media.volumeInfo.categories"
          :key="index"
        >
          <NuxtLink :to="'#'" class="genre-link">
            {{ genre }}
          </NuxtLink>
          {{ index < media.volumeInfo.categories.length - 1 ? '• ' : '' }}
        </template><slug v-if="media.volumeInfo.pageCount"> | {{ media.volumeInfo.pageCount }} pages</slug>
      </p>
      <div v-if="media.volumeInfo.averageRating" class="movie-rating">Google Rating: {{ media.volumeInfo.averageRating }}</div>
      <MoviesActionButtons />
      <p v-if="media.tagline" class="movie-overview">{{ media.tagline }}</p>
      <p v-if="media.volumeInfo.description" class="movie-overview">{{ media.volumeInfo.description }}</p>
      <p v-if="media.volumeInfo.authors" class="movie-overview">
        Authors: 
        <slug 
          v-for="(authors, index) in media.volumeInfo.authors"
          :key="index"
        >
          {{ authors }}{{ index < media.volumeInfo.authors.length - 1 ? ', ' : '' }}
        </slug>
      </p>
    </div>
</div>
</template>

<script setup>
const props = defineProps({
  media: Object,
  credits: Object
});

const releaseDate = computed(() => {
  let date = new Date(props.media.volumeInfo.publishedDate || props.media.first_air_date)
  return date.getUTCFullYear();
});

const imageUrl = computed(() => {
  const imagePath = props.media.volumeInfo.imageLinks.thumbnail || props.media.profile_path;

  if (imagePath) {
    return `${imagePath}`;
  }
  return '';
});

const backdropUrl = computed(() => {
  const imagePath = props.media.backdrop_path;

  if (imagePath) {
    return `https://image.tmdb.org/t/p/original${imagePath}`;
  }
  return '';
});

</script>

<style scoped>
.details-page-header {
  display: flex;
  justify-content: center;
  align-items: center;
  position: relative;
}

.backdrop-image {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: -1; /* Place it behind other content */
}

.backdrop {
  width: 100%;
  height: 100%;
  object-fit: cover; /* Ensure the image covers the entire space */
  filter: brightness(50%); /* Apply a black tint with 50% brightness */
}

.image-wrapper {
  margin: 20px; /* Adjust the margin as needed */
}

.image {
  width: 300px; /* Adjust the width as needed */
  height: 450px; /* Adjust the height as needed */
  object-fit: cover;
}

.movie-details {
  font-size: 22px;
  color: black; /* Set text color to white */
}

.movie-title {
  font-size: 50px;
  color: black; /* Set title color to white */
}

.genre-link {
  color: black; /* Set genre link color to white */
  text-decoration: none; /* Remove underline from links */
}

/* Add media queries or additional styles for responsiveness if needed */
</style>