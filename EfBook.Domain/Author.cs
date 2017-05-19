using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EfBook.Domain
{
    public class Author
    {
        public Author()
        {
            

        }
        public Author(string firstName, string lastName, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            Country = country;
        }

        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Country { get; set; }
        public string FullNameAndCountry => $"{FirstName} {LastName} {Country}";

        public virtual ICollection<Book> Books { get; set; }
    }
}