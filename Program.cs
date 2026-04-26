using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YachayTech_p_cov.Data;
using YachayTech_p_cov.Models;
using YachayTech_p_cov.Seguridad;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 1. HABILITA LA SESIÓN EN MEMORIA
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // La sesión dura 30 minutos
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Cuenta/Login";
        options.AccessDeniedPath = "/Cuenta/AccessDenied";
        options.Cookie.Name = "YachayTech.Auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

builder.Services.AddAuthorization();


// >>> Conexión a la Base de Datos <<<
builder.Services.AddDbContext<EvaluacionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.Use(async (context, next) =>
{
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["X-Frame-Options"] = "DENY";
    context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
    context.Response.Headers["Content-Security-Policy"] = "default-src 'self'; script-src 'self'; style-src 'self' 'unsafe-inline'; img-src 'self' data:;";
    await next();
});


// 2.
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EvaluacionContext>();
    context.Database.Migrate();

    var adminEmail = "admin@cibertec.edu.pe";
    if (!context.Usuarios.Any(u => u.Correo == adminEmail))
    {
        var (hash, salt) = ContraseñasHasheadas.HashPassword("Admin123*");
        context.Usuarios.Add(new Usuario
        {
            Nombre = "Admin",
            Apellido = "Sistema",
            Correo = adminEmail,
            Rol = "Admin",
            PasswordHash = hash,
            PasswordSalt = salt,
            FechaRegistro = DateTime.UtcNow
        });
        context.SaveChanges();
    }
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cuenta}/{action=Login}/{id?}");


app.Run();
