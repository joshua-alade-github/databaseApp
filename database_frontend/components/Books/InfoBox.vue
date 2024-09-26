<template>
  <div class="info-box-container">
    <ul v-if="media.spoken_languages">
      <li>
        <p class="section-header"><strong>Spoken Languages:</strong></p>
        <ul>
          <li v-for="langs in media.spoken_languages" :key="langs.id">
            {{ langs.name }}
          </li>
        </ul>
      </li>
    </ul>
    <p v-if="budgetInCurrency"><strong>Budget: </strong>{{ budgetInCurrency }}</p>
    <p v-if="revenueInCurrency"><strong>Revenue: </strong>{{ revenueInCurrency }}</p>
    <p v-if="media.status"><strong>Status: </strong>{{ media.status }}</p>
    <p v-if="media.number_of_seasons"><strong>Seasons: </strong>{{ media.number_of_seasons }}</p>
    <p v-if="media.number_of_episodes"><strong>Episodes: </strong>{{ media.number_of_episodes }}</p>
    <p v-if="media.episode_run_time"><strong>Episode runtime: </strong>{{ media.episode_run_time }}</p>
    <ul v-if="media.networks">
      <li>
        <p class="section-header"><strong>Networks:</strong></p>
        <ul>
          <li v-for="networks in media.networks" :key="networks.id">
            {{ networks.name }}
          </li>
        </ul>
      </li>
    </ul>
    <ul v-if="media.production_companies">
      <li>
        <p class="section-header"><strong>Production Companies:</strong></p>
        <ul>
          <li v-for="company in media.production_companies" :key="company.id">
            {{ company.name }}
          </li>
        </ul>
      </li>
    </ul>
    <ul v-if="media.production_countries">
      <li>
        <p class="section-header"><strong>Production Countries:</strong></p>
        <ul>
          <li v-for="country in media.production_countries" :key="country.id">
            {{ country.name }}
          </li>
        </ul>
      </li>
    </ul>
    <p v-if="media.homepage">
      <strong>Homepage: </strong>
      <NuxtLink :to="media.homepage" target="_blank" rel="noopener noreferrer">
        {{ media.homepage }}
      </NuxtLink>
    </p>
    <p v-if="imdbLink">
      <strong>IMDB: </strong>
      <NuxtLink :to="imdbLink" target="_blank" rel="noopener noreferrer">
        {{ imdbLink }}
      </NuxtLink>
    </p>
    <p v-if="tmdbLink">
      <strong>TMDB: </strong>
      <NuxtLink :to="tmdbLink" target="_blank" rel="noopener noreferrer">
        {{ tmdbLink }}
      </NuxtLink>
    </p>
  </div>
</template>

<script setup>
const props = defineProps({
  media: Object,
});

const budgetInCurrency = computed((locale = 'en-US', currency = 'USD') => {
  if (props.media && props.media.budget) {
    return numToCurrency(props.media.budget, locale, currency);
  } else {
    return null;
  }
})

const revenueInCurrency = computed((locale = 'en-US', currency = 'USD') => {
  if (props.media && props.media.revenue) {
    return numToCurrency(props.media.revenue, locale, currency);
  } else {
    return null;
  }
})

const numToCurrency = (num, locale = 'en-US', currency = 'USD') => {
  return new Intl.NumberFormat(locale, {
    style: 'currency',
    currency: currency,
  }).format(num);
};

const imdbLink = computed(() => {
  if (props.media && props.media.imdb_id) {
    return `https://www.imdb.com/title/${props.media.imdb_id}`;
  } else {
    return null;
  }
})

const tmdbLink = computed(() => {
  if (props.media && props.media.imdb_id) {
    return `https://www.themoviedb.org/movie/${props.media.id}`;
  } else {
    return null;
  }
})

</script>

<style scoped>


.section-header {
  margin-bottom: 5px;
}

ul {
  list-style-type: none;
  padding: 0;
}

ul ul {
  padding-left: 20px;
}
</style>
