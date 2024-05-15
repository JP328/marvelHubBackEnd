using marvelHub.Data;
using marvelHub.Validator;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using marvelHub.Model;
using marvelHub.Service;
using marvelHub.Service.Implements;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {
    options.AddPolicy(name: "MyPolicy",
        policy =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

var connectionString = builder.Configuration.
        GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
);

builder.Services.AddControllers()
       .AddJsonOptions(options =>
       {
           options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
       });


builder.Services.AddTransient<IValidator<Post>, PostValidator>();
builder.Services.AddTransient<IValidator<Theme>, ThemeValidator>();
builder.Services.AddTransient<IValidator<Comment>, CommentValidator>();
builder.Services.AddTransient<IValidator<User>, UserValidator>();

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();

var app = builder.Build();

using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
