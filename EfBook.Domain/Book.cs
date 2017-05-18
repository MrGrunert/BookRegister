using System.Collections.Generic;

namespace EfBook.Domain
{
    public class Book
    {
        public Book()
        {
            

        }
        public Book(string title, int authorId, int releaseYear, int numberOfPages, string bookLanguage, string resume)
        {
            NumberOfPages = numberOfPages;
            Title = title;
            ReleaseYear = releaseYear;
            BookLanguage = bookLanguage;
            Resume = resume;
            AuthorId = authorId;
        }

        public int Id { get; set; }
        public int ReleaseYear { get; set; }
        public int NumberOfPages { get; set; }
        public string Title { get; set; }
        public string BookLanguage { get; set; }
        public string Resume { get; set; }
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
        public virtual ICollection<BookGenre> BookGenres { get; set; }
    }
}