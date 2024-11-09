using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

//���������� ���� ��� �������� ����������� �������������.
//�� ������� �������� ����� ������� ����������� � ���� �������� HTML �������� � �������������� ������.
//�� ���� �������� ��������� ������ �� ����� �����������. �� ����� �����������
//������������ ������ ���, email � ����� ��������. ����� �������� �����������,
//������ ��������� � ��������� ������ � �������� ������������� �� �������� � �������������� �� �����������.

List<User> inviteUsers = new List<User>();

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
    else if (path == "/form" && request.Method == "GET")
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("Html/formPage.html");
    }
    else if (path == "/form" && request.Method == "POST")
    {
        var form = await request.ReadFormAsync();
        string userName = form["userName"];
        string userEmail = form["userEmail"];
        string userPhone = form["userPhone"];
        inviteUsers.Add(new User(userName,userEmail,userPhone));
        response.Redirect("/thankyou");
    }
    else if (path == "/thankyou")
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.WriteAsync("<h1>������� �� �����������!</h1>");
    }
    else
    {
        response.StatusCode = 404;
        await response.WriteAsync("Page not found");
    }
});

app.Run();

public class User(string? Name, string? Email, string? PhoneNumber)
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
};