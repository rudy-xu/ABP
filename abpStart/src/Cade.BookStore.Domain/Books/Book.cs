using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Domain.Entities.Auditing;

namespace Cade.BookStore.Books
{
    /**
     * Guid is same as uuid, that is the key of some table 
     */
    public class Book : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }
}
