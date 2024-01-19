using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;

namespace BookStore.Books
{
    public class CreateUpdateBookDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public DateTime ReleaseDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "This field is required")]
        public float Price { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int BookType { get; set; }
        public string Translator { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Publication { get; set; }

    }

    
    
}