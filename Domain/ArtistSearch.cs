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
            do
            {
                string searchString = GetSearchString();
                artists = GetResults(searchString);
                ShowResults(artists);
            }
            while (artists != null && artists.Count != 1);
            if (artists != null && artists.Count == 1)
            {
                artist = artists.First();
                
            }
            return artist;
        }



        /// <summary>
        /// Showing results depending on how many Artists have been found
        /// </summary>
        /// <param name="artists">Artists list to analyse and use</param>
        private static void ShowResults(List<Artist> artists)
        {
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
                Console.WriteLine(Texts.MoreThanOneArtistFoundBeMoreSpecific);
                Console.Write(Texts.ManyArtistsFoundNames + ": ");
                foreach(Artist artist in artists)
                {
                    Console.Write("'" + artist.Name + "'  |  ");
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
            Console.WriteLine(Texts.SearchingArtistsAlbums);
            Console.Write(Texts.PleaseEnterFullOrPartialArtistName);
            string searchString = Console.ReadLine();
            return searchString;
        }
    }
}
