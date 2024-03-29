﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Senai.InLock.WebApi {
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

            }).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(x =>
                x.SwaggerDoc("v1",new Swashbuckle.AspNetCore.Swagger.Info{
                    Title = "Senai.InLock.WebApi",Version="v1"
                })
            );


            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }

                ).AddJwtBearer("JwtBearer" , options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuer = true ,
                        ValidateAudience = true ,
                        ValidateLifetime = true ,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("inlock-chave-autenticacao")) ,
                        ClockSkew = TimeSpan.FromHours(2),
                        ValidIssuer = "InLock.WebApi" ,
                        ValidAudience = "InLock.WebApi"
                    };
                });

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy" , builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app , IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseCors("CorsPolicy");
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(x =>
                x.SwaggerEndpoint("/swagger/v1/swagger.json","Senai.InLock.WebApi")
            );
        }
    }
}
