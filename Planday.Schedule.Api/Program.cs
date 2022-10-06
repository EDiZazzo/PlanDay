using Planday.Schedule;
using Planday.Schedule.Infrastructure.Providers;
using Planday.Schedule.Infrastructure.Providers.Interfaces;
using Planday.Schedule.Infrastructure.Queries;
using Planday.Schedule.Infrastructure.Services;
using Planday.Schedule.Infrastructure.Services.Commands;
using Planday.Schedule.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConnectionStringProvider>(new ConnectionStringProvider(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddSingleton<IExternalApiStringProvider>(new ExternalApiStringProvider(builder.Configuration.GetConnectionString("externalEmployeeApi"), builder.Configuration.GetValue<string>("ApiKey")));

builder.Services.AddScoped<IGetAllShiftsQuery, GetAllShiftsQuery>();
builder.Services.AddScoped<IGetEntityByIdQuery<ShiftId, Shift>, GetShiftByIdQuery>();
builder.Services.AddScoped<IGetEntityByIdQuery<EmployeeId, Employee>, GetEmployeeByIdQuery>();
builder.Services.AddScoped<IGetEntityByIdQuery<EmployeeId, IList<Shift>>, GetShiftByEmployeeIdQuery>();
builder.Services.AddScoped<ICudShiftQuery<UpdateShiftCommand>, UpdateShiftQuery>();
builder.Services.AddScoped<ICudShiftQuery<InsertShiftCommand>, InsertNewShiftQuery>();

builder.Services.AddScoped<INewOpenShiftService, InsertNewShiftService>();
builder.Services.AddScoped<IAssignShiftService, AssignShiftService>();
builder.Services.AddScoped<IGetEmployeeByExternalApiService, GetEmployeeByExternalApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
