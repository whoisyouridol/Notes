using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Areas.Identity.Data;
using Notes.Data;

[assembly: HostingStartup(typeof(Notes.Areas.Identity.IdentityHostingStartup))]
namespace Notes.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<AuthDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AuthDbContextConnection")));

                //services.AddIdentityCore<ApplicationUser>(options =>
                //{
                //    //options.User.AllowedUserNameCharacters = "f";
                //    options.SignIn.RequireConfirmedAccount = false;
                //}).AddEntityFrameworkStores<AuthDbContext>();
                //without this guy user can`t sign in account because by default we need account confirmation
                services.AddDefaultIdentity<ApplicationUser>(options =>
                    {
                        options.User.AllowedUserNameCharacters =
                            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
                        options.SignIn.RequireConfirmedAccount = false;
                    })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<AuthDbContext>();
                // services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
                //we need this line to add to the identity 
                // services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();
            });
        }
    }
}