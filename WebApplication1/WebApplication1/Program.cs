var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.MapGet("/", () => Project1.HelloWorld.Salutations);

app.Run();