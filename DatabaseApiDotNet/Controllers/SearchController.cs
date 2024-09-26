using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class SearchController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ApiKeyService _apiKeyService;

        public SearchController(IHttpClientFactory clientFactory, ApiKeyService apiKeyService)
        {
            _clientFactory = clientFactory;
            _apiKeyService = apiKeyService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query, [FromQuery] string page, [FromQuery] string mediaType)
        {
            var client = _clientFactory.CreateClient();

            var apiEndpoints = new Dictionary<string, string>
            {
                {"movies", "https://api.themoviedb.org/3/search/multi"},
                {"books", "https://www.googleapis.com/books/v1/volumes"},
                {"games", "https://api.rawg.io/api/games"},
                {"music", "https://api.discogs.com/database/search"}
            };

            var apiKeys = new Dictionary<string, string>
            {
                {"movies", $"Bearer {_apiKeyService.TheMovieDbBearerApiKey}"},
                //{"books", _apiKeyService.BooksApiKey},
                {"games", _apiKeyService.RawgApiKey},
                {"music", $"Discogs key={_apiKeyService.DiscogsConsumerKey}, secret={_apiKeyService.DiscogsConsumerSecret}"}
            };

            if (!apiEndpoints.ContainsKey(mediaType) || !apiKeys.ContainsKey(mediaType))
            {
                return BadRequest("Invalid media type specified.");
            }

            var apiUrl = apiEndpoints[mediaType];
            var apiKey = apiKeys[mediaType];

            client.DefaultRequestHeaders.Add("Authorization", apiKey);

            var response = await client.GetAsync($"{apiUrl}?query={query}&page={page}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(JsonSerializer.Deserialize<JsonElement>(content));
            }

            return StatusCode((int)response.StatusCode, "Failed to fetch data from the external API");
        }
    }
}