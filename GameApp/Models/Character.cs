using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }

        public List<Skill> Skills { get; set; }
        public List<Equipment> Equipment { get; set; }
    }
}
