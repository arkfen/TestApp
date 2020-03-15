using System;
using System.Collections.Generic;
using System.Text;

namespace LifeItMusicApp.Models
{
    class ArtistResultWrapper
    {
        public string wrapperType {get; set;}
        public string artistType { get; set; }
        public string artistName { get; set; }
        public string artistLinkUrl { get; set; }
        public int artistId { get; set; }
        public int amgArtistId { get; set; }
        public string primaryGenreName { get; set; }
        public int primaryGenreId { get; set; }
    }
}
