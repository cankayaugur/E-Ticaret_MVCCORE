using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnemliBookStore.Business.Abstract.Books;
using OnemliBookStore.Business.Abstract.Carts;
using OnemliBookStore.Business.Abstract.Categories;
using OnemliBookStore.Business.Concrete.Books;
using OnemliBookStore.Business.Concrete.Carts;
using OnemliBookStore.Business.Concrete.Categories;
using OnemliBookStore.Dal.Abstract;
using OnemliBookStore.Dal.Abstract.Carts;
using OnemliBookStore.Dal.Concrete.EfCore.Repositories;
using OnemliBookStore.Ui.EmailServices;
using OnemliBookStore.Ui.Identity;

namespace OnemliBookStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddControllers();
            services.AddCors();

            #region Identity Server configuration

            // Uygulamanın kullandığı db farklı, uygulamanın kullanıcılarının bilgilerinin tutulduğu db farklı olabilr.
            // Buraya migration atarken Ui katmanında olduğundan emin ol
            // More Context hatası alırsan ( add-migration -context ApplicationContext) yaz.
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer("Data Source=./;Initial Catalog=BookDB;Integrated Security=True"));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                //password
                options.Password.RequireDigit = true; // sayısal değer girmek zorunlu
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6; // minimum 6 karakter 
                options.Password.RequireNonAlphanumeric = true;

                // Lockout
                options.Lockout.MaxFailedAccessAttempts = 5; // max. 5 kere yanlış girebilir, hesabı askıya alınır.
                options.Lockout.AllowedForNewUsers = true; // lockout aktif yapıldı
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // 5 dk sonra açılır hale geldi

                // options.User.AllowedUserNameCharacters = ""; // Username içerisinde olması istenilen karakterler. Misal sadece harflerden oluşsun
                options.User.RequireUniqueEmail = true; // Her kullanıcın uniq emaili olacak.
                options.SignIn.RequireConfirmedEmail = false; // Mail onay ile hesap aktif olur
                options.SignIn.RequireConfirmedPhoneNumber = false; // Telefon no için onay.
            });

            #endregion Identity Server configuration

            #region Cookie configuration

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie oluşturuluyor. Cookie: kullanıcının tarayıcısına uygulama tarafından konulan bilgi.
                // Siteye ziyaret edildğinde tarayıcıya bazı bilgiler (login vs.) bırakır. Daha sonra ziyaret edildiğinde tanıma işlemi için kullanılır.
                // Googleda telefon aratırsak reklamlarda telefon oluşmaya başlar. Bunlar hep cookie
                // Akış: login olunduğunda sw login olunan tarayıcıya uniqe sayı bırakır. Bu sayı cookie içerisinde 
                // saklanır. Serverdada session oluşturulur. Cookie ile session birbirini tanıyan bir yapı olur.
                // Yani erişimi kısıtlayabilir ya da izin verir buda cookie-based authentication olur. Farklı authenticationlarda kullanılabilir.
                options.LoginPath = "/account/login"; // cookienin süresi bittiyse vs. login sayfasına gönderilir.
                options.LogoutPath = "/account/logout"; // çıkış yapıldığında cookie tarayıcıdan silinir. Logout sayfasına gönderilir
                options.AccessDeniedPath = "/account/accessdenied"; // kullanıcı admin sayfalarına gitmeye çalışırsa bu sayfaya gönderilir.
                options.SlidingExpiration = true; // default cookie süresi 20 dk. 20 dk içinde istek yaparsak tekrar 20dk sıfırlanır.
                options.ExpireTimeSpan = TimeSpan.FromDays(1); // 1 gün boyunca loginin aktif olur.
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true, // cookie sadece http talebiyle elde edilir. Bunun haricinde uygulama tarafında çalışan bir javascript uygulaması cookie alamasın
                    Name = ".OnemliBookStore.Security.Cookie", // cookieye verilen isim tarayıcıda göstermek için,
                    SameSite = SameSiteMode.Strict // bir kullanıcının cookiesi çalınıp istek atılırsa, session diyorki aynı laptoptan istek atılması lazım ve işlemi iptal ediyor (güvenlik)
                };

            });

            #endregion cookie confiugration

            #region Books
            services.AddScoped<IBookRepository, EfCoreBookRepository>();
            services.AddScoped<IBookService, BookManager>();
            #endregion Books

            #region Categories
            services.AddScoped<ICategoryRepository, EfCoreCategoryRepository>();
            services.AddScoped<ICategoryService, CategoryManager>();
            #endregion Categories

            #region Carts
            services.AddScoped<ICartRepository, EfCoreCartRepository>();
            services.AddScoped<ICartService, CartManager>();
            #endregion Carts

            #region Email
            services.AddScoped<IEmailSender, SmtpEmailSender>(i =>
        new SmtpEmailSender(
            Configuration["EmailSender:Host"],
            Configuration.GetValue<int>("EmailSender:Port"),
            Configuration.GetValue<bool>("EmailSender:EnableSSL"),
            Configuration["EmailSender:UserName"],
            Configuration["EmailSender:Password"]));
            #endregion Email


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles(); // wwwroot

            if (env.IsDevelopment())
            {
                //SeedDatabase.Seed();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                #region Books
                endpoints.MapControllerRoute(
                    name: "adminbooks",
                    pattern: "admin/books",
                    defaults: new { controller = "Admin", Action = "BookList" });

                endpoints.MapControllerRoute(
                 name: "adminbookscreate",
                 pattern: "admin/books/create",
                 defaults: new { controller = "Admin", Action = "BookCreate" });

                endpoints.MapControllerRoute(
                 name: "adminbooksedit",
                 pattern: "admin/books/{id?}",
                 defaults: new { controller = "Admin", Action = "BookEdit" });

                #endregion Books

                #region Carts
                endpoints.MapControllerRoute(
                    name: "cart",
                    pattern: "cart",
                    defaults: new { controller = "Cart", Action = "Index" });

                #endregion Carts

                #region Checkouts
                endpoints.MapControllerRoute(
                    name: "checkout",
                    pattern: "checkout",
                    defaults: new { controller = "Cart", Action = "checkout" });

                #endregion Checkouts

                #region Roles
                endpoints.MapControllerRoute(
                    name: "adminroles",
                    pattern: "admin/role/list",
                    defaults: new { controller = "Admin", Action = "RoleList" });

                endpoints.MapControllerRoute(
                 name: "adminrolecreate",
                 pattern: "admin/role/create",
                 defaults: new { controller = "Admin", Action = "RoleCreate" });

                endpoints.MapControllerRoute(
                 name: "adminroleedit",
                 pattern: "admin/role/{id?}",
                 defaults: new { controller = "Admin", Action = "RoleEdit" });

                #endregion Roles

                #region Users
                endpoints.MapControllerRoute(
                    name: "adminusers",
                    pattern: "admin/user/list",
                    defaults: new { controller = "Admin", Action = "UserList" });

                endpoints.MapControllerRoute(
                 name: "adminuseredit",
                 pattern: "admin/user/{id?}",
                 defaults: new { controller = "Admin", Action = "UserEdit" });

                #endregion Users

                #region Categories
                endpoints.MapControllerRoute(
                    name: "admincategories",
                    pattern: "admin/categories",
                    defaults: new { controller = "Admin", Action = "CategoryList" });

                endpoints.MapControllerRoute(
                 name: "admincategorycreate",
                 pattern: "admin/categories/create",
                 defaults: new { controller = "Admin", Action = "CategoryCreate" });

                endpoints.MapControllerRoute(
                  name: "admincategoryedit",
                  pattern: "admin/categories/{id?}",
                  defaults: new { controller = "Admin", Action = "CategoryEdit" });
                #endregion Categories

                #region Shop
                endpoints.MapControllerRoute(
                  name: "search",
                  pattern: "search",
                  defaults: new { controller = "Shop", Action = "Search" });

                endpoints.MapControllerRoute(
                   name: "bookdetails",
                   pattern: "{url}",
                   defaults: new { controller = "Shop", Action = "details" });

                endpoints.MapControllerRoute(
                    name: "books",
                    pattern: "books/{category?}",
                    defaults: new { controller = "Shop", Action = "list" });
                #endregion Shop

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
