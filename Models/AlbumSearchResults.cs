using System;
using System.Collections.Generic;
using System.Text;

namespace LifeItMusicApp.Models
{
    /// <summary>
    /// iTunes API Search results for Album type
    /// </summary>
    class AlbumSearchResults
    {
        public int resultCount { get; set; }
        public List<AlbumResultWrapper> results { get; set;}
    }
}
