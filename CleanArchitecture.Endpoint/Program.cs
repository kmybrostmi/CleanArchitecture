using CleanArchitecture.Application;
using CleanArchitecture.Endpoint;
using CleanArchitecture.Infrastructure;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);


//Configuration
builder.Services.AddInfrastructureServices(builder.Configuration);   //Infrastructure Layer     
builder.Services.AddApplicationServices(builder.Configuration);      //Application Layer
builder.AddWebConfigureService();                                    //Web Layer







//Build
var app = builder.Build();
app.UseStaticFiles();
await app.AddWebAppService().ConfigureAwait(false);














