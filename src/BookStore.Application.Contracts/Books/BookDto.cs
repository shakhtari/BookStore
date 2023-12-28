using System;
using Volo.Abp.Application.Dtos;


namespace BookStore.Books
{
    public class BookDto:AuditedEntityDto<Guid>
    {
        
        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
