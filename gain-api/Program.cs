using core.Context;
using core.Services.Auditoria;
using core.Services.Base;
using core.Services.Hallazgo;
using core.Services.Responsable;
using FluentValidation;
using gain_api.Filters;
using gain_api.Mappers;
using gain_api.Validators.Auditoria;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

#region CONTEXT

var connectionString = builder.Configuration.GetConnectionString("GainConnectionString");
builder.Services.AddDbContext<GainDbContext>(options => options.UseSqlServer(connectionString));

#endregion

#region VALIDATORS

builder.Services.AddValidatorsFromAssemblyContaining<AuditoriaDtoValidator>();
builder.Services.AddScoped(typeof(ValidationFilter<>));
#endregion

#region SERVICES

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = 
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddScoped(typeof(IBaseService<>),  typeof(BaseService<>));
builder.Services.AddScoped<IHallazgoService, HallazgoService>();
builder.Services.AddScoped<IAuditoriaService, AuditoriaService>();
builder.Services.AddScoped<IResponsableService, ResponsableService>();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
#endregion

#region MAPPER

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

#endregion

// Add services to the container.
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
app.UseRouting();
app.MapControllers();
app.Run();