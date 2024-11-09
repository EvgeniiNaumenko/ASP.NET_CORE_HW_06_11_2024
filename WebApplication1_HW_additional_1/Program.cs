using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Реализовать обработку POST запроса с возвращением ответа.
//Когда вы запустите приложение и сделаете POST-запрос к URL "/api/greeting" с телом запроса,
//содержащим строку (например, {"name": "John"}), вы должны 
//получить ответ с персонализированным приветствием (например, "Hello, John!").


app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    string path = request.Path.ToString().ToLower();

    if (path == "/")
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("Html/startPage.html");
    }
    else if (context.Request.Path == "/api/greeting" && request.Method == "POST")
    {
        var form = await request.ReadFormAsync();
        string userName = form["userName"];

        if (string.IsNullOrWhiteSpace(userName))
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Параметр 'name' не указан.");
            return;
        }

        var message = $@"<p style=""font-size: 32px; color: red;"">Hello, {userName}!</p>";
        response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync(message);
    }
    else
    {
        response.StatusCode = 404;
        await response.WriteAsync("Page not found");
    }
});

app.Run();
