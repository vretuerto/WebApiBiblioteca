using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace WebApiBiblioteca.Servicios
{
    public class TareaProgramadaService : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IWebHostEnvironment env;        
        private Timer timer;

        public TareaProgramadaService(IServiceProvider serviceProvider, IWebHostEnvironment env)
        {
            this.serviceProvider = serviceProvider;
            this.env = env;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(EnviarMails, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            //Debug.WriteLine("Proceso iniciado");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Dispose();
            //Debug.WriteLine("Proceso finalizado");
            return Task.CompletedTask; // Parar la depuración desde el icono de IIS para que se ejecute el StopAsync
        }

        private async void EnviarMails(object state)
        {
            using (var scope = serviceProvider.CreateScope()) // Necesitamos delimitar un alcance scoped. Los servicios IHostedService son singleton y el ApplicationDBContext Scoped. A continuación "abrimos" un scoped con using para poder
                                                              // utilizar el ApplicationDbContext en este servicio Singleton
            {
                if (DateTime.Now.Hour == 21 && DateTime.Now.Minute == 0) { 

                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var usuarios = context.Usuarios;
                    // Solo obtenemos uno
                    await foreach (var usu in usuarios)
                    {
                        EnviarMail(usu.Email);
                    }
                }

            }
        }

        public void EnviarMail(string mail)
        {
            //Envía el Mail
        }

    }
}
