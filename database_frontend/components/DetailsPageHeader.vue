<template>
  <div class="details-page-header">
    <div v-if="backdropUrl" class="backdrop-image">
      <img :src="backdropUrl" :alt="media.title || media.name" class="backdrop" />
    </div>
    <div v-if="imageUrl" class="image-wrapper">
      <img :src="imageUrl" :alt="media.title || media.name" class="image" />
    </div>
    <div class="movie-details">
      <h2 class="movie-title">{{ media.title || media.name }} ({{ releaseDate }})</h2>
      <p>
        <template 
          v-for="(genre, index) in media.genres"
          :key="genre.id"
        >
          <NuxtLink :to="'#'" class="genre-link">
            {{ genre.name }}
          </NuxtLink>
          {{ index < media.genres.length - 1 ? '• ' : '' }}
        </template><slug v-if="media.runtime"> | {{ media.runtime }} mins</slug>
      </p>
      <div v-if="media.vote_average" class="movie-rating">TMDB Rating: {{ media.vote_average }}</div>
      <MoviesActionButtons />
      <p v-if="media.tagline" class="movie-overview">{{ media.tagline }}</p>
      <p v-if="media.overview" class="movie-overview">{{ media.overview }}</p>
      <p v-if="directorName" class="movie-overview">{{ directorName }} - Director</p>
      <p v-if="media.created_by" class="movie-overview">
        Created By: 
        <slug 
          v-for="(created_by, index) in media.created_by"
          :key="created_by.id"
        >
          {{ created_by.name }}{{ index < media.created_by.length - 1 ? ', ' : '' }}
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
  let date = new Date(props.media.release_date || props.media.first_air_date)
  return date.getUTCFullYear();
});

const imageUrl = computed(() => {
  const imagePath = props.media.poster_path || props.media.profile_path;

  if (imagePath) {
    const imageSize = 'w300';
    return `https://image.tmdb.org/t/p/${imageSize}${imagePath}`;
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

const directorName = computed(() => {
  if (props.credits && props.credits.crew) {
    const director = props.credits.crew.find(person => person.job === 'Director');
    return director ? director.name : null;
  } else {
    return null;
  }
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
  color: white; /* Set text color to white */
}

.movie-title {
  font-size: 50px;
  color: white; /* Set title color to white */
}

.genre-link {
  color: white; /* Set genre link color to white */
  text-decoration: none; /* Remove underline from links */
}

/* Add media queries or additional styles for responsiveness if needed */
</style>