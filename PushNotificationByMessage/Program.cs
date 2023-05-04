using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PushNotificationByMessage.Adapters.Repository;
using PushNotificationByMessage.Adapters.SqLiteAdapter.Infrastructure;
using PushNotificationByMessage.Core;
using PushNotificationByMessage.Models.Entites;
using PushNotificationByMessage.Ports.In;
using PushNotificationByMessage.Ports.Out;
using PushNotificationByMessage.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
/*builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<SensediaContext>()
    .AddDefaultTokenProviders();*/
builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();
builder.Services.AddScoped<ICRUDUserUseCase, CRUDUserUseCase>();
builder.Services.AddScoped<IRegisterUseCase, RegisterUseCase>();
builder.Services.AddScoped<IJWTCore, JWTCore>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("SensediaLocalSqlLite");
builder.Services.AddDbContext<SensediaContext>(x => x.UseSqlite(connectionString));
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

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<SensediaContext>();
await context.Database.MigrateAsync();




app.Run();
