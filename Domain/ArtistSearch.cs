using LifeItMusicApp.Local;
using LifeItMusicApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;

namespace LifeItMusicApp.Domain
{
    class ArtistSearch
    {
        /// <summary>
        /// Implementing the search for the Artist using user search string input
        /// </summary>
        /// <returns>The Artist object or null if nothing has been found</returns>
        internal static Artist Implement()
        {
            Artist artist = null;
            List<Artist> artists;
            string searchString = GetSearchString();
            artists = GetResults(searchString);
            ShowResults(artists);
            if(artists != null && artists.Count > 1)
            {
                artist = ChooseOneArtist(artists);
            }
            if (artists != null && artists.Count == 1)
            {
                artist = artists.First();

            }
            return artist;
        }

        /// <summary>
        /// Choosing only one Artist from the multiple artists result list
        /// </summary>
        /// <param name="artists">Artists list</param>
        /// <returns>Artist user chooses</returns>
        private static Artist ChooseOneArtist(List<Artist> artists)
        {
            Console.WriteLine(string.Empty);
            Console.Write(Texts.PleaseEnterTheIdOfTheArtist);
            try
            {
                int id = Convert.ToInt32(Console.ReadLine());
                Artist artist = artists.Find(a => a.Id == id);
                Console.WriteLine(string.Empty);
                if (artist == null)
                {
                    Console.WriteLine(Texts.NothingFoundTryAgain);
                }
                else
                {
                    Console.WriteLine(Texts.FoundArtistName + ": " + artist.Name);
                }
                return artist;
            }
            catch
            {
                return null;
            }
        }



        /// <summary>
        /// Showing results depending on how many Artists have been found
        /// </summary>
        /// <param name="artists">Artists list to analyse and use</param>
        private static void ShowResults(List<Artist> artists)
        {
            Console.WriteLine(string.Empty);
            if(artists == null)
            {
                Console.WriteLine(Texts.SomethingWentWrongTryAgainLater);
            }
            if(artists.Count == 0)
            {
                Console.WriteLine(Texts.NothingFoundTryAgain);
            }
            if(artists.Count > 1)
            {
                Console.WriteLine(Texts.MoreThanOneArtistFoundBeMoreSpecific + "\n");
                Console.WriteLine(Texts.ManyArtistsFoundNames + "\n");
                foreach(Artist artist in artists)
                {
                    Console.WriteLine("Id: " + artist.Id + "  |  Name: " + artist.Name);
                }
            }
            if (artists.Count == 1)
            {
                Console.WriteLine(Texts.FoundArtistName + ": " + artists.First().Name);
            }
        }



        /// <summary>
        /// Getting list of Artists according to the search string
        /// </summary>
        /// <returns>The list of Artists found</returns>
        private static List<Artist> GetResults(string searchString)
        {
            List<Artist> artists;
            // Normally try to get the data over Internet
            try
            {
                artists = iTunesAPI.GetArtists(searchString);
                if(artists.Count == 1)
                {
                    Cache.UpdateArtist(artists.First());
                }
            }
            // If no Internet connection or any other problem getting information over http - try to search in Cache!
            catch
            {
                artists = Cache.GetArtists(searchString);
            }
            return artists;
        }



        /// <summary>
        /// Getting the search string from the user
        /// </summary>
        /// <returns>Search string user entered</returns>
        private static string GetSearchString()
        {
            Console.WriteLine(Texts.SearchingArtistsAlbums + "\n");
            Console.Write(Texts.PleaseEnterFullOrPartialArtistName);
            string searchString = Console.ReadLine();
            return searchString;
        }
    }
}
