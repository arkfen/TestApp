using System;
using System.Collections.Generic;
using System.Text;

namespace LifeItMusicApp.Models
{
    class AlbumSearchResults
    {
        public int resultCount { get; set; }
        public List<AlbumResultWrapper> results { get; set;}
    }
}
