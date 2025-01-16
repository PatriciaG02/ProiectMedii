using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProiectMedii.Data;
using ProiectMedii.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProiectMediiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProiectMediiContext") ?? throw new InvalidOperationException("Connection string 'ProiectMediiContext' not found.")));

// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//var jwtSettings = builder.Configuration.GetSection("JwtSettings");
//var secretKey = jwtSettings.GetValue<string>("SecretKey");

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
//        ValidAudience = jwtSettings.GetValue<string>("Audience"),
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
//    };
//});


//builder.Services.AddSwaggerGen(options =>
//{
//    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
//        Description = "JWT Authorization header using the Bearer scheme."
//    });
//    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
//    {
//        {
//            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//            {
//                Reference = new Microsoft.OpenApi.Models.OpenApiReference
//                {
//                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            new string[] {}
//        }
//    });
//});



//// Configurează pipeline-ul HTTP
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
