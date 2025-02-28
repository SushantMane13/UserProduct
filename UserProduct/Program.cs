
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserProduct.Managers.Implmentattion.ProductClasses;
using UserProduct.Managers.Implmentattion.UserClasses;
using UserProduct.Managers.Interface.ProductInterface;
using UserProduct.Managers.Interface.UserInterfaces;
using UserProduct.Services.Implementation.ProductClasses;
using UserProduct.Services.Implementation.UserClasses;
using UserProduct.Services.Interface.ProductInterface;
using UserProduct.Services.Interface.UserInterfaces;
using UserProduct.Services.Models;
using UserProduct.Services.Models.Entity;

namespace UserProduct
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IUserService,UserService>();
            builder.Services.AddScoped<IUserManager, UserManager>();

            builder.Services.AddScoped<IUserAddressService, UserAddressService>();
            builder.Services.AddScoped<IUserAddressManager, UserAddressManager>();

            builder.Services.AddScoped<IUserEmailService, UserEmailService>();
            builder.Services.AddScoped<IUserEmailManager, UserEmailManager>();

            builder.Services.AddScoped<IUserPersonalService, UserPersonalService>();
            builder.Services.AddScoped<IUserPersonalManager, UserPersonalManager>();

            builder.Services.AddScoped<IUserPhoneService, UserPhoneService>();
            builder.Services.AddScoped<IUserPhoneManager, UserPhoneManager>();

            builder.Services.AddScoped<IProductService, ProductServices>();
            builder.Services.AddScoped<IProductManager, ProductManager>();

            builder.Services.AddScoped<IProductTypeService, ProductTypeService>();
            builder.Services.AddScoped<IProductTypeManager, ProductTypeManager>();

            builder.Services.AddScoped<IPaymentModeService, PaymentModeService>();
            builder.Services.AddScoped<IPaymentModeManager, PaymentModeManager>();

            builder.Services.AddScoped<IDelivaryTypeServices, DelivaryTypeService>();
            builder.Services.AddScoped<IDelivaryTypeManager, DelivaryTypeManager>();

            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderManager, OrderManager>();

            builder.Services.AddScoped<IOrderStatusService, OrderStatusService>();
            builder.Services.AddScoped<IOrderStatusManager, OrderStatusManager>();


            builder.Services.AddControllers();

            builder.Services.AddDbContext<UserDbContext>();


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

            builder.Services.AddCors(options => options.AddPolicy(name: "FrontendUI",
                policy =>
                {
                    policy.WithOrigins("http://localhost:5174").AllowAnyMethod().AllowAnyHeader();
                    policy.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                    policy.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader();
                }));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("FrontendUI");
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
