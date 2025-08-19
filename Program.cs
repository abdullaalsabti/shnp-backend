using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var tokenKey =
    new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AuthorizationSettings:TokenKey").Value!));

var tokenValidationParameters = new TokenValidationParameters
{
    IssuerSigningKey = tokenKey,
    ValidateIssuer = false,
    ValidateIssuerSigningKey = false,
    ValidateAudience = false
};

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = tokenValidationParameters;
});

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("DevCors", policyBuilder =>
//     {
//         policyBuilder.WithOrigins("http://localhost:3000") // Changed to HTTP
//             .AllowAnyMethod()
//             .AllowAnyHeader();
//     });
//
//     options.AddPolicy("ProdCors", policyBuilder =>
//     {
//         policyBuilder.WithOrigins("https://yourdomain.com") // Replace with your production domain
//             .AllowAnyHeader()
//             .AllowAnyMethod();
//     });
// });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder
            .SetIsOriginAllowed(_ => true) // Allow any origin for debugging
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();


// CORS must come first, before other middleware
if (app.Environment.IsDevelopment())
{
    // app.UseCors("DevCors");
    app.UseCors();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // app.UseCors("ProdCors");
    app.UseCors();
    app.UseHttpsRedirection();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
