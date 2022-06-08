using System.ComponentModel.DataAnnotations;

namespace WebApiBiblioteca.Entidades
{
    public class Libro
    {
        public int LibroId { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Range(1,10000)]
        public int Paginas { get; set; }
        public int EditorialId { get; set; }
        public Editorial Editorial { get; set; }
    }
}
