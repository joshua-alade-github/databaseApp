<template>
  <div class="button-container">
    <div v-for="(button, index) in buttons" :key="index" class="icon-button">
      <img :src="button.icon" alt="Icon" width="40" height="40">
      <div class="tooltip">{{ button.label }}</div>
    </div>
  </div>
</template>

<script setup>
const buttons = [
  { icon: 'icon1.png', label: 'Watchlist' },
  { icon: 'icon2.png', label: 'Favorite' },
  { icon: 'icon3.png', label: 'Add to custom lists' },
  { icon: 'icon4.png', label: 'Rating' },
];
</script>

<style scoped>
.button-container {
  display: flex;
}

.icon-button {
  position: relative;
  margin: 0 10px;
  cursor: pointer;
}

.tooltip {
  display: none;
  position: absolute;
  top: 100%;
  left: 50%;
  transform: translateX(-50%);
  background-color: #333333;
  color: #fff;
  padding: 5px;
  border-radius: 5px;
  font-size: 15px;
}

.icon-button:hover .tooltip {
  display: block;
}
</style>
