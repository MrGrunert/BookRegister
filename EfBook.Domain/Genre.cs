using System.Collections.Generic;

namespace EfBook.Domain
{
    public class Genre
    {
        public Genre()
        {
            
        }
        public Genre(string type)
        {
            Type = type;
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<BookGenre> BookGenres { get; set; }
    }
}