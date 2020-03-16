using System;
using System.Collections.Generic;
using System.Text;
using LifeItMusicApp.Models;

namespace LifeItMusicApp.Domain
{
    static class Cache
    {
        private static List<Artist> _artists;
        private static List<Album> _albums;

        static Cache()
        {
            _artists = GetArtists();
            _albums = GetAlbums();
        }

        private static List<Artist> GetArtists()
        {
            List<Artist> artists = new List<Artist>();

            throw new NotImplementedException();
            return artists;
        }

        private static List<Album> GetAlbums()
        {
            List<Album> albums = new List<Album>();

            throw new NotImplementedException();
            return albums;
        }

    }
}
