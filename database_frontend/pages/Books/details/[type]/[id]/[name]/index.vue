<template>
    <BooksDetailsPageHeader
      v-if="bookData"
      :media="bookData"
    />
    <DetailsPageContent
      v-if="mediaCredits"
      :media="mediaData"
      :credits="mediaCredits"
      :recommendations="mediaRecommendations"
    />
</template>

<script setup>
const route = useRoute()
const bookData = ref(null)

onMounted(async () => {
    const book_id = route.params.id

    try {
        const data = await getBookDetails(book_id);
        bookData.value = data
    } catch (error) {
        console.error("Failed to fetch book details:", error);
    }
});

const getBookDetails = async (book_id) => {
  return await $fetch(`${useRuntimeConfig().public.BASE_API_URL}/get-book-details/${book_id}/`);
};
</script>