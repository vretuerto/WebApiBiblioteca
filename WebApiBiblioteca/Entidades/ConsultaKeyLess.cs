using Microsoft.EntityFrameworkCore;

namespace WebApiBiblioteca.Entidades
{
    [Keyless] 
    public class ConsultaKeyLess
    {
        public int EditorialId { get; set; }
        public string NombreEditorial { get; set; }
        public int LibroId { get; set; }
        public string NombreLibro { get; set; }
        public int Paginas { get; set; }
    }
}
