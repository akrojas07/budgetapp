using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetManagement.Persistence.Repositories;
using BudgetManagement.Persistence.Repositories.Interfaces;
using BudgetManagement.Domain.Services.Interfaces;
using BudgetManagement.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BudgetManagement.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IBudgetExpensesRepository, BudgetExpensesRepository>();
            services.AddSingleton<IBudgetIncomeRepository, BudgetIncomeRepository>();
            services.AddSingleton<IBudgetSavingsRepository, BudgetSavingRepository>();


            services.AddTransient<IBudgetIncomeServices, BudgetIncomeServices>();
            services.AddTransient<IBudgetExpensesServices, BudgetExpensesServices>();
            services.AddTransient<IBudgetSavingsServices, BudgetSavingsServices>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
