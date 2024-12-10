using Unit_Trust_Backend.Data;
using Unit_Trust_Backend.Services;
using Microsoft.EntityFrameworkCore;
using Unit_Trust_Backend.Investor.Interfaces;
using Unit_Trust_Backend.Staff.Interfaces;
using Unit_Trust_Backend.Staff.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// CORS policy setup to allow only localhost:3000 (React app)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000") // React app's URL
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Add DbContext using MySQL
builder.Services.AddDbContext<MyDatabase>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))
    )
);

// Register IUserService and UserService for Dependency Injection
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IInvestorService, InvestorService>();
builder.Services.AddScoped<IDepositService, DepositService>();
builder.Services.AddScoped<IAccountsService, AccountsService>();
builder.Services.AddScoped<IStaffService, StaffService>();



// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(); // Enable CORS with the default policy

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers(); // Map API controllers to routes

app.Run(); // Run the application
