using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EfBook.Domain;

namespace EfBook.App.Models
{
    public class CreateBookVM
    {
        public int Id { get; set; }
        [Required]
        public int ReleaseYear { get; set; }
        [Required]
        public int NumberOfPages { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string BookLanguage { get; set; }
        [Required]
        public string Resume { get; set; }
        [Required]
        public int AuthorId { get; set; }

        public  Author Author { get; set; }
        public  ICollection<BookGenre> BookGenres { get; set; }


        public List<GenreTypeModel> Genres { get; set; }

        public CreateBookVM()
        {
            Genres = new List<GenreTypeModel>();
        }
    }
}