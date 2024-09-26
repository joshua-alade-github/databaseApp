import { onMounted, onUnmounted } from 'vue';

export function useWindowScroll(callback) {
  onMounted(() => {
    window.addEventListener('scroll', callback);
  });

  onUnmounted(() => {
    window.removeEventListener('scroll', callback);
  });
}
