using Coffee.Areas.Identity;
using Coffee.Data;
using Coffee.Data.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
/*builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));*/
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source = appDB.db"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDbContext<DBCoffeeContext>(options =>
{
    options.UseSqlite("Data Source = Coffee.db");
});
/*builder.Services.AddDbContextPool<DBCoffeeContext>(options =>
{
    options.UseSqlServer(connectionString);
});*/
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddScoped<SQLRpostitory>();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddAuthentication().AddVkontakte(caption =>
{
    caption.ClientId = builder.Configuration["Authentication:VK:AppId"];
    caption.ClientSecret = builder.Configuration["Authentication:VK:AppSecret"];
    caption.CallbackPath = new PathString("/sing-vk-token");
    caption.AuthorizationEndpoint = "https://oauth.vk.com/authorize";
    caption.TokenEndpoint = "https://oauth.vk.com/access_token";
    caption.Scope.Add("email");
    caption.Fields.Add("id");
    caption.Fields.Add("first_name");
    caption.Fields.Add("last_name");
    caption.SaveTokens = true;
    caption.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
    caption.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
    caption.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "first_name");
    caption.ClaimActions.MapJsonKey(ClaimTypes.Surname, "last_name");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
//app.MapFallbackToPage("/coffee-list");

app.Run();
