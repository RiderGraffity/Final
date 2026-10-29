using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

var distPath = Path.GetFullPath(
    Path.Combine(builder.Environment.ContentRootPath, "..", "service-requests", "dist"));

if (!Directory.Exists(distPath))
{
    throw new DirectoryNotFoundException(
        $"Папку dist не знайдено: {distPath}. Спочатку виконайте 'npm run build' у папці service-requests.");
}

var distProvider = new PhysicalFileProvider(distPath);

var app = builder.Build();

app.UseDefaultFiles(new DefaultFilesOptions { FileProvider = distProvider });
app.UseStaticFiles(new StaticFileOptions { FileProvider = distProvider });
app.MapFallbackToFile("index.html", new StaticFileOptions { FileProvider = distProvider });

app.Run();

