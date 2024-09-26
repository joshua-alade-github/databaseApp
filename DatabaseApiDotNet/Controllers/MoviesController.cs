using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Headers;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class MoviesController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ApiKeyService _apiKeyService;

        public MoviesController(IHttpClientFactory clientFactory, ApiKeyService apiKeyService)
        {
            _clientFactory = clientFactory;
            _apiKeyService = apiKeyService;
        }

        [HttpGet("get-movies-home")]
        public async Task<IActionResult> GetMoviesHome()
        {
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKeyService.TheMovieDbBearerApiKey);

            var tasks = new[]
            {
                GetTrendingDay(client),
                GetTrendingWeek(client),
                GetNowPlayingMovies(client),
                GetOnTheAirTV(client),
                GetPopularMovies(client),
                GetPopularTV(client),
                GetUpcomingMovies(client)
            };

            await Task.WhenAll(tasks);

            var combinedData = new
            {
                trendingDayData = JsonSerializer.Deserialize<JsonElement>(tasks[0].Result),
                trendingWeekData = JsonSerializer.Deserialize<JsonElement>(tasks[1].Result),
                nowPlayingMoviesData = JsonSerializer.Deserialize<JsonElement>(tasks[2].Result),
                onTheAirTVData = JsonSerializer.Deserialize<JsonElement>(tasks[3].Result),
                popularMoviesData = JsonSerializer.Deserialize<JsonElement>(tasks[4].Result),
                popularTVData = JsonSerializer.Deserialize<JsonElement>(tasks[5].Result),
                upcomingMoviesData = JsonSerializer.Deserialize<JsonElement>(tasks[6].Result)
            };

            return Ok(combinedData);
        }

        [HttpGet("get-movie-details/{mediaType}/{mediaId:int}")]
        public async Task<IActionResult> GetMovieDetails(string mediaType, int mediaId)
        {
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKeyService.TheMovieDbBearerApiKey);

            var response = await client.GetAsync($"https://api.themoviedb.org/3/{mediaType}/{mediaId}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, $"Failed to fetch media data. Status code: {response.StatusCode}");
            }

            var mediaData = await response.Content.ReadAsStringAsync();

            if (mediaType == "movie" || mediaType == "tv")
            {
                var tasks = new[]
                {
                    GetMovieCredits(client, mediaType, mediaId),
                    GetMoviePhotos(client, mediaType, mediaId),
                    GetMovieVideos(client, mediaType, mediaId),
                    GetMovieExternalIDs(client, mediaType, mediaId),
                    GetMovieKeywords(client, mediaType, mediaId),
                    GetMovieLists(client, mediaType, mediaId),
                    GetMovieRecommendations(client, mediaType, mediaId),
                    GetMovieReleaseDates(client, mediaType, mediaId),
                    GetMovieReviews(client, mediaType, mediaId),
                    GetMovieSimilar(client, mediaType, mediaId),
                    GetMovieWatchProviders(client, mediaType, mediaId)
                };

                await Task.WhenAll(tasks);

                var combinedData = new
                {
                    mediaData = JsonSerializer.Deserialize<JsonElement>(mediaData),
                    mediaCredits = JsonSerializer.Deserialize<JsonElement>(tasks[0].Result),
                    mediaPhotos = JsonSerializer.Deserialize<JsonElement>(tasks[1].Result),
                    mediaVideos = JsonSerializer.Deserialize<JsonElement>(tasks[2].Result),
                    mediaExternalIDs = JsonSerializer.Deserialize<JsonElement>(tasks[3].Result),
                    mediaKeywords = JsonSerializer.Deserialize<JsonElement>(tasks[4].Result),
                    mediaLists = JsonSerializer.Deserialize<JsonElement>(tasks[5].Result),
                    mediaRecommendations = JsonSerializer.Deserialize<JsonElement>(tasks[6].Result),
                    mediaReleaseDates = JsonSerializer.Deserialize<JsonElement>(tasks[7].Result),
                    mediaReviews = JsonSerializer.Deserialize<JsonElement>(tasks[8].Result),
                    mediaSimilar = JsonSerializer.Deserialize<JsonElement>(tasks[9].Result),
                    mediaWatchProviders = JsonSerializer.Deserialize<JsonElement>(tasks[10].Result)
                };

                return Ok(combinedData);
            }

            return Ok(JsonSerializer.Deserialize<JsonElement>(mediaData));
        }

        private async Task<string> GetTrendingDay(HttpClient client)
        {
            var response = await client.GetAsync("https://api.themoviedb.org/3/trending/all/day");
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetTrendingWeek(HttpClient client)
        {
            var response = await client.GetAsync("https://api.themoviedb.org/3/trending/all/week");
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetNowPlayingMovies(HttpClient client)
        {
            var response = await client.GetAsync("https://api.themoviedb.org/3/movie/now_playing?page=1");
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetOnTheAirTV(HttpClient client)
        {
            var response = await client.GetAsync("https://api.themoviedb.org/3/tv/on_the_air?page=1");
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetPopularMovies(HttpClient client)
        {
            var response = await client.GetAsync("https://api.themoviedb.org/3/movie/popular?page=1");
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetPopularTV(HttpClient client)
        {
            var response = await client.GetAsync("https://api.themoviedb.org/3/tv/popular?page=1");
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetUpcomingMovies(HttpClient client)
        {
            var response = await client.GetAsync("https://api.themoviedb.org/3/movie/upcoming?page=1");
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetMovieCredits(HttpClient client, string mediaType, int mediaId)
        {
            var response = await client.GetAsync($"https://api.themoviedb.org/3/{mediaType}/{mediaId}/credits");
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetMoviePhotos(HttpClient client, string mediaType, int mediaId)
        {
            var response = await client.GetAsync($"https://api.themoviedb.org/3/{mediaType}/{mediaId}/images");
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetMovieVideos(HttpClient client, string mediaType, int mediaId)
        {
            var response = await client.GetAsync($"https://api.themoviedb.org/3/{mediaType}/{mediaId}/videos");
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetMovieExternalIDs(HttpClient client, string mediaType, int mediaId)
        {
            var response = await client.GetAsync($"https://api.themoviedb.org/3/{mediaType}/{mediaId}/external_ids");
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetMovieKeywords(HttpClient client, string mediaType, int mediaId)
        {
            var response = await client.GetAsync($"https://api.themoviedb.org/3/{mediaType}/{mediaId}/keywords");
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetMovieLists(HttpClient client, string mediaType, int mediaId)
        {
            var response = await client.GetAsync($"https://api.themoviedb.org/3/{mediaType}/{mediaId}/lists");
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetMovieRecommendations(HttpClient client, string mediaType, int mediaId)
        {
            var response = await client.GetAsync($"https://api.themoviedb.org/3/{mediaType}/{mediaId}/recommendations");
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetMovieReleaseDates(HttpClient client, string mediaType, int mediaId)
        {
            var response = await client.GetAsync($"https://api.themoviedb.org/3/{mediaType}/{mediaId}/release_dates");
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetMovieReviews(HttpClient client, string mediaType, int mediaId)
        {
            var response = await client.GetAsync($"https://api.themoviedb.org/3/{mediaType}/{mediaId}/reviews");
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetMovieSimilar(HttpClient client, string mediaType, int mediaId)
        {
            var response = await client.GetAsync($"https://api.themoviedb.org/3/{mediaType}/{mediaId}/similar");
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetMovieWatchProviders(HttpClient client, string mediaType, int mediaId)
        {
            var response = await client.GetAsync($"https://api.themoviedb.org/3/{mediaType}/{mediaId}/watch/providers");
            return await response.Content.ReadAsStringAsync();
        }
    }
}