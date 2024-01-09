using System;
using Volo.Abp.Application.Dtos;


namespace BookStore.Books
{
    public class BookDto:AuditedEntityDto<Guid>
    {
        //public BookDto(Guid id) => Id = id;
        
        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsTranslated { get; set; }
        public string Translator { get; set; }
        public int Publication { get; set; }
    }
}
