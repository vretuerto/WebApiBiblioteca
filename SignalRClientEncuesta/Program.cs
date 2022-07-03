
using Microsoft.AspNetCore.SignalR.Client;
using SignalRClientEncuesta;

System.Threading.Thread.Sleep(2000);

var connection = new HubConnectionBuilder().WithUrl("https://localhost:7218/encuestaHub").Build();
connection.On<List<Encuesta>>("GetResultados", (encuesta) =>
{
    foreach (var item in encuesta)
    {
        Console.WriteLine($"{item.Framework} - {item.Puntuacion} votos");
    }
});

connection.On<string>("GetOk", (mensaje) =>
{
    Console.WriteLine(mensaje);
});

connection.StartAsync().Wait();

string voto;
do
{
    Console.WriteLine("¿Cual es tu framework favorito? 1-->Angular, 2-->React, 3-->Vue");
    voto = Console.ReadLine();
    if (voto=="1" || voto == "2" || voto == "3" )
        break;
} while (true);

connection.InvokeAsync("SendResultados", Int32.Parse(voto));

Console.ReadLine();