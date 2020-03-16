using LifeItMusicApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Newtonsoft.Json;

namespace LifeItMusicApp.Domain
{
    class iTunesAPI
    {
        private const string _urlMediaEntity = "https://itunes.apple.com/search?media=music&entity=";
        private const string _entityValueArtist = "musicArtist";
        private const string _entityValueAlbum = "album";
        private const string _searchStringParam = "&term=";

        /// <summary>
        /// Getting
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        internal static List<Artist> GetArtists(string searchString)
        {
            // Trying to get data online and throwing exception or returning null if no success
            ArtistSearchResults searchResults = null;
            string url = _urlMediaEntity + _entityValueArtist + _searchStringParam + HttpUtility.UrlEncode(searchString); 
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();
                    string json = response.Content.ReadAsStringAsync().Result;
                    searchResults = JsonConvert.DeserializeObject<ArtistSearchResults>(json);
                }
            }
            catch
            {
                throw;
            }
            if (searchResults == null) return null;
            // If there is not null results and no exceptions converting results to the internal Artists list format and return it
            List<Artist> artists = new List<Artist>();
            if(searchResults != null && searchResults.resultCount > 0)
            {
                foreach(ArtistResultWrapper result in searchResults.results)
                {
                    artists.Add(new Artist
                    {
                        Id = result.artistId,
                        Name = result.artistName,
                        Update = DateTime.Now
                    });
                }
            }
            return artists;
        }

        internal static List<Album> GetAlbums(Artist artist)
        {
            throw new NotImplementedException();
        }
    }
}
