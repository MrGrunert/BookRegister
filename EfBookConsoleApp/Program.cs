using EfBook.Data;
using EfBook.Domain;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace EfBookConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ClearDatabase();
            AddAuthor();
            AddBook();
            AddGenre();
            AddBookGenre();

            Console.ReadKey();
        }

        static void AddAuthor()
        {
            Author[] author =
            {
                new Author("Joppe", "Jansson", "Sweden"),
                new Author("Robert ", "Jordan", "USA"),
                new Author("Stephen", "King", "USA"),
            };

            SaveToDatabase(author);
        }

        static void AddBook()
        {
            Book[] books =
            {
                new Book("L's Erotic Advetures", 1, 2015, 45, "Swedish", "L is meeting a hairy woman in a bar..."),
                new Book("The Eye of the World", 2, 1994, 702, "English","The Eye of the World revolves around protagonists Rand al\'Thor, " +
                                                                         "Matrim (Mat) Cauthon, Perrin Aybara, Egwene al\'Vere, and Nynaeve al\'Meara, " +
                                                                         "after their residence of \"Emond\'s Field\" is unexpectedly attacked by Trollocs " +
                                                                         "(the antagonist\'s soldiers) and a Myrddraal (the undead-like officer commanding the Trollocs) " +
                                                                         "intent on capturing Rand, Mat, and Perrin. To save their village from further attacks, " +
                                                                         "Rand, Mat, Perrin, and Egwene flee the village, accompanied by the Aes Sedai Moiraine Damodred, " +
                                                                         "her Warder Al\'Lan Mandragoran, and gleeman Thom Merrilin, and later joined by " +
                                                                         "Wisdom Nynaeve al\'Meara. Pursued by increasing numbers of Trollocs and Myrddraal, " +
                                                                         "the travellers take refuge in the abandoned city of Shadar Logoth, " +
                                                                         "where Mat is infected by the malevolent Mashadar."),
                new Book("The Stand", 3, 1978, 823, "English", "At a remote U.S. Army base, a weaponized strain of influenza known as Project Blue is accidentally released " +
                                                               "inside a secret underground laboratory. Charles Campion, a soldier charged with security, " +
                                                               "manages to escape the base by car with his wife and child. By the time the Army tracks Campion down to the " +
                                                               "East Texas town of Arnette and establishes a cordon sanitaire around it, he as patient zero has already died " +
                                                               "of the Project Blue virus and spread it to numerous others beyond the cordon. " +
                                                               "A pandemic of apocalyptic proportions is triggered, which eventually kills off 99.4% of the world\'s human population.")
            };
            SaveToDatabase(books);
            
        }


        public static void AddGenre()
        {
            Genre[] genres =
            {
                new Genre("Erotic"),
                new Genre("Fantasy"),
                new Genre("Post Apocalytic"),
                new Genre("Horror"),
                new Genre("Sci-fi"),
                new Genre("Thriller")
            };
            SaveToDatabase(genres);
        }

        public static void AddBookGenre()
        {
            BookGenre[] bookGenres =
            {
                new BookGenre(1, 1),
                new BookGenre(1, 2),
                new BookGenre(2, 2),
                new BookGenre(3, 3),
            };
            SaveToDatabase(bookGenres);
        }

        private static void SaveToDatabase<TEntity>(TEntity[] entities)
        {
            try
            {
                using (var context = new BookContext())
                {
                    foreach (var entity in entities)
                        context.AddRange(entity);
                    context.SaveChanges();
                }
                Console.WriteLine($"Saved {entities.Length} {typeof(TEntity).Name}'s to database!");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Could not save {typeof(TEntity).Name}'s to database.\n{exception}");
            }
        }

        private static void ClearDatabase()
        {
            DeleteAllEntities<BookGenre>();
            DeleteAllEntities<Author>();
            DeleteAllEntities<Book>();
            DeleteAllEntities<Genre>();           

            void DeleteAllEntities<TEntity>(string tableName = null)
                where TEntity : class
            {
                tableName = tableName ?? $"{typeof(TEntity).Name}s";
                try
                {
                    using (var context = new BookContext())
                    {
                        var entityContext = context.Set<TEntity>();
                        var allEntities = entityContext.Select(e => e);
                        entityContext.RemoveRange(allEntities);
                        context.SaveChanges();
                        if (!(typeof(TEntity).GetProperty("Id") is null))
                            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('{tableName}', RESEED, 0)");
                    }
                    Console.WriteLine($"{tableName} cleared!");
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Could not clear '{tableName}'.\n{exception}");
                }
            }
        }
    }
}