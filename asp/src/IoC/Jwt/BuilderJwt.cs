using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Domain.Services;

namespace IoC.Jwt;

public static class BuilderJwt
{
     public static WebApplicationBuilder AddJwtConf(this WebApplicationBuilder builder)
     {
          builder.Services.AddAuthentication(options => 
               options.DefaultAuthenticateScheme = 
               options.DefaultChallengeScheme = 
               options.DefaultForbidScheme = 
               options.DefaultSignInScheme =
               options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme
               ).AddJwtBearer(options => 
               {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                         ValidateIssuer = true,
                         ValidIssuer = builder.Configuration["JWT:Issuer"],
                         ValidateAudience = true,
                         ValidAudience = builder.Configuration["JWT:Audience"],
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(
                              System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]!)
                         ),
                    };

                    options.Events = new JwtBearerEvents
                    {
                         OnChallenge = context =>
                         {
                              context.HandleResponse();
                              var traceId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier; 

                              var problemDetails = new ProblemDetails
                              {
                                   Type = "https://tools.ietf.org/html/rfc9110#section-15.5.2",
                                   Title = "Unauthorized",
                                   Status = StatusCodes.Status401Unauthorized,
                                   Extensions = { { "traceId", traceId } }
                              };

                              context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                              context.Response.ContentType = "application/json";
                              return context.Response.WriteAsJsonAsync(problemDetails);
                         }
                    };
          });

        return builder;
     }
}