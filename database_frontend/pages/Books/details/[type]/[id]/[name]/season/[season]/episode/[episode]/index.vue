<template>
  <div>
    <div>
      <div class="episode-details" v-if="episodeDetails">
        <img :src="getImageUrl(episodeDetails.still_path)" alt="episode image" class="episode-image" />
        <div class="episode-info">
          <h1>Episode {{ episodeDetails.episode_number }} | {{ episodeDetails.name }}</h1>
          <p class="episode-info">Rating: {{ episodeDetails.vote_average }}</p>
          <p>{{ episodeDetails.runtime }} minutes</p>
          <p>({{ new Date(episodeDetails.air_date).getFullYear() }})</p>
          <p>{{ episodeDetails.overview }}</p>
        </div>
      </div>
      <h1>Crew</h1>
      <div class="crew-container" v-if="episodeDetails">
        <div v-for="person in episodeDetails.crew" :key="person.id" class="episode-card">
          <img :src="getImageUrl(person.profile_path)" alt="person" class="person-image" />
          <p>{{ person.name }} - {{ person.job }}</p>
        </div>
      </div>
      <h1>Guest Stars</h1>
      <div class="crew-container" v-if="episodeDetails">
        <div v-for="actor in episodeDetails.guest_stars" :key="actor.id" class="episode-card">
          <img :src="getImageUrl(actor.profile_path)" alt="actor" class="person-image" />
          <p>{{ actor.name }} - Character: {{ actor.character }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
const episodeDetails = ref(null)
const route = useRoute();

const seriesId = ref(route.params.id);
const seasonNumber = ref(route.params['season']);
const episodeNumber = ref(route.params['episode'])

onMounted(async () => {
    try {
        const data = await getEpisodeDetails();
        episodeDetails.value = data
      } catch (error) {
        console.error("Failed to fetch movie details:", error);
    }
});

const getEpisodeDetails = async () => {
  return await $fetch(`${useRuntimeConfig().public.BASE_API_URL}/get-episode-details/${seriesId.value}/${seasonNumber.value}/${episodeNumber.value}/`);
};

const getImageUrl = (profile_path) => {
    return profile_path
    ? `https://image.tmdb.org/t/p/w200${profile_path}`
    : 'https://via.placeholder.com/200';
}

</script>

<style scoped>
.crew-container {
  display: flex;
  flex-wrap: wrap; /* Allows wrapping to a new line */
  gap: 20px; /* Adds space between the cards */
  justify-content: center; /* Centers the cards */
  padding: 10px;
}

.episode-card {
  flex: 0 1 calc(20% - 20px); /* Ensures cards take up a reasonable amount of space */
  margin-bottom: 20px; /* Adds space below each card */
  text-align: center;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  padding: 10px;
  border-radius: 10px;
}

.person-image {
  width: 100px;
  height: 150px;
  object-fit: cover;
  border-radius: 10px;
  margin-bottom: 5px;
}

.episode-card p {
  margin: 0;
  font-size: 14px;
}
</style>