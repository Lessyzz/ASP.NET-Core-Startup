var builder = WebApplication.CreateBuilder(args);

#region Database Connection
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

builder.Services.AddControllersWithViews(); // Controller

var app = builder.Build();
app.UseCors("cors"); // CORS

app.MapControllers(); // Activate API and MVC Route

if (!app.Environment.IsDevelopment()) // Redirect On Error
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); // Redirect to Https
app.UseStaticFiles(); // Activate Static Files

//app.UseAuthentication(); // Authentication Token
//app.UseAuthorization(); // Authorization

app.Run();