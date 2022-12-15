using CleanArchitecture.Endpoint;
using CleanArchitecture.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


//Configuration
builder.Services.AddInfrastructureServices(builder.Configuration);   //Infrastructure Layer     
builder.AddWebConfigureService();                                    //Web Layer





//Build
var app = builder.Build();
app.UseStaticFiles();
await app.AddWebAppService().ConfigureAwait(false);














