using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class BooksController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public BooksController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        [HttpGet("get-book-details/{bookId}")]
        public async Task<IActionResult> GetBookDetails(string bookId)
        {
            var client = _clientFactory.CreateClient();
            var apiUrl = $"https://www.googleapis.com/books/v1/volumes/{bookId}";

            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var bookData = JsonSerializer.Deserialize<JsonElement>(content);
                return Ok(bookData);
            }
            else
            {
                return StatusCode((int)response.StatusCode, 
                    $"Failed to fetch book data. Status code: {response.StatusCode}");
            }
        }
    }
}