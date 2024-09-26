<template>
  <div class="details-page-content">
    <MoviesDetailsTabs
      v-if="credits"
      :media="media"
      :credits="credits"
      class="details-tabs" 
    />
    <MoviesMediaTabs
      v-if="recommendations"
      :recommendations="recommendations"
      class="media-tabs"
    />
</div>
</template>

<script setup>
const props = defineProps({
  media: Object,
  credits: Object,
  recommendations: Object,
});
</script>

<style scoped>
.details-page-content {
  display: flex;
  border: 1px solid #eee;
  border-radius: 2px;
  margin-top: 1em;
  margin-bottom: 40px;
}

.details-tabs {
  flex: 1; /* Takes up one-third of the container */
  box-sizing: border-box; /* Include padding and border in the width calculation */
}

.media-tabs {
  flex: 3; /* Takes up two-thirds of the container */
  box-sizing: border-box; /* Include padding and border in the width calculation */
}
</style>