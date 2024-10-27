using Kawkaba.Core.Entity.ApplicationData;
using Kawkaba.Core.Entity.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawkaba.Core.Entity.Posts
{
    public class Post
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Text { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public IEnumerable<Images> Files { get; set; } = new List<Images>();
        public string? CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public ApplicationUser? Company { get; set; }
    }
}
