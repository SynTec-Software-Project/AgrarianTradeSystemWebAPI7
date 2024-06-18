using AgrarianTradeSystemWebAPI.Data;
using AgrarianTradeSystemWebAPI.Services.EmailService;
using AgrarianTradeSystemWebAPI.Services.ProductServices;
using AgrarianTradeSystemWebAPI.Services.ReviewServices;
using AgrarianTradeSystemWebAPI.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AgrarianTradeSystemWebAPI.Services.AdminServices;
using AgrarianTradeSystemWebAPI.Services.NewOrderServices;
using AgrarianTradeSystemWebAPI.Services.OrderServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AgrarianTradeSystemWebAPI.Notification;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper service setup
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add connection to Azure Blob Storage
builder.Services.AddScoped(_ =>
{
    return new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage"));
});

// Register services
builder.Services.AddScoped<IFileServices, FileServices>();
builder.Services.AddScoped<IReviewServices, ReviewServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IReturnServices, ReturnServices>();
builder.Services.AddScoped<IShoppingCartServices, ShoppingCartServices>();

// Add CORS for connecting React and .NET
builder.Services.AddCors(option =>
{
    option.AddPolicy(name: "ReactJSDomain",
        policy => policy.WithOrigins("*")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()
    );
});

builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IAdminServices, AdminServices>();
builder.Services.AddScoped<INewOrderServices, NewOrderServices>();
<<<<<<< Updated upstream

//builder.Services.AddScoped<IUserServices>(sp =>
// {
//     var dbContext = sp.GetRequiredService<DataContext>();
//     var configuration = sp.GetRequiredService<IConfiguration>();
//     return new UserServices(dbContext, configuration);
// });
builder.Services.AddDbContext<DataContext>();
builder.Services.AddDbContext<DataContext>();
=======
>>>>>>> Stashed changes

builder.Services.AddDbContext<DataContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext")));

builder.Services.AddCors(option =>
{
    option.AddPolicy(name: "ReactJSDomain",
        policy => policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddAuthentication()
.AddJwtBearer("JwtBearer", jwtBearerOptions =>
{
    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey:SecretKey"]!)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
    };
});
<<<<<<< Updated upstream

//builder.Services.AddAuthentication(options =>
//{
//	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//            {
//                options.SaveToken = true;
//                options.RequireHttpsMetadata = false;
//                options.TokenValidationParameters = new TokenValidationParameters()
//                {
//                    ValidateIssuer = false,
//                    ValidateAudience = false,
//                    ValidAudience = builder.Configuration["JWTKey:ValidAudience"],
//                    ValidIssuer = builder.Configuration["JWTKey:ValidIssuer"],
//                    ClockSkew = TimeSpan.Zero,
//                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey:SecretKey"]!))
//                };
//            });
=======
// .AddJwtBearer(options =>
// {
//     options.SaveToken = true;
//     options.RequireHttpsMetadata = false;
//     options.TokenValidationParameters = new TokenValidationParameters()
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidAudience = builder.Configuration["JWTKey:ValidAudience"],
//         ValidIssuer = builder.Configuration["JWTKey:ValidIssuer"],
//         ClockSkew = TimeSpan.Zero,
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey:Secret"]!))
//     };
// });

builder.Services.AddSignalR(); // Add SignalR services
>>>>>>> Stashed changes

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
<<<<<<< Updated upstream
	app.UseSwaggerUI();
=======
    app.UseSwaggerUI();
>>>>>>> Stashed changes
}

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.UseCors("ReactJSDomain");

app.MapControllers();

// Map SignalR hub endpoint
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
