var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // Agregar sesión

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles(); // ?? ¡Debe ir antes de UseRouting()!
app.UseRouting();

app.UseSession(); // ?? La sesión va después de UseRouting()
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Usuario}/{action=Login}/{id?}");

app.Run();
