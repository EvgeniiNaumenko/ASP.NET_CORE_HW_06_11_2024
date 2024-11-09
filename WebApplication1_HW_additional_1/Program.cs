using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//����������� ��������� POST ������� � ������������ ������.
//����� �� ��������� ���������� � �������� POST-������ � URL "/api/greeting" � ����� �������,
//���������� ������ (��������, {"name": "John"}), �� ������ 
//�������� ����� � ������������������� ������������ (��������, "Hello, John!").


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
            await context.Response.WriteAsync("�������� 'name' �� ������.");
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
