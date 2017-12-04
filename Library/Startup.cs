
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LibraryData;
using LibraryService;
using Microsoft.EntityFrameworkCore;

namespace Library
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddMvc();
            services.AddSingleton(Configuration);
            services.AddScoped<ILibraryCard, LibraryCardService>(); // so that Bookservice is injected into controllers and other components that request IBook
            services.AddScoped<ILibraryBranch, LibraryBranchService>(); // so that Bookservice is injected into controllers and other components that request IBook
            services.AddScoped<IPatron, PatronService>(); // so that Bookservice is injected into controllers and other components that request IBook
            services.AddScoped<ICheckout, CheckoutService>(); // so that Bookservice is injected into controllers and other components that request IBook
            services.AddScoped<ILibraryAsset, LibraryAssetService>(); // so that Bookservice is injected into controllers and other components that request IBook
            services.AddScoped<IBook, BookService>(); // so that Bookservice is injected into controllers and other components that request IBook
            services.AddScoped<IVideo, VideoService>(); // so that Bookservice is injected into controllers and other components that request IBook
            services.AddScoped<IStatus, StatusService>(); // so that Bookservice is injected into controllers and other components that request IBook

            // configure ef and dbcontext.
            // ef can now work with other databases, including non-relational
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LibraryConnection")));

            // Now we can use EF to generate our database in a two-step process.
            // Write migration code we can execute to create a database and a schema.
            // ef can generate migration code by looking at a database and seeing its
            // current state.

            // Install NuGet package EntityFrameworkCore.Tools
            // in the package manager console.
            // console command - add-migration
            // console command - update-databse
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
