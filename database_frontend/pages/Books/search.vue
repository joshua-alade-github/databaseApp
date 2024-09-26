<template>
  <!-- <div class="movies-search-page" ref="scrollContainer"> -->
  <div class="books-search-page">
    <div class="books-grid">
      <BooksCard
        v-for="book in searchStore.searchResults"
        :key="book.id"
        :id="book.id"
        :title="book.volumeInfo.title"
        :image="book.volumeInfo.imageLinks.thumbnail || book.profile_path"
        :overview="book.volumeInfo.description"
        :voteAverage="book.volumeInfo.averageRating"
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
.movies-grid {
  /* Styles for the movies grid */
}
.pagination {
  /* Styles for the pagination controls */
}
</style>
