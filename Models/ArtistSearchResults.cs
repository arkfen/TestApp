using System;
using System.Collections.Generic;
using System.Text;

namespace LifeItMusicApp.Models
{
    class ArtistSearchResults
    {
        public int resultCount { get; set; }
        public List<ArtistResultWrapper> results { get; set;}
    }
}
