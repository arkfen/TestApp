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
        private const string _artistsUrl = @"..\..\..\cache\artists.json";
        private const string _albumsUrl = @"..\..\..\cache\albums.json";
        private static List<Artist> _artists;
        private static List<Album> _albums;
        public static bool IsOn { get; private set; } = true;

        static Cache()
        {
            if (IsOn) LoadArtists();
            if (IsOn) LoadAlbums();
        }

        private static void LoadArtists()
        {
            try
            {
                string json = File.ReadAllText(_artistsUrl);
                _artists = (List<Artist>)JsonConvert.DeserializeObject(json, typeof(List<Artist>));
            }
            catch(Exception e)
            {
                TurnOff();
            }
        }

        private static void LoadAlbums()
        {
            try
            {
                string json = File.ReadAllText(_albumsUrl);
                _albums = (List<Album>)JsonConvert.DeserializeObject(json, typeof(List<Album>));
            }
            catch
            {
                TurnOff();
            }
            
        }

        private static void TurnOff()
        {
            Console.WriteLine(Texts.CacheTurningOff);
            IsOn = false;
        }


        // Test
        public static void Test()
        {
            Console.WriteLine(_artists[0].Name);

            return;
        }
    }
}
