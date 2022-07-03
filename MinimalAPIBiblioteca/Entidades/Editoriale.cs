using System;
using System.Collections.Generic;

namespace MinimalAPIBiblioteca.Entidades
{
    public partial class Editoriale
    {
        public Editoriale()
        {
            Libros = new HashSet<Libro>();
        }

        public int EditorialId { get; set; }
        public string Nombre { get; set; } = null!;
        public bool? Eliminado { get; set; }

        public virtual ICollection<Libro> Libros { get; set; }
    }
}
