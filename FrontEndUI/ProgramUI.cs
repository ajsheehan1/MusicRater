using FrontEndUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndUI
{
    public class ProgramUI
    {

        SWAPIService sWAPIService = new SWAPIService();
        

        public void Run()
        {
            StartMenu();
        }

        private void StartMenu()
        {


            bool keepRunning = true;
            while (keepRunning)
            {
                Console.Clear();

                Console.WriteLine("---- Seven Music Rater ----");
                Console.WriteLine();

                Console.WriteLine("What would you like to navigate by today? \n" +
                    "1) Navigate by Artist \n" +
                    "2) Navigate by Album \n" +
                    "3) Navigate by Song \n" +
                    "4) Navigate to Stores \n" +
                    "5) Exit");

                string response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        NavigateByArtist();
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "2":
                        ComingSoon();
                        Console.Clear();
                        break;
                    case "3":
                        ComingSoon();
                        Console.Clear();
                        break;
                    case "4":
                        ComingSoon();
                        break;
                    case "5":
                        keepRunning = false;
                        break;
                        default:
                        Console.WriteLine("Please enter a number between 1 and 5 \n" +
                            "To continue, press any key");
                        Console.ReadKey();
                        break;
                }

            }

        }
        private void NavigateByArtist()
        {
            Console.Clear();
            Console.WriteLine("---- Seven Music Rater ----");
            Console.WriteLine("-----------Artist ---------");
            Console.WriteLine();

            Console.WriteLine("Please select an option below: \n" +
                    "1) Search by Artist Name \n" +
                    "2) Select from All Artists \n" +
                    "3) Exit");
            string response = Console.ReadLine();

            switch (response) 
            {
                case "1":
                    ComingSoon();
                    Console.Clear();
                    break;
                case "2":
                    ListArtists();
                    // Select Artist -> View Artist Page (Albums -> Songs) 
                    Console.ReadLine();
                    Console.Clear();
                    break;

            }
        }

        private void ListArtists()
        {
            Console.Clear();
            List<Artist> artists = sWAPIService.GetArtistsAsync().Result;

            int count = 1;
            Console.WriteLine($"{"Number",-5} {"Name",-20} {"Rating",-20}");
            foreach (Artist artist in artists)
            {
                Console.WriteLine($"{count + ".", -5} {artist.ArtistName, -20} {artist.Rating,-20}");
                count++;
            }
        }

        
        private void ComingSoon()
        {
            Console.WriteLine("Coming Soon... \n " +
                            "Press Any Key to Continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
