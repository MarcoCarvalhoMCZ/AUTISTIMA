using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using AUTistima.Data;
using AUTistima.Models;
using AUTistima.Models.Enums;
using AUTistima.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Entity Framework com SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning)));

// Configuração do ASP.NET Core Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Configurações de senha mais flexíveis para acessibilidade
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    
    // Configurações de usuário
    options.User.RequireUniqueEmail = true;
    
    // Configurações de login
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Configuração de cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.SlidingExpiration = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registrar serviços personalizados
builder.Services.AddSignalR();
builder.Services.AddSingleton<AUTistima.Services.SentimentService>();
builder.Services.AddAIServices();
builder.Services.AddScoped<IPushNotificationService, PushNotificationService>();
builder.Services.AddScoped<IPanicService, PanicService>();
builder.Services.AddScoped<IActivityTrackingService, ActivityTrackingService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();

// Adicionar sessão para rastreamento
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapStaticAssets();
app.MapHub<AUTistima.Hubs.ChatHub>("/chatHub");

// Rota para área administrativa
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Criar banco de dados e aplicar migrations automaticamente em desenvolvimento
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro ao aplicar migrations do banco de dados.");
    }
    
    // Criar usuário administrador padrão
    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var adminEmail = "lorena@autistima.app.br";
        
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                NomeCompleto = "Lorena - Administradora",
                TipoPerfil = TipoPerfil.Administrador,
                EmailConfirmed = true,
                Ativo = true,
                DataCadastro = DateTime.UtcNow
            };
            
            var result = await userManager.CreateAsync(adminUser, "Lorena@2025");
            if (result.Succeeded)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Usuário administrador criado: {Email}", adminEmail);
            }
        }

        // Criar usuário administrador adicional (Solicitado)
        var adminEmail2 = "diretoria@sosdados.com.br";
        var adminUser2 = await userManager.FindByEmailAsync(adminEmail2);
        if (adminUser2 == null)
        {
            adminUser2 = new ApplicationUser
            {
                UserName = adminEmail2,
                Email = adminEmail2,
                NomeCompleto = "Diretoria SOS Dados",
                TipoPerfil = TipoPerfil.Administrador,
                EmailConfirmed = true,
                Ativo = true,
                DataCadastro = DateTime.UtcNow
            };
            
            var result2 = await userManager.CreateAsync(adminUser2, "Eamf5644");
            if (result2.Succeeded)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Usuário administrador criado: {Email}", adminEmail2);
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro ao criar usuário administrador.");
    }
}

app.Run();
