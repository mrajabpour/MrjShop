using System.Collections.Generic;

namespace MRJ.DomainClasses
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
