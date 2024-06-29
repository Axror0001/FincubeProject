using ProjectBlazor.StudentData;
using ProjectBlazor.TeacherData;
using ProjectBlazor.TeacherStudentData;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ProjectBlazor.StudentData;
using ProjectBlazor.TeacherData;
using ProjectBlazor.TeacherStudentData;
using Syncfusion.Blazor;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<IStudent, StudentDto>();
builder.Services.AddTransient<ITeacher, TeacherDto>();
builder.Services.AddTransient<ITeacherStudent, TeacherStudentDto>();
builder.Services.AddSyncfusionBlazor();
//builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddScoped(sp => new HttpClient() { BaseAddress = new Uri("") });
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

