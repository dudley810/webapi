using Microsoft.AspNetCore.DataProtection;
using Microsoft.OpenApi.Models;

var CorsPolicy = "CorsPolicy";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicy,
    builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"/share/dotnetwebapi/keys"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger(c =>
{
    c.PreSerializeFilters.Add((swagger, httpReq) =>
    {
        var myurl = httpReq.Host.Host;
        swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = "https://" + myurl } };
    });
});
app.UseSwaggerUI(c =>
{
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});

app.UseHsts();
app.UseHttpsRedirection();
app.UseCors(CorsPolicy);
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();
app.Run();