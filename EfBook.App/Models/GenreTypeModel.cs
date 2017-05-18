using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfBook.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EfBook.App.Models
{
    public class GenreTypeModel
    {
        public int PoopId { get; set; }
        public string Display { get; set; }
        public bool IsChecked { get; set; }

        public GenreTypeModel()
        {
            
        }

        public GenreTypeModel(Genre genre)
        {
            PoopId = genre.Id;
            Display = genre.Type;
            IsChecked = false;
        }
    }
}
