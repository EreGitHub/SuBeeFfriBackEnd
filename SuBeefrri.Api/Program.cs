using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SuBeefrri.Contexts.DataContext;
using SuBeefrri.Services.Filters;
using SuBeefrri.Services.Helpers;
using SuBeefrri.Services.HubNotifications;
using SuBeefrri.Services.Interfaces;
using SuBeefrri.Services.Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllers(options => options.Filters.Add<GlobalExceptionFilter>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
        .GetBytes(builder.Configuration.GetSection("AppSettings:key").Value)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Autenticacion JWT (BEARER)",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Reference=new OpenApiReference
            {
                Id="Bearer",
                Type=ReferenceType.SecurityScheme
            }
        },new List<string>()
    }
    });
});

//start Inyecion
builder.Services
    .AddDbContext<SuBeefrriContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("SuBeefrri")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSignalR();

builder.Services.AddScoped<IOrdenPedidoRepository, OrdenPedidoRepository>();
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();
builder.Services.AddScoped<ISucursalRepository, SucursalRepository>();
builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IMail, Mail>();
//end inyeccion

builder.Services.AddCors(options =>
{
    options.AddPolicy("Default", conf =>
    {
        conf.AllowAnyHeader();
        conf.AllowAnyMethod();
        conf.SetIsOriginAllowed((Host) => true);
        conf.AllowCredentials();
        //conf.AllowAnyOrigin();
    });
});



var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("Default");
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<NotificacionHub>("/notify");
app.MapControllers();
app.UseStaticFiles();
app.Run();
