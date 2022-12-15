using CleanArchitecture.Endpoint;

var builder = WebApplication.CreateBuilder(args);


//Configuration
//builder.Services.AddApplicationServices();                           //Application Layer
//builder.Services.AddInfrastructureServices(builder.Configuration);   //Infrastructure Layer     
builder.AddWebConfigureService();                                    //Web Layer





//Build
var app = builder.Build();
//app.UseMiddleware<MiddlewareExceptionHandler>();
app.UseStaticFiles();
await app.AddWebAppService().ConfigureAwait(false);














