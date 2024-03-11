using AgrarianTradeSystemWebAPI.Data;
using AgrarianTradeSystemWebAPI.Services.ProductServices;
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//auto mapper service setup
builder.Services.AddAutoMapper(typeof(Program).Assembly);



//add connection azure blob
builder.Services.AddScoped(_ =>
{
	return new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage"));
});
//register IFileService
builder.Services.AddScoped<IFileServices, FileServices>();

//add cors for connect react and .net
builder.Services.AddCors(option =>
{
	option.AddPolicy(name: "ReactJSDomain",
		policy => policy.WithOrigins("http://localhost:5173")
		.AllowAnyHeader()
		.AllowAnyMethod());

});

builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddDbContext<DataContext>();

builder.Services.AddDbContext<DataContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext")));

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
