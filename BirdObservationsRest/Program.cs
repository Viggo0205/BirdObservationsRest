using BirdObservationsLib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<BirdObservationsRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                              policy =>
                              {
                                  policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                              });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();
