using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace BookStore.Books
{
    public class Book:FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string BookType { get; set; }
        public string Translator { get; set; }
        public int Publication { get; set; }
    }
}
