using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;


namespace BookStore.Books
{
    public class BookDto:AuditedEntityDto<Guid>
    {
        //public BookDto(Guid id) => Id = id;
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public float Price { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int BookType { get; set; }
        public string Translator { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Publication { get; set; }
    }
}
