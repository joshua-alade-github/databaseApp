<template>
  <div class="details-tabs">
    <button
       v-for="(_, tab) in tabs"
       :key="tab"
       :class="['tab-button', { active: currentTab === tab }]"
       @click="currentTab = tab"
     >
      {{ tabsButton[tab] }}
    </button>
	<component :is="currentTabComp" v-bind="tabs[currentTab]" class="tab"></component>
  </div>
</template>

<script>
import { MoviesCast, MoviesCrew, MoviesInfoBox, MoviesSeasons } from '#components';
export default {
  components: {
    MoviesInfoBox,
    MoviesCast,
    MoviesCrew,
    MoviesSeasons,
  }
}
</script>
<script setup>
const props = defineProps({
  media: Object,
  credits: Object,
});
 
let currentTab = ref('MoviesInfoBox')

let currentTabComp = computed(() => resolveComponent(currentTab.value))

const tabs = {
  MoviesInfoBox: { media: props.media },
  MoviesCast: { cast: props.credits.cast },
  MoviesCrew: { crew: props.credits.crew },
  ...(props.media.seasons ? { MoviesSeasons: { seasons: props.media.seasons } } : {}),
};

const tabsButton = {
  MoviesInfoBox: 'Info',
  MoviesCast: 'Cast',
  MoviesCrew: 'Crew',
  MoviesSeasons: 'Seasons',
}
</script>

<style scoped>
.info-box-container {
  
}
.details-tabs {
  padding: 20px 30px;
  user-select: none;
}

.tab-button {
  padding: 6px 10px;
  border-top-left-radius: 3px;
  border-top-right-radius: 3px;
  border: 1px solid #ccc;
  cursor: pointer;
  background: #f0f0f0;
  margin-bottom: -1px;
  margin-right: -1px;
}
.tab-button:hover {
  background: #e0e0e0;
}
.tab-button.active {
  background: #e0e0e0;
}
.tab {
  border: 1px solid #ccc;
  padding: 10px;
  width: 100%; /* Set your desired fixed width */
  height: 400px; /* Set your desired fixed height */
  overflow-y: auto; /* Enable vertical scrolling if needed */
  font-size: 18px; /* Adjust the font size as needed */
}
</style>