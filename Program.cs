var builder = WebApplication.CreateBuilder(args);

// L�gg till MVC
builder.Services.AddControllersWithViews();

// St�d f�r sessions
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Konfigurera HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Aktivera statiska filer
app.UseStaticFiles();

// Aktivera routing
app.UseRouting();

app.UseAuthorization();

// Grundrouting (styr mot login-sidan)
app.MapControllerRoute
    (
    name: "default",
    pattern: "{controller=Home}/{action=Login}"
    );

app.MapBlazorHub();

app.UseSession();

app.Run();