using EfBook.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Semantics;
using Book = EfBook.Domain.Book;

namespace EfBook.App.Models
{
    public class BookVM
    {
        public int Id { get; set; }
        public int ReleaseYear { get; set; }
        public int NumberOfPages { get; set; }
        public string Title { get; set; }
        public string BookLanguage { get; set; }
        public string Resume { get; set; }
        public int AuthorId { get; set; }

        public string FullNameAndCountry { get; set; }
        public string Genres { get; set; }

        public BookVM()
        {

        }

        public BookVM(Domain.Book book)
        {
            Id = book.Id;
            ReleaseYear = book.ReleaseYear;
            NumberOfPages = book.NumberOfPages;
            Title = book.Title;
            BookLanguage = book.BookLanguage;
            Resume = book.Resume;
            AuthorId = book.AuthorId;
            FullNameAndCountry = book.Author.FullNameAndCountry;

            foreach (var bookBookGenre in book.BookGenres)
            {
                Genres += bookBookGenre.Genre.Type + ", ";
            }

           Genres = Genres.Remove(Genres.Length - 2);
        }
    }
}
