<template>
    <DetailsPageHeader
      v-if="mediaData"
      :media="mediaData"
      :credits="mediaCredits"
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
const mediaData = ref(null)
const mediaCredits = ref(null)
const mediaPhotos = ref(null)
const mediaVideos = ref(null)
const mediaExternalIDs = ref(null)
const mediaKeywords = ref(null)
const mediaLists = ref(null)
const mediaRecommendations = ref(null)
const mediaReleaseDates = ref(null)
const mediaReviews = ref(null)
const mediaSimilar = ref(null)
const mediaWatchProviders = ref(null)

onMounted(async () => {
    const media_id = route.params.id
    const media_type = route.params.type

    try {
        const data = await getMovieDetails(media_type, media_id);
        mediaData.value = data.mediaData
        mediaCredits.value = data.mediaCredits
        mediaPhotos.value = data.mediaPhotos
        mediaVideos.value = data.mediaVideos
        mediaExternalIDs.value = data.mediaExternalIDs
        mediaKeywords.value = data.mediaKeywords
        mediaLists.value = data.mediaLists
        mediaRecommendations.value = data.mediaRecommendations
        mediaReleaseDates.value = data.mediaReleaseDates
        mediaReviews.value = data.mediaReviews
        mediaSimilar.value = data.mediaSimilar
        mediaWatchProviders.value = data.mediaWatchProviders
    } catch (error) {
        console.error("Failed to fetch movie details:", error);
    }
});

const getMovieDetails = async (media_type, media_id) => {
  return await $fetch(`${useRuntimeConfig().public.BASE_API_URL}/get-movie-details/${media_type}/${media_id}/`);
};
</script>