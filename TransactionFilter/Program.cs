using TransactionFilter.business;
using TransactionFilter.infra;
using TransactionFilter.infra.configuration;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, true)
    .Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen()


.AddScoped<ITransactionHandler, TransactionHandler>()
.AddScoped<IMathHandler, MathHandler>()
.AddScoped<ICardHandler, CardHandler>()
.AddScoped<IMerchantHandler, MerchantHandler>()

.AddScoped<IDbProvider, DbProvider>()
.AddScoped<IBaseRepository, BaseRepository>()
.AddScoped<IMerchantRepository, MerchantRepository>()
.AddScoped<ICardRepository, CardRepository>()
.AddScoped<ITransactionRepository, TransactionRepository>()

.AddSingleton(config.GetSection("DataBaseConnection").Get<DatabaseConfiguration>());

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
