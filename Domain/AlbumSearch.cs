using LifeItMusicApp.Local;
using LifeItMusicApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LifeItMusicApp.Domain
{
    class AlbumSearch
    {
        private static bool _isOn { get; set; } = false;

        /// <summary>
        /// The loop method for starting and continuing the album search UX routing and getting data accordignly...
        /// </summary>
        public static void Start()
        {
            _isOn = true;
            while(_isOn)
            {
                Artist artist = ArtistSearch.Implement();
                if(artist != null)
                {
                    List<Album> albums = GetAlbums(artist);
                    ShowResults(albums);
                }
                CheckIfUserWantsToGoOn();
            }
        }

        /// <summary>
        /// Showing results of albums found
        /// </summary>
        /// <param name="albums">List of Albums</param>
        private static void ShowResults(List<Album> albums)
        {
            Console.WriteLine(Texts.TheListOfAlbums);
            foreach (Album album in albums)
            {
                Console.WriteLine(album.Name + " " + album.ReleaseDate.Year + "   iTunes => " + album.Url);
            }
        }

        /// <summary>
        /// Getting list of Albums for specific Artist
        /// </summary>
        /// <param name="artist">Artist object we wanna get albums of</param>
        /// <returns>The list of Albums for speciic Artist</returns>
        private static List<Album> GetAlbums(Artist artist)
        {
            List<Album> albums;
            // Normally try to get the data over Internet
            try
            {
                albums = iTunesAPI.GetAlbums(artist);
                if (albums.Count > 0 && Cache.CheckIfMustUpdate(artist))
                {
                    Cache.UpdateAlbums(albums);
                }
            }
            // If no Internet connection or any other problem getting information over http - try to search in Cache!
            catch
            {
                albums = Cache.GetAlbums(artist);
            }
            return albums;
        }


        /// <summary>
        /// Checking if the user wants to continues the search
        /// </summary>
        private static void CheckIfUserWantsToGoOn()
        {
            Console.WriteLine(Texts.DoYouWantToSearchAlbumsAgain);
            Console.WriteLine(Texts.YesOrNoQuestion);
            string answer = Console.ReadLine();
            switch(answer)
            {
                case Texts.Yes: 
                    break;
                case Texts.No: 
                    _isOn = false; 
                    break;
                default: 
                    Console.WriteLine(Texts.WeDidNotUnderstandButAssumeYesByDefault);
                    break;
            }
        }


    }
}
