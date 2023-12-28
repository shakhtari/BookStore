using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Books
{
    public class CreateUpdateBookDto
    {
        
        public string Name { get; set; }

     
        public DateTime ReleaseDate { get; set; } = DateTime.Now;

      
        public float Price { get; set; }
    }
}