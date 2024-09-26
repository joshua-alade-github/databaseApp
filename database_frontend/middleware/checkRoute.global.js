import { useSearchStore } from '~/stores/searchStore';

export default function(context) {
  const pathSegments = context.path.split('/');
  const mediaType = pathSegments[1] || null;
  const searchStore = useSearchStore();

  switch (mediaType) {
    case 'movies':
      searchStore.setMediaType('movies');
      break;
    case 'books':
      searchStore.setMediaType('books');
      break;
    case 'games':
      searchStore.setMediaType('games');
      break;
    case 'music':
      searchStore.setMediaType('music');
      break;
    default:
      searchStore.setMediaType('unknown');
  }
}