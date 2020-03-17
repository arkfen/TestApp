using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LifeItMusicApp.Models;
using Newtonsoft.Json;
using LifeItMusicApp.Local;

namespace LifeItMusicApp.Domain
{
    static class Cache
    {
        private const string _artistsFilePath = @"..\..\..\cache\artists.json";
        private const string _albumsFilePath = @"..\..\..\cache\albums.json";
        private static List<Artist> _artists;
        private static List<Album> _albums;
        public static bool IsOn { get; private set; } = true;

        static Cache()
        {
            // If cahce is on loading all information from the persistent cache (json files)
            if (IsOn) LoadArtists();
            if (IsOn) LoadAlbums();
        }


        /// <summary>
        /// Loading the artists list
        /// </summary>
        private static void LoadArtists()
        {
            try
            {
                string json = File.ReadAllText(_artistsFilePath);
                _artists = (List<Artist>)JsonConvert.DeserializeObject(json, typeof(List<Artist>));
                if (_artists == null) _artists = new List<Artist>();
            }
            catch
            {
                TurnOff();
            }
        }

        /// <summary>
        /// Adding Artist to in-memory and persistent cache if Artists is absent there
        /// </summary>
        /// <param name="artist">Artist object to add to the cache</param>
        internal static void UpdateArtist(Artist artist)
        {
            if (!IsOn) return;
            var result = _artists.Find(a => a.Id == artist.Id);
            if(result == null)
            {
                _artists.Add(artist);
                try
                {
                    string json = JsonConvert.SerializeObject(_artists);
                    File.WriteAllText(_artistsFilePath, json);
                }
                catch
                {
                    TurnOff();
                }
            }
        }


        /// <summary>
        /// Getting the list of Albums from the in-memory chache
        /// </summary>
        /// <param name="artist">Artist which albums must be retrieved</param>
        /// <returns>The list of Albums of the Artist</returns>
        internal static List<Album> GetAlbums(Artist artist)
        {
            if (!IsOn) return null;
            return _albums.FindAll(a => a.ArtistId == artist.Id);
        }


        /// <summary>
        /// Adding albums to the in-memeory and persistent cache if information is absent
        /// </summary>
        /// <param name="albums">List of albums to be added to the cache</param>
        internal static void UpdateAlbums(List<Album> albums)
        {
            if (!IsOn) return;
            bool isPersistentMustBeUpdated = false;
            foreach(Album album in albums)
            {
                var result = _albums.Find(a => a.Id == album.Id);
                if(result == null)
                {
                    isPersistentMustBeUpdated = true;
                    _albums.Add(album);
                }
            }
            if(isPersistentMustBeUpdated)
            {
                try
                {
                    string json = JsonConvert.SerializeObject(_albums);
                    File.WriteAllText(_albumsFilePath, json);
                }
                catch
                {
                    TurnOff();
                }
            }
        }



        /// <summary>
        /// Checking if the information about Artist must be updated (for instance the Album lists must be cached)
        /// </summary>
        /// <param name="artist">Artist object</param>
        /// <returns>True if update must be done</returns>
        internal static bool CheckIfMustUpdate(Artist artist)
        {
            if (!IsOn) return false;
            var result = _artists.Find(a => a.Id == artist.Id);
            if(result == null)
            {
                // If for any reason new Artist was not added to the cache - doing it now!
                UpdateArtist(artist);
                return true;
            }
            else
            {
                // If the Artist was updated more than 24 hours ago or if it is NEW Artist - it must be updated...
                if (DateTime.Now - result.Update > TimeSpan.FromHours(24) || DateTime.Now - result.Update < TimeSpan.FromSeconds(20))
                {
                    return true;
                }
            }
            return false;
        }



        /// <summary>
        /// Loading the albums list
        /// </summary>
        private static void LoadAlbums()
        {
            if (!IsOn) return;
            try
            {
                string json = File.ReadAllText(_albumsFilePath);
                _albums = (List<Album>)JsonConvert.DeserializeObject(json, typeof(List<Album>));
                if (_albums == null) _albums = new List<Album>();
            }
            catch
            {
                TurnOff();
            }
            
        }

        /// <summary>
        /// Turing the cache off
        /// </summary>
        private static void TurnOff()
        {
            Console.WriteLine(Texts.CacheTurningOff);
            IsOn = false;
        }


        /// <summary>
        /// Getting Artists list from the in memory cache
        /// </summary>
        /// <param name="searchString">Full or partial artist name to search again</param>
        /// <returns>List of the Artists found in the cache</returns>
        internal static List<Artist> GetArtists(string searchString)
        {
            if (!IsOn) return null;
            searchString = searchString.ToLower();
            return _artists.FindAll(a => a.Name.ToLower().Contains(searchString));
        }







        /// <summary>
        /// Testing loading from json files
        /// </summary>
        //public static void LoadTest()
        //{
        //    Console.WriteLine("Load Test Begins");
        //    Console.WriteLine(_artists[0].Name);
        //    Console.WriteLine(_albums[0].Name);
        //    Console.WriteLine("Load Test Ends");
        //}


    }
}
