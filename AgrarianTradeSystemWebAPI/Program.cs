using AgrarianTradeSystemWebAPI.Data;
using AgrarianTradeSystemWebAPI.Services.EmailService;
using AgrarianTradeSystemWebAPI.Services.ProductServices;
using AgrarianTradeSystemWebAPI.Services.UserServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUserServices>(sp =>
 {
     var dbContext = sp.GetRequiredService<DataContext>();
     var configuration = sp.GetRequiredService<IConfiguration>();
     return new UserServices(dbContext, configuration);
 });
builder.Services.AddDbContext<DataContext>();

builder.Services.AddDbContext<DataContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext")));

builder.Services.AddCors(option =>
{
	option.AddPolicy(name: "ReactJSDomain",
        policy => policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("ReactJSDomain");

app.UseAuthorization();

app.MapControllers();

app.Run();
