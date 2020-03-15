using System;
using System.Collections.Generic;
using System.Text;

namespace LifeItMusicApp.Models
{
    class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ArtistId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Url { get; set; }
    }
}
