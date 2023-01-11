using edk.Kchef.Application.Features.GetProducts;
using edk.Kchef.Application.Fusc;
using edk.Kchef.IoC.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddContext(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddScoped<IMediatorUseCase>(x =>
{
    return new UseCaseMediator(builder.Services);
});

builder.Services.CreateMediatorUseCase((mediator) =>
{
    mediator.AddUseCase<GetProductsUseCase, GetProductsValidator, GetProductsPresenter, GetProductsRequest, GetProductsResponse>();
});


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
