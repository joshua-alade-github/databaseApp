<template>
    <div>
      <div class="season-details" v-if="seasonDetails">
        <img :src="getImageUrl(seasonDetails.poster_path)" alt="season" class="season-image" />
        <div class="season-info">
          <h1>Season {{ seasonDetails.season_number }}</h1>
          <slug>({{ new Date(seasonDetails.air_date).getFullYear() }})</slug>
          <p>{{ seasonDetails.overview }}</p>
        </div>
      </div>
      <h3 v-if="seasonDetails">{{ seasonDetails.episodes.length }} Episodes</h3>
      <div v-if="seasonDetails" v-for="episode in seasonDetails.episodes" :key="episode.id" class="episode-card">
        <div class="episode-image">
          <img :src="getEpisodeImageUrl(episode.still_path)" :alt="episode.name" />
        </div>
        <div class="episode-details">
          <NuxtLink :to="`${$route.path}/episode/${episode.episode_number}`" class="movie-card-link">
            <h2>{{ episode.episode_number }} | {{ episode.name }}</h2>
          </NuxtLink>
          <div class="details-line">
            <p v-if="episode.vote_average" class="episode-info">Rating: {{ episode.vote_average }}</p>
            <p v-if="episode.runtime" class="episode-info">{{ episode.runtime }} minutes</p>
            <p v-if="episode.air_date" class="episode-info">{{ episode.air_date }}</p>
          </div>
          <p v-if="episode.overview" class="overview">{{ episode.overview }}</p>
        </div>
      </div>
    </div>
    {{ seasonDetails }}
</template>

<script setup>
const seasonDetails = ref(null)
const route = useRoute();

const seriesId = ref(route.params.id);
const seasonNumber = ref(route.params['season']);

const getEpisodeImageUrl = (episodeImg) => {
  return `https://image.tmdb.org/t/p/w200${episodeImg}`;
};

onMounted(async () => {
    try {
        const data = await getSeasonDetails();
        seasonDetails.value = data
      } catch (error) {
        console.error("Failed to fetch movie details:", error);
    }
});

const getSeasonDetails = async () => {
  return await $fetch(`${useRuntimeConfig().public.BASE_API_URL}/get-season-details/${seriesId.value}/${seasonNumber.value}/`);
};

const getImageUrl = (poster_path) => {
    return poster_path
    ? `https://image.tmdb.org/t/p/w200${poster_path}`
    : 'https://via.placeholder.com/200';
}
</script>

<style lang="scss" scoped>
.season-details {
  display: flex;
  align-items: flex-start;
}

.season-image {
  flex: 0 0 auto;
  margin-right: 20px;
}

.season-info {
  flex: 1 1 auto;
}

h1, p {
  margin: 0;
}

.slug {
  color: #888;
  font-style: italic;
}

.episode-card {
  display: flex;
  margin-bottom: 20px;
  background-color: #f9f9f9;
  border-radius: 10px;
  overflow: hidden;
}

.episode-image {
  flex: 0 0 auto;
}

.episode-image img {
  width: 200px;
  height: auto;
  border-radius: 10px 0 0 10px;
}

.episode-details {
  flex: 1 1 auto;
  padding: 20px;
}

.episode-details h2 {
  margin-top: 0;
}

.details-line {
  display: flex;
  align-items: center;
  margin-bottom: 10px;
}

.episode-info {
  margin-right: 20px;
  font-size: 14px;
}

.overview {
  margin-top: 0;
}
</style>