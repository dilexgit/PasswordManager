using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Extensions.DependencyInjection;
using PasswordManagerMVC.Controllers;
using PasswordManagerMVC.Services;
using PasswordManagerMVC.Data;

namespace PasswordManagerMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Dependency Injection Configuration
            var services = new ServiceCollection();

            // Register services
            services.AddTransient<IUserService, UserService>();
            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAesCryptoService, AesCryptoService>();
            services.AddScoped<AppDbContext>();

            // Register all controllers
            var controllerTypes = typeof(MvcApplication).Assembly.GetExportedTypes()
                .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
                .Where(t => typeof(IController).IsAssignableFrom(t)
                       || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase));

            foreach (var type in controllerTypes)
            {
                services.AddTransient(type);
            }

            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

            // Set the custom dependency resolver
            DependencyResolver.SetResolver(new CustomDependencyResolver(serviceProvider));
        }
    }

    // Custom Dependency Resolver Implementation
    public class CustomDependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomDependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object GetService(Type serviceType)
        {
            return _serviceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _serviceProvider.GetServices(serviceType);
        }
    }
}