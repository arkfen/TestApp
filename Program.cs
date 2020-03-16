using System;
using System.Text;
using LifeItMusicApp.Domain;

namespace LifeItMusicApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Cache Load Testing
            //Cache.LoadTest();


            AlbumSearch.Start();


        }
    }
}
