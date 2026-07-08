using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        "Logs/log-.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductDTOValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
var app = builder.Build();
if (!builder.Environment.IsDevelopment())
  {
      var keyVaultUri = builder.Configuration["KeyVaultUri"];
      builder.Configuration.AddAzureKeyVault(
          new Uri(keyVaultUri),
          new Azure.Identity.DefaultAzureCredential());
  }
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();