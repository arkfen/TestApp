using System;
using System.Collections.Generic;
using System.Text;

namespace LifeItMusicApp.Models
{
    class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> Albums { get; set; }
    }
}
