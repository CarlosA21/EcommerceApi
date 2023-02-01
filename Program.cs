using BusinessLogic.Logic;
using EcommerceAPI.Data;
using EcommerceAPI.Dtos;
using EcommerceAPI.Interfaces;
using EcommerceAPI.Logic;
using EcommerceAPI.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Stripe;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

void Startup(IConfiguration configuration)
{
    configuration = configuration;
}


builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
}));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<ITokenService, EcommerceAPI.Logic.TokenService>();
    builder.Services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
    builder.Services.AddScoped<IPaymentService, PaymentService>();

    //User model, services for migration
    var a = builder.Services.AddIdentityCore<User>();
    a = new IdentityBuilder(a.UserType, a.Services);

    a.AddRoles<IdentityRole>();

    a.AddEntityFrameworkStores<SecurityDbContext>();
    a.AddSignInManager<SignInManager<User>>();

    //jwt

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"])),
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidateIssuer = true,
            ValidateAudience = false
        };
    });

    //tproducts
    builder.Services.AddAutoMapper(typeof(MappingProfiles));
    builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));

    //This is generic for user 
    builder.Services.AddScoped(typeof(IGenericSecurityRepository<>), (typeof(GenericSecurityRepository<>)));



    builder.Services.AddDbContext<MarketDbContext>(opt =>
    {
        opt.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
                );
    });

    // Connect db with the security(user)
    builder.Services.AddDbContext<SecurityDbContext>(x =>
    {
        x.UseSqlServer(builder.Configuration.GetConnectionString("IdentitySecurity"));
    });

    // connection for redis
    builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
    {
        var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);

        return ConnectionMultiplexer.Connect(configuration);
    });

    // i use this for the time a new record is being inserted - roles
    builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

    // this is for products
    builder.Services.AddTransient<IProductRepository, ProductRepository>();
    builder.Services.AddControllers();

    //This is for ShoppingCart
    builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}
app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
