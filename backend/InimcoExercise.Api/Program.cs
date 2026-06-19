using InimcoExercise.Api.Repositories;
using InimcoExercise.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection: interface → implementatie
builder.Services.AddSingleton<IPersonRepository, FilePersonRepository>();
builder.Services.AddScoped<IPersonAnalysisService, PersonAnalysisService>();

// CORS — nodig zodra de Angular requests stuurt
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");
app.UseAuthorization();
app.MapControllers();
app.Run();