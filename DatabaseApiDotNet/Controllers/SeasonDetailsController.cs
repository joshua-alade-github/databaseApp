using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Headers;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class SeasonDetailsController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ApiKeyService _apiKeyService;

        public SeasonDetailsController(IHttpClientFactory clientFactory, ApiKeyService apiKeyService)
        {
            _clientFactory = clientFactory;
            _apiKeyService = apiKeyService;
        }

        [HttpGet("get-season-details/{seriesId:int}/{seasonNum:int}")]
        public async Task<IActionResult> GetSeasonDetails(string seriesId, string seasonNum)
        {
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKeyService.TheMovieDbBearerApiKey);

            var apiUrl = $"https://api.themoviedb.org/3/tv/{seriesId}/season/{seasonNum}";
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(JsonSerializer.Deserialize<JsonElement>(content));
            }

            return StatusCode((int)response.StatusCode, 
                $"Failed to fetch season details. Status code: {response.StatusCode}");
        }
    }
}