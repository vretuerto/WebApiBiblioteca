using Microsoft.EntityFrameworkCore;

namespace WebApiBiblioteca.Entidades.Seed
{
    public class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {

            // Editoriales
            var e1 = new Editorial { EditorialId = 1, Nombre = "Editorial 1" };
            var e2 = new Editorial { EditorialId = 2, Nombre = "Editorial 2" };

            modelBuilder.Entity<Editorial>().HasData(e1, e2);

            //Libros
            var l1 = new Libro { LibroId = 1, Nombre = "Libro 1", Paginas = 50, EditorialId = 1 };
            var l2 = new Libro { LibroId = 2, Nombre = "Libro 2", Paginas = 500, EditorialId = 2 };
            var l3 = new Libro { LibroId = 3, Nombre = "Libro 3", Paginas = 1500, EditorialId = 1 };
            var l4 = new Libro { LibroId = 4, Nombre = "Libro 4", Paginas = 850, EditorialId = 2 };
            var l5 = new Libro { LibroId = 5, Nombre = "Libro 5", Paginas = 700, EditorialId = 1 };
            var l6 = new Libro { LibroId = 6, Nombre = "Libro 6", Paginas = 4000, EditorialId = 2 };
            var l7 = new Libro { LibroId = 7, Nombre = "Libro 7", Paginas = 550, EditorialId = 1 };
            var l8 = new Libro { LibroId = 8, Nombre = "Libro 8", Paginas = 150, EditorialId = 2 };
            var l9 = new Libro { LibroId = 9, Nombre = "Libro 9", Paginas = 100, EditorialId = 1 };
            var l10 = new Libro { LibroId = 10, Nombre = "Libro 10", Paginas = 50, EditorialId = 2 };

            modelBuilder.Entity<Libro>().HasData(l1, l2, l3, l4, l5, l6, l7, l8, l9, l10);
        }
    }
}

