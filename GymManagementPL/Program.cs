using GymManagmentBLL;
using GymManagmentDAL.Data.Contexts;
using GymManagmentDAL.Data.DataSeeding;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repository.Classes;
using GymManagmentDAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GymDBContext>(options =>
            {
                //options.UseSqlServer("Server = .; Database = GymManagment; Trusted_Connection = true; TrustServerCertificate = true");
                //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //builder.Services.AddScoped(typeof(IGenericRepository<>) ,typeof(GenericRepository<>));
            //builder.Services.AddScoped<IPlanRepository, PlanRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));

            var app = builder.Build();

            #region DataSeeding - Migrate DB

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dBContext = services.GetRequiredService<GymDBContext>();
                var PendingMigrations = dBContext.Database.GetPendingMigrations();
                if ( PendingMigrations?.Any()??false)
                    dBContext.Database.Migrate();

                GymDBContextDataSeeding.SeedData(dBContext);
            }

            #endregion


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
