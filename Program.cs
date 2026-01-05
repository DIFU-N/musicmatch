using Odyssey.MusicMatcher;
using SpotifyWeb;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<SpotifyService>();

builder.Services.AddGraphQLServer().AddQueryType<Query>()
.AddMutationType<Mutation>()
.RegisterService<SpotifyService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://studio.apollographql.com").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// app.MapGet("/", () => "Hello World!");
app.MapGraphQL();
app.UseCors();

app.Run();
