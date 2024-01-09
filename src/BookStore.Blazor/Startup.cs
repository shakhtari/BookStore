using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using BookStore.Blazor.Shared;
using System.Globalization;



namespace BookStore.Blazor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<BookStoreBlazorModule>();
           
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

           
            app.InitializeApplication();
           
        }

    }


}