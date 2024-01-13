using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Books
{
    public class CreateUpdateBookDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

     
        public DateTime ReleaseDate { get; set; } = DateTime.Now;

      
        public float Price { get; set; }
        public string BookType { get; set; }
        public string Translator { get; set; }
        public int Publication { get; set; }
    }
}