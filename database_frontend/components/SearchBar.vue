<template>
  <div class="search-bar">
    <input v-model="searchQuery" @keyup.enter="performSearch" placeholder="Search...">
    <button @click="performSearch">Search</button>
  </div>
</template>

<script setup>
import { useSearchStore } from '~/stores/searchStore';

const router = useRouter()
const searchQuery = ref('');

const performSearch = () => {
  const searchStore = useSearchStore();
  searchStore.resetSearch();
  searchStore.setSearchQuery(searchQuery.value);
  searchStore.getSearchResults();
  router.push({ path: `/${searchStore.mediaType}/search`, query: { q: searchStore.query } });
};

</script>

<style lang="scss" scoped>
.search-bar {
  display: flex;
  align-items: center;
  margin-bottom: 20px;

  input {
    flex: 1;
    padding: 10px;
    font-size: 16px;
    border: 1px solid #ccc;
    border-radius: 4px;
    margin-right: 10px;
  }

  button {
    padding: 10px 15px;
    font-size: 16px;
    background-color: #007BFF;
    color: #fff;
    border: none;
    border-radius: 4px;
    cursor: pointer;
  }
}
</style>
