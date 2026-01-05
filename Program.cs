using Odyssey.MusicMatcher;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer().AddQueryType<Query>().AddType<Playlist>();

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
