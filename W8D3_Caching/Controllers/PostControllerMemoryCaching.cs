using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory; 
using System.Text.Json;
using W8D3_Caching.Models;

namespace W8D3_Caching.Controllers{

 public class PostMemoryCachingController : Controller{

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _memoryCache;

    // Inject IMemoryCache into the constructor
        public PostMemoryCachingController(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            _httpClientFactory = httpClientFactory;
            _memoryCache = memoryCache; // Initialize memory cache
        }

          // Renders the view that uses JavaScript to dynamically load posts
        public IActionResult Index()
        {
            return View(); 
        }

    // Returns cached or fetched data as JSON
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        //Define cache key 
        {string cacheKey = "posts";
            List<Post> posts;
            //Check if data is stored in cache, if not it will fetch it from the API
            if (!_memoryCache.TryGetValue(cacheKey, out posts))
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts");
                //After fetching the data, store it in MemoryCache
                if (response.IsSuccessStatusCode)
                {
                    posts = await response.Content.ReadFromJsonAsync<List<Post>>();
                    if (posts != null)
                    {
                        _memoryCache.Set(cacheKey, posts, TimeSpan.FromMinutes(5));
                    }
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Failed to fetch posts from API.");
                }
            }

            return Json(posts);
        }}}