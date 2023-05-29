using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Chat.Api.ActivityModule.Middlewares;
using Chat.Api.CoreModule.Services;

namespace Chat.Api
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }
        private IWebHostEnvironment WebHostEnvironment { get; set; }
        
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = Configuration["TokenConfig:Issuer"],
                    ValidAudience = Configuration["TokenConfig:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(Configuration["TokenConfig:SecretKey"]))
                };
            });
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            services.AddCors(options => {
                options.AddDefaultPolicy(builder => {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            
            services.AddTransient<LastSeenMiddleware>();
            DIService.Instance.Initialize(services.BuildServiceProvider(), "Chat");
        }

        public void Configure(IApplicationBuilder app)
        {
            if (WebHostEnvironment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            app.UseMiddleware<LastSeenMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}