using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Database Connection
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

#region JWT Tokens

var key = Encoding.ASCII.GetBytes("YourVeryLongAndSecureSecretKeyThatIsAtLeast32Characters"); // Secret key

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

#endregion

builder.Services.AddControllersWithViews(); // Controller

// Swagger stuffs
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("cors"); // CORS

app.MapControllers(); // Activate API and MVC Route

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Swagger UI'yi etkinleþtirin
}

if (!app.Environment.IsDevelopment()) // Redirect On Error
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseAntiforgery(); // authentication token
app.UseAuthorization(); // Authorization

app.UseHttpsRedirection(); // Redirect to Https
app.UseStaticFiles(); // Activate Static Files

app.Run();