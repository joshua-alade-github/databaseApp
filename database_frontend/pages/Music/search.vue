<template>
  <!-- <div class="movies-search-page" ref="scrollContainer"> -->
  <div class="music-search-page">
    <div class="music-grid">
      {{ searchStore.searchResults }}
      <MusicCard
        v-for="music in searchStore.searchResults"
        :key="music.id"
        :id="music.id"
        :mediaType="music.media_type"
        :title="music.title || music.name"
        :image="music.thumb || music.profile_path"
        :overview="music.overview"
        :voteAverage="music.vote_average"
      />
    </div>
  </div>
</template>

<script setup>
import { useSearchStore } from '@/stores/searchStore';

const searchStore = useSearchStore();
const route = useRoute();

watch(() => route.query.q, (newQuery) => {
  searchStore.resetSearch();
  searchStore.setSearchQuery(newQuery || '');
  searchStore.getSearchResults();
  console.log("test watch");
  console.log(newQuery);
}, {
  immediate: true // This option calls the handler immediately with the current route
});

// watch(
//   () => ({
//     path: route.path,
//     query: route.query.q
//   }),
//   (newRoute, oldRoute) => {
//     // Destructure new values
//     const { path: newPath, query: newQuery } = newRoute;
//     // Destructure old values, provide default empty object to avoid errors
//     const { path: oldPath } = oldRoute || {};

//     console.log("test", oldPath, "new", newPath);

//     if (newPath === '/movies/search' && oldPath === '/movies/search') {
//       searchStore.resetSearch();
//       searchStore.setSearchQuery(newQuery || '');
//       searchStore.getSearchResults();
//       console.log("test watch");
//     }
//   },
//   {
//     immediate: true // Run the handler immediately with the current route
//   }
// );
</script>

<style>
.music-grid {
  /* Styles for the music grid */
}
.pagination {
  /* Styles for the pagination controls */
}
</style>
