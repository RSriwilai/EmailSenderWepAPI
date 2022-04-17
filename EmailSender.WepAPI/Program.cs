using EmailSender.DataAccess.DBContext;
using EmailSender.Interface;
using EmailSender.Model;
using EmailSender.Service;
using EmailSenderBusinessLogic.Interfaces;
using EmailSenderBusinessLogic.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddSingleton<IEmailSenderService, EmailSenderService>();
builder.Services.AddScoped<ICampaignEmailSenderService, CampaignEmailSenderService>();

builder.Services.AddDbContext<EmailSenderDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Email Sender Api",
        Version = "v1",
        Description = "Sending Email with Mimekit and Mailkit smtp",
        Contact = new OpenApiContact
        {
            Name = "Rattanachai Sriwilai",
            Email = "rattanachai.sriwilai@iths.se",
            Url = new Uri("https://github.com/RSriwilai"),
        },
    });

});


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
