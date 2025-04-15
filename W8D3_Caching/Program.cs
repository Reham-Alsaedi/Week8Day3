using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Register IMemoryCache
builder.Services.AddMemoryCache();

// Add services to the container.
builder.Services.AddHttpClient();

// Register HttpClient and Redis
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect("localhost:6379"));

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Post}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "postMemoryCaching",
    pattern: "{controller=PostMemoryCaching}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

