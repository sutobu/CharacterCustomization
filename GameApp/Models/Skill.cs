using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public int CharacterId { get; set; }
        public Character Character { get; set; }

        public int? ClassId { get; set; }
        public Class Class { get; set; }

    }
}
