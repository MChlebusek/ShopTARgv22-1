using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shop.ApplicationServices.Services;
using Shop.Core.ServiceInterface;

namespace Shop.SpaceshipTest
{
    public abstract class TestBase
    {
        protected IServiceProvider ServiceProvider { get; }

        protected TestBase() 
        {
            var services = new ServiceCollection();
            SetupServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        public void Dispose()
        {

        }

        protected T Svc<T>()
        {
            return ServiceProvider.GetService<T>();
        }

        protected T Macro<T>() where T : IMacros
        {
            return ServiceProvider.GetService<T>();
        }


        public virtual void SetupServices(IServiceCollection services)
        {
            services.AddScoped<ISpaceshipServices, SpaceshipServices>();
            services.AddDbContext<ShopContext>(x =>
            {
                x.UseInMemoryDatabase("TEST");
                x.ConfigureWarnings(e => e.Ignore(InMemoryEventId.TransactionIgnoreWarning)
            });

            RegisterMacros(services);
        }

        private void RegisterMacros(IServiceCollection services)
        {
            var macroBaseType = typeof(IMacros);

            var macros = macroBaseType.Assembly.GetTypes()
                .Where(x=> macroBaseType.IsAssignableFrom(x)); 

            foreach (var macro in macros)
            {
                services.AddSingleton(macro);
            }
        }


    }


}
