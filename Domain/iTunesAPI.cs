﻿using LifeItMusicApp.Models;
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
        /// Getting the list of artists via iTunes API using search string
        /// </summary>
        /// <param name="searchString">Search String</param>
        /// <returns>The list of Artists</returns>
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
            // If there results are not null and no exceptions converting results to the internal Artists list format and return it
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



        /// <summary>
        /// Getting the list of albums using iTunesAoi and Artist object
        /// </summary>
        /// <param name="artist">Artist whos albums needs to be retrived</param>
        /// <returns>The lists of Albums</returns>
        internal static List<Album> GetAlbums(Artist artist)
        {
            // Trying to get data online and throwing exception or returning null if no success
            AlbumSearchResults searchResults = null;
            string url = _urlMediaEntity + _entityValueAlbum + _searchStringParam + HttpUtility.UrlEncode(artist.Name);
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();
                    string json = response.Content.ReadAsStringAsync().Result;
                    searchResults = JsonConvert.DeserializeObject<AlbumSearchResults>(json);
                }
            }
            catch
            {
                throw;
            }
            if (searchResults == null) return null;
            // If there results are not null and no exceptions converting results to the internal Album list format and return it
            // !Attention => getting only those albums which have the same artistId property value as the target Artist object Id
            List<Album> albums = new List<Album>();
            if (searchResults != null && searchResults.resultCount > 0)
            {
                foreach (AlbumResultWrapper result in searchResults.results)
                {
                    if(result.artistId == artist.Id)
                    {
                        albums.Add(new Album
                        {
                            Id = result.collectionId,
                            Name = result.collectionName,
                            ArtistId = result.artistId,
                            ReleaseDate = result.releaseDate,
                            Url = result.collectionViewUrl
                        });
                    }
                }
            }
            return albums;
        }



    }
}
