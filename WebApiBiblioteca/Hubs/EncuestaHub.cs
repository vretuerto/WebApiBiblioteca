using Microsoft.AspNetCore.SignalR;

namespace WebApiBiblioteca.Hubs
{
    public class EncuestaHub : Hub
    {
        public static List<Encuesta> LstEncuesta { get; set; } = new List<Encuesta>()
        {
            new Encuesta()
            {
                Id =1,
                Framework ="Angular",
                Puntuacion =0
            },
            new Encuesta()
            {
                Id=2,
                Framework ="React",
                Puntuacion =0
            },
            new Encuesta()
            {
                Id=3,
                Framework ="Vue",
                Puntuacion =0
            }
        };

        public async Task SendResultados(int voto)
        {
            LstEncuesta.FirstOrDefault(x => x.Id == voto).Puntuacion++;
            await Clients.Client(Context.ConnectionId).SendAsync("GetOk", "Gracias por tu voto");
            await Clients.All.SendAsync("GetResultados", LstEncuesta);
        }
    }
}
