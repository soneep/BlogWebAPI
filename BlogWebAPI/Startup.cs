using Blog.Business;
using Blog.Business.Core;
using Blog.Entities;
using BlogWebAPI.ErrorHandler;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Core;
using System.Text;

public class Startup
{
    private IConfiguration configuration { get; set; }
    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

        // configure strongly typed settings objects
        var jwtSection = configuration.GetSection("JwtBearerTokenSettings");
        services.Configure<JwtBearerTokenSettings>(jwtSection);
        var jwtBearerTokenSettings = jwtSection.Get<JwtBearerTokenSettings>();
        var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = jwtBearerTokenSettings.Issuer,
                ValidAudience = jwtBearerTokenSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
            };
        });

        services.AddRazorPages();
        services.AddControllers();
        //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        this.RegisterServices(services);
    }

    private void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<ILoggerFactory, LoggerFactory>();
        services.AddTransient<IBlogManager, BlogManager>();

        services.AddScoped<IBlogStore, BlogStore>();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        using (var scope = app.Services.CreateScope())

        {

            var loggerFactory = scope.ServiceProvider.GetRequiredService(typeof(ILoggerFactory));
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}