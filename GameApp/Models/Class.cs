using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Character> Characters { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
