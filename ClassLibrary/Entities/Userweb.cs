using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryInfinity.Entities
{
    public partial class Userweb
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Nickname { get; set; } = null!;

        public virtual ICollection<Publication> Publications { get; } = new List<Publication>();
    }
}
