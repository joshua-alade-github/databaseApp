<template>
  <div class="moviesHomePage">
    <h1>
    Movies // page content
    </h1>
    <div>
      <div class="trending-buttons">
        <button :class="{ 'active-button': activeSlideShow === 'trendingDay' }" @click="showTrendingDay">Today</button>
        <button :class="{ 'active-button': activeSlideShow === 'trendingWeek' }" @click="showTrendingWeek">This Week</button>
      </div>

      <MoviesSlideShow
        v-if="activeSlideShow === 'trendingDay' && trendingDayData"
        :items="trendingDayData.results"
        :title="'Trending Today'"
      />
      <MoviesSlideShow
        v-if="activeSlideShow === 'trendingWeek' && trendingWeekData"
        :items="trendingWeekData.results"
        :title="'Trending This Week'"
      />
    </div>
    <!-- <MoviesSlideShow
      v-if="popularMoviesData"
      :items="popularMoviesData.results"
      :title="'Popular Right Now'"
    />
    <MoviesSlideShow
      v-if="upcomingMoviesData"
      :items="upcomingMoviesData.results"
      :title="'Upcoming Movies'"
    /> -->
  </div>
</template>

<script setup>
const activeSlideShow = ref('trendingDay');
const trendingDayData = ref(null)
const trendingWeekData = ref(null)
//const nowPlayingMoviesData = ref(null)
//const onTheAirTVData = ref(null)
//const popularMoviesData = ref(null)
//const popularTVData = ref(null)
//const upcomingMoviesData = ref(null)

onMounted(async () => {
    try {
        const data = await getMovieHome();
        trendingDayData.value = data.trendingDayData
        trendingWeekData.value = data.trendingWeekData
        //nowPlayingMoviesData.value = data.nowPlayingMoviesData
        //onTheAirTVData.value = data.onTheAirTVData
        //popularMoviesData.value = data.popularMoviesData
        //popularTVData.value = data.popularTVData
        //upcomingMoviesData.value = data.upcomingMoviesData
      } catch (error) {
        console.error("Failed to fetch movie details:", error);
    }
});

const getMovieHome = async () => {
  return await $fetch(`${useRuntimeConfig().public.BASE_API_URL}/get-movies-home/`);
};

const showTrendingDay = () => {
  activeSlideShow.value = 'trendingDay';
};

const showTrendingWeek = () => {
  activeSlideShow.value = 'trendingWeek';
};
</script>

<style lang="scss" scoped>
.trending-buttons {
  display: flex;
  justify-content: center;
}

button {
  padding: 10px 20px;
  margin: 0 5px;
  font-size: 16px;
  color: #000; /* Change text color to black */
  background-color: transparent;
  border: 1px solid #4CAF50; /* Default border */
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.active-button {
  background-color: #4CAF50;
  border: none; /* Remove border for the active button */
  color: #fff; /* Change text color to white for the active button */
}

button:hover {
  background-color: #45a049;
  color: #fff;
}
</style>
