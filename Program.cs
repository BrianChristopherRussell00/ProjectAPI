
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectAPI.Data;
using ProjectAPI.Mappings;
using ProjectAPI.Repository;
using System.Text;

namespace ProjectAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<BrianRussellDbContext>(options =>  
            options.UseSqlServer(builder.Configuration.GetConnectionString("BRConnectionString")));

            builder.Services.AddDbContext<BRAuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BRAuthConnectionString")));
            builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
            builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

            builder.Services.AddIdentityCore<IdentityUser>()    
                .AddRoles<IdentityRole>()   
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("BRConnection") 
                .AddEntityFrameworkStores<BRAuthDbContext>()    
                .AddDefaultTokenProviders();


            builder.Services.Configure<IdentityOptions>(options =>
                {
                    options.Password.RequireDigit= false;   
                    options.Password.RequireLowercase= false;
                    options.Password.RequireNonAlphanumeric= false; 
                    options.Password.RequireUppercase= false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;

                });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters    
            {   
            ValidateIssuer = true,  
            ValidateAudience= true, 
            ValidateLifetime =true, 
            ValidateIssuerSigningKey =true, 
            ValidIssuer = builder.Configuration["Jwt:Issuer"],  
            ValidAudience = builder.Configuration["Jwt:Audience"],  
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            
            
            });
            
            
            
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
