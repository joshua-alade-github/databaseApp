export const useSearchStore = defineStore({
  id: 'search',
  state: () => ({
    query: '',
    mediaType: '',
    baseApiUrl: useRuntimeConfig().public.BASE_API_URL,
    searchResults: [],
    nextPage: 1,
    hasMoreResults: true,
  }),
  actions: {
    setSearchQuery(query) {
      this.query = query;
    },
    setMediaType(type) {
      this.mediaType = type;
    },
    async getSearchResults() {
      try{
        const response = await $fetch(`${this.baseApiUrl}/search`, {
          params: {
            query: this.query,
            mediaType: this.mediaType,
            page: this.nextPage,
          },
        });

        if (this.mediaType == "books") {
          this.searchResults.push(...response.items);
        }
        else {
          this.searchResults.push(...response.results);
        }
        console.log("test store");
        this.hasMoreResults = this.nextPage < response.total_pages;
        this.nextPage++;
      }
      catch (error) {
        console.error('Error fetching more results:', error);
      }
    },
    resetSearch() {
      this.searchResults = [];
      this.nextPage = 1;
      this.hasMoreResults = true;
    },
  }
});
