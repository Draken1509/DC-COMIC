using HUUTRUNG.DataAccess.Data;
using HUUTRUNG_WEBAPI.Mappings;
using HUUTRUNG_WEBAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// swagger
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo { Title = "HUUTRUNG API", Version = "v1" });
	options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
	{
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = JwtBearerDefaults.AuthenticationScheme
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = JwtBearerDefaults.AuthenticationScheme
				},
				Scheme = "Oauth2",
				Name = JwtBearerDefaults.AuthenticationScheme,
				In = ParameterLocation.Header
			},
			new List<string>()
		}
	});
});

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: "FrontendUI", policy =>
	{
		policy.WithOrigins(
				"https://read-dc-comic-free-fagkdhhadwfjfwcp.southeastasia-01.azurewebsites.net",
				"http://localhost:4200"
			)
			.AllowAnyHeader()
			.AllowAnyMethod();
	});
});


// add dbcontext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//dk dich vu

builder.Services.AddScoped<ISeriesAPIRepository, SQLSeriesRepository>();
builder.Services.AddScoped<IComicAPIRepository, SQLComicRepository>();
builder.Services.AddScoped<IBookmarkAPIRepository, SQLBookmarkRepository>();
builder.Services.AddScoped<IApplicationUserAPIRepository, SQLApplicationUserRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// add identity

builder.Services.AddIdentityCore<IdentityUser>()
	.AddRoles<IdentityRole>()
	.AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("HUUTRUNG.API")
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//	.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

//fix - lỗi vòng lặp trong get api
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);



builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 6;
	options.Password.RequiredUniqueChars = 1;
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
	});



var app = builder.Build();



//Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//	app.UseSwagger();
//	app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
	options.SwaggerEndpoint("/swagger/v1/swagger.json", "HUUTRUNGAPIv1");
	options.SwaggerEndpoint("/swagger/v2/swagger.json", "HUUTRUNGAPIv2");
    options.RoutePrefix =String.Empty;

});

app.UseCors("FrontendUI");
app.UseHttpsRedirection();
app.UseRouting();



//app.UseCors(options =>
//		{
//			options.AllowAnyHeader();
//			options.AllowAnyMethod();
//			options.AllowAnyOrigin();
//		});




app.UseAuthentication();
app.UseAuthorization();

//app.UseStaticFiles(new StaticFileOptions
//{
//	FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "..", "HUUTRUNGWEB/wwwroot/images")),  // ".." để đi lên một cấp
//	RequestPath = "/images"  // Đặt đường dẫn cho file tĩnh, ví dụ "/images"
//});

//app.UseStaticFiles(new StaticFileOptions
//{
//	FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images")),
//	RequestPath = "/images"
//});


app.MapControllers(); // *

app.Run();
