namespace EfBook.Domain
{
    public class BookGenre
    {
        public BookGenre()
        {
            
        }
        public BookGenre(int bookId, int genreId)
        {
            BookId = bookId;
            GenreId = genreId;
        }

        public int Id { get; set; }
        public int BookId { get; set; }
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Book Book { get; set; }
    }
}