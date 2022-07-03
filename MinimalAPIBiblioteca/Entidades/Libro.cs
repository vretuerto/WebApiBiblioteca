using System;
using System.Collections.Generic;

namespace MinimalAPIBiblioteca.Entidades
{
    public partial class Libro
    {
        public int LibroId { get; set; }
        public string Nombre { get; set; } = null!;
        public int Paginas { get; set; }
        public int EditorialId { get; set; }

        public virtual Editoriale Editorial { get; set; } = null!;
    }
}
