using Microsoft.EntityFrameworkCore;

namespace MyNamespace;

public class AppDbDataContext : DbContext
{
    public DbSet<Todo> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfiguration configuracao = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        
        var connectionString = configuracao.GetConnectionString("DefaultConnection");
        
        optionsBuilder.UseSqlServer(connectionString);
    }
}