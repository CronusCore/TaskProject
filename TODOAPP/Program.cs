using Interfaces;
using Services;


namespace TODOAPP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //agregar las connection string para utilizar en mis servicios
            //inyectamos la dependencia a todo servicio que en su constructor requiera un IOption<TipoEstpecificado>
            //No es necesario regristrar IConfiguration, ya esta disponible, se utilizara donde sea necesario

            //registramos el servicio que ocupara el o los controladores.
            builder.Services.AddScoped<IDatabaseService, DatabaseService>();
            builder.Services.AddScoped<ITaskService,TaskService>();
            builder.Services.AddScoped<CatalogService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");



            app.MapControllers();


            app.Run();
        }
    }
}