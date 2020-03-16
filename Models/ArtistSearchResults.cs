using System;
using System.Collections.Generic;
using System.Text;

namespace LifeItMusicApp.Models
{
    /// <summary>
    /// iTunes API Search results for Artist type
    /// </summary>
    class ArtistSearchResults
    {
        public int resultCount { get; set; }
        public List<ArtistResultWrapper> results { get; set;}
    }
}
