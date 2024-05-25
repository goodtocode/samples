using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

// https://www.infoworld.com/article/3662294/use-logging-and-di-in-minimal-apis-in-aspnet-core-6.html

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddPolicy("allowAny", o => o.AllowAnyOrigin()));
builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);
builder.Services.AddAuthorization(); // (JwtBearerDefaults.AuthenticationScheme)
                    //.AddMicrosoftIdentityWebApi(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();


var TodoStore = new Dictionary<int, TodoItem>
{
    { 1, new TodoItem() { Id = 1, Task = "Pick up groceries" } },
    { 2, new TodoItem() { Id = 2, Task = "Finish invoice report" } },
    { 3, new TodoItem() { Id = 3, Task = "Water plants" }},
};

app.MapGet("/todolist", () =>
{
    //HttpContext.ValidateAppRole("DaemonAppRole");
    return TodoStore.Values;
})
.WithName("ToDoList");


app.MapGet("/secure", [EnableCors("allowAny")] (HttpContext context) =>
{
    AuthHelper.UserHasAnyAcceptedScopes(context, new string[] { "access_as_user" });
    return "hello from secure";
}).RequireAuthorization();


app.MapGet("/insecure", [EnableCors("allowAny")] () =>
{
    return "hello from insecure";
});



//app.Urls.Add("https://localhost:44372");
app.Run();
