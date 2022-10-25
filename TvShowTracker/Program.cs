using TvShowTracker.Config.LoadDataApi;
using TvShowTracker.Startup;
using Hangfire;
var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterServices(builder.Configuration);
var app = builder.Build();
app.UseHangfireServer();
app.UseHangfireDashboard();
if (!app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TvShowTracker v1"));
    RecurringJob.AddOrUpdate<ApiServiceTMDB>("ApiServiceTMDB", (Api) => Api.LoapApi(), Cron.Weekly);
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.UseResponseCaching();
app.Use(async (context, next) =>
{
    context.Response.GetTypedHeaders().CacheControl =
    new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
    {
        Public = true,
        MaxAge = TimeSpan.FromSeconds(10)
    };
    context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
        new string[] { "Accept-Encoding" };
    await next();
});
app.Run();
public partial class Program { }