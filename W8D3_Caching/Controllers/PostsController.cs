using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Text.Json;
using W8D3_Caching.Models;

namespace W8D3_Caching.Controllers{

    public class PostController : Controller{


      private readonly IHttpClientFactory _httpClientFactory;
      private readonly IDatabase _redisDb;


public PostController(IHttpClientFactory httpClientFactory, IConnectionMultiplexer redis)
        {
            _httpClientFactory = httpClientFactory;
            _redisDb = redis.GetDatabase();
        }

        
    // Serves the HTML page only
    public IActionResult Index()
    {
        return View();
    }

        public async Task<IActionResult> GetPosts()
        {
            //define the cache key
            string cacheKey = "posts";
            //retrive values assoited with cacheKey from cache
            var cachedPosts = await _redisDb.StringGetAsync(cacheKey);

            List<Post>? posts;
            
            //check if the posts data in the cache
            if (!string.IsNullOrEmpty(cachedPosts))
            {
                posts = JsonSerializer.Deserialize<List<Post>>(cachedPosts);
            }
             /* else if the posts data not stored in the cache
             it will fetch it from the API */
            else
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts");
                posts = await response.Content.ReadFromJsonAsync<List<Post>>();

                if (posts != null)
                {
                    var serialized = JsonSerializer.Serialize(posts);
                    await _redisDb.StringSetAsync(cacheKey, serialized, TimeSpan.FromMinutes(5));
                }
            }

            return Json(posts);
        }
    }
}


    