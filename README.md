# W8D3 Caching Demo: ASP.NET Core with IMemoryCache & Redis

This project demonstrates two types of caching techniques in ASP.NET Core MVC:

- **In-Memory Caching (`IMemoryCache`)**
- **Distributed Caching using Redis**

Both versions fetch posts from a public API (`https://jsonplaceholder.typicode.com/posts`) 
and cache them for 5 minutes to improve performance and reduce unnecessary API calls.

---

## 🔧 Technologies Used

- ASP.NET Core MVC (.NET 6+)
- IMemoryCache
- StackExchange.Redis
- IHttpClientFactory
- JSON Placeholder API
- Bootstrap (for simple styling)
- JavaScript (fetch API for dynamic data loading)
- LazyLoading for images

