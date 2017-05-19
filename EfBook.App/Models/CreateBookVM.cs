using System.Collections.Generic;
using EfBook.Domain;

namespace EfBook.App.Models
{
    public class CreateBookVM
    {
        public int Id { get; set; }
        public int ReleaseYear { get; set; }
        public int NumberOfPages { get; set; }
        public string Title { get; set; }
        public string BookLanguage { get; set; }
        public string Resume { get; set; }
        public int AuthorId { get; set; }

        public  Author Author { get; set; }
        public  ICollection<BookGenre> BookGenres { get; set; }


        public List<GenreTypeModel> Genres { get; set; }

        public CreateBookVM()
        {
            Genres = new List<GenreTypeModel>();
            //BookGenres = new List<BookGenre>();
        }
    }
}