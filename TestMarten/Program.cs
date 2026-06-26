using FluentValidation;
using JasperFx.CodeGeneration;
using JasperFx.Events.Daemon;
using JasperFx.Events.Projections;
using Marten;
using TestMarten.Handlers.Commands.CreateMovie;
using TestMarten.Projections;
using Wolverine;
using Wolverine.FluentValidation;
using Wolverine.Marten;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var connectionString = builder.Configuration.GetConnectionString("MartenDb") ?? "";


builder.Services.AddValidatorsFromAssemblyContaining<CreateMovieCommandValidator>(
    ServiceLifetime.Singleton);

builder.UseWolverine(opts =>
{

    opts.CodeGeneration.TypeLoadMode = TypeLoadMode.Dynamic;

    opts.UseFluentValidation();

});


builder.Services.AddMarten(opts =>
{
    opts.Connection(connectionString);

    opts.DatabaseSchemaName = "movie";
     

    opts.Events.UseIdentityMapForAggregates = true;

    opts.Projections.Add<MoveListItemProjection>(ProjectionLifecycle.Async);
    opts.Projections.Add<MovieDetailProjection>(ProjectionLifecycle.Async); 


}).IntegrateWithWolverine()
.AddAsyncDaemon(DaemonMode.Solo);



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
