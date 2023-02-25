using edk.Kchef.IoC.Extensions;


var builder = WebApplication.CreateBuilder(args);

// IoC Layer
builder.Services.AddContext(builder.Configuration.GetConnectionString("DefaultConnection"))
                .AddRepository()
                .AddDomainServices()
                .AddFusc();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
