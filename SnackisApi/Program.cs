
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SnackisApi.Data;
using SnackisApi.Interfaces;
using SnackisApi.Repositories;
using SnackisApi.Services;

namespace SnackisApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var connectionString = builder.Configuration.GetConnectionString("AzureConnection") ?? throw new InvalidOperationException("Connection string 'AzureConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddAuthorization();
        builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.Configure<BearerTokenOptions>(IdentityConstants.BearerScheme, options =>
        {
            options.BearerTokenExpiration = TimeSpan.FromMinutes(30);
            options.RefreshTokenExpiration = TimeSpan.FromDays(7);
        });

        builder.Services.AddScoped<IForumCategoryRepository, ForumCategoryRepository>();
        builder.Services.AddScoped<IForumCategoryService, ForumCategoryService>();

        builder.Services.AddScoped<IForumThreadRepository, ForumThreadRepository>();
        builder.Services.AddScoped<IForumThreadService, ForumThreadService>();

        builder.Services.AddScoped<IForumPostRepository, ForumPostRepository>();
        builder.Services.AddScoped<IForumPostService, ForumPostService>();

        builder.Services.AddScoped<IForumPostReportRepository, ForumPostReportRepository>();
        builder.Services.AddScoped<IForumPostReportService, ForumPostReportService>();

        builder.Services.AddScoped<IForumMessageRepository, ForumMessageRepository>();
        builder.Services.AddScoped<IForumMessageService, ForumMessageService>();

        var app = builder.Build();

        app.MapGroup("/identity").MapIdentityApi<ApplicationUser>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
