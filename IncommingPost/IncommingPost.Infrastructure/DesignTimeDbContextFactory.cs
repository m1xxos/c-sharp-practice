using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace IncommingPost.Infrastructure;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Получение директории проекта
        string projectDirectory = Directory.GetCurrentDirectory();
        
        // Поиск директории основного проекта
        string webProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "IncommingPost");
        if (!Directory.Exists(webProjectPath))
        {
            webProjectPath = Directory.GetCurrentDirectory();
        }
        
        // Чтение конфигурации из appsettings.json
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(webProjectPath)
            .AddJsonFile("appsettings.json")
            .Build();
            
        // Получение строки подключения
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        // Создание опций для DbContext
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}