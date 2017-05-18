using System.Collections.Generic;

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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string FullNameAndCountry => $"{FirstName} {LastName} {Country}";

        public virtual ICollection<Book> Books { get; set; }
    }
}