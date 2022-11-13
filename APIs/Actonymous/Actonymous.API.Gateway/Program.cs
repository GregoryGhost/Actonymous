using Actonymous.API.Gateway;

var app = WebApplication.CreateBuilder(args).RegisterServices().Build();

app.UseWebSockets();
app.MapGraphQL();

app.Run();