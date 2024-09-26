<template>
  <div class="slideshow-container">
    <h1>{{ title }}</h1>
    <transition name="slide-fade" mode="out-in">
      <div :key="currentIndex" class="slide-wrapper">
        <div v-for="(item, index) in getCurrentItems" :key="index" class="slide-item">
          <NuxtLink :to="`/movies/details/${item.media_type}/${item.id}/${item.title || item.name}`" class="item-link">
          <img :src="getImageUrl(item.poster_path)" alt="Item Image" class="item-image" />
          <div class="item-details">
            <h3 class="item-name">{{ getItemTitle(item) }}</h3>
            <p v-if="item.vote_average" class="item-rating">Rating: {{ item.vote_average }}</p>
          </div>
          </NuxtLink>
        </div>
      </div>
    </transition>

    <div class="navigation-buttons">
      <button @click="prev" :disabled="currentIndex === 0" class="prev-button">&#10094; Previous</button>
      <button @click="next" :disabled="currentIndex >= items.length - visibleItems" class="next-button">Next &#10095;</button>
    </div>

    <!-- Indicators -->
    <div class="indicators">
      <span
        v-for="(item, index) in indicators"
        :key="index"
        class="indicator"
        :class="{ active: index === currentIndicator }"
        @click="goToSlide(index)"
      ></span>
    </div>
  </div>
</template>

<script setup>
const props = defineProps({
  title: String,
  items: Array, // Array of items for the slideshow
});

const state = reactive({
  currentIndex: 0,
  visibleItems: 5,
});

const currentItem = computed(() => props.items[state.currentIndex]);

const getCurrentItems = computed(() => {
  const endIndex = state.currentIndex + state.visibleItems;
  return props.items.slice(state.currentIndex, endIndex);
});

const next = () => {
  const newIndex = state.currentIndex + state.visibleItems;
  if (newIndex < props.items.length) {
    state.currentIndex = newIndex;
  }
};

const prev = () => {
  const newIndex = state.currentIndex - state.visibleItems;
  if (newIndex >= 0) {
    state.currentIndex = newIndex;
  }
};

const goToSlide = (index) => {
  state.currentIndex = index;
};

const indicators = computed(() => {
  const numIndicators = Math.ceil(props.items.length / state.visibleItems);
  return new Array(numIndicators).fill(null);
});

const currentIndicator = computed(() => Math.floor(state.currentIndex / state.visibleItems));

const getImageUrl = (posterPath) => `https://image.tmdb.org/t/p/w200/${posterPath}`;

const getItemTitle = (item) => item.title || item.name;
</script>

<style scoped>
.slideshow-container {
  position: relative;
  display: flex; /* Add flexbox */
  flex-direction: column; /* Stack items vertically */
  align-items: center; /* Center items horizontally */
}

h1 {
  margin-bottom: 20px; /* Optional: Adjust margin as needed */
}

.slide-wrapper {
  display: flex;
  transition: transform 0.5s ease;
}

.slide-item {
  margin-right: 10px;
  width: 200px;
  border-radius: 10px;
  border: 1px solid #ccc;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

.item-image {
  width: 200px;
  height: 300px;
}

.item-details {
  text-align: center;
}

.item-link {
  text-decoration: none;
  color: inherit;
}

.navigation-buttons {
  position: absolute;
  top: 50%;
  width: 100%;
  display: flex;
  justify-content: space-between;
}

.prev-button,
.next-button {
  background-color: #4CAF50;
  color: white;
  padding: 10px 20px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.prev-button:hover,
.next-button:hover {
  background-color: #45a049;
}

/* Styling for disabled state */
.prev-button:disabled,
.next-button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

.indicators {
  display: flex;
  justify-content: center;
  margin-top: 10px;
}

.indicator {
  width: 10px;
  height: 10px;
  border-radius: 50%;
  background-color: #bbb;
  margin: 0 5px;
  cursor: pointer;
}

.indicator.active {
  background-color: #333;
}

/* Your existing styles for the slideshow */
</style>
