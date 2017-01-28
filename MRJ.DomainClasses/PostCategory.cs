using System.Collections.Generic;

namespace MRJ.DomainClasses
{
    public class PostCategory : BaseEntity
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
