using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using MyNamespace;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbDataContext>();
var app = builder.Build();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{actions=Index}/{id?}"
    );

app.Run();
