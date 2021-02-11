using Contacts.Common.Interfaces;
using Contacts.Service.Configuration;
using Contacts.Service.Interfaces;
using Contacts.Service.SQLite;
using Contacts.Service.SQLite.DataAdapters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Service
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
            //Normally this is where I would add JWT for authentication, but I wasn't sure how to handle this. I 
            //understand that the purpose of this assessment is to showcase my skill, but it honestly feels like 
            //adding authentication to something like this would be over-engineering. I've decided to not add it in
            //as it feels like a better decision architechture-wise. If the requirements had this in, I would add 
            //in the jwt, with a EXP for a couple of minutes, refresh that token on every connection that is re-made
            //and add roles (if we perhaps had an admin/user role setup. The token wouldnt be encrypted, but simply 
            //validated as the payload of the token wouldnt carry any information that could be abused. 
            services.AddControllers();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.Configure<DatabaseConfiguration>(Configuration.GetSection(nameof(DatabaseConfiguration)));
            services.AddScoped(cfg => cfg.GetService<IOptionsSnapshot<DatabaseConfiguration>>().Value);

            services.AddScoped(typeof(IDatabaseConnection), typeof(SQLiteDbConnection));
            services.AddScoped(typeof(IDbPreDeploy), typeof(DbPreDeploy));

            services.AddScoped(typeof(IEntryDataAdapter), typeof(EntryDataAdapter));
            services.AddScoped(typeof(IPhoneBookDataAdapter), typeof(PhoneBookDataAdapter));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDatabaseConnection connection, IDbPreDeploy dbPreDeploy)
        {
            dbPreDeploy.Execute(connection);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("MyPolicy");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
