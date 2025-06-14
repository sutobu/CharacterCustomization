using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? ClassId { get; set; }
        public Class Class { get; set; }

        public List<Character> Characters { get; set; } = new();
    }
}
