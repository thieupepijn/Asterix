using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Asterix
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length != 2)
            {
                Console.WriteLine("Usage: Asterix <lokationsfile> <destination> ");
                return;
            }

            string locationsFile = args[0];
            List<string> locations = File.ReadLines(locationsFile).ToList();

            string mainDestination = args[1];
            int albumCounter = 1;

            WebClient webClient = new WebClient();

            foreach (string location in locations)
            {
                string albumLocation = Path.Join(mainDestination, string.Format("Album{0}", albumCounter));
                Directory.CreateDirectory(albumLocation);

                for (int pageNumber = 1; pageNumber < 100; pageNumber++)
                {
                    string origin = string.Format("{0}{1}.jpg", location, FormatNumber(pageNumber));
                    string destination = Destination(albumLocation, albumCounter, pageNumber);
                    string message = string.Format("Downloading album {0} page {1} to {2}", albumCounter, pageNumber, destination);
                    Console.Write(message);
                    try
                    {
                        webClient.DownloadFile(origin, destination);
                        Console.Write(" SUCCES");
                    }
                    catch
                    {
                        Console.Write(" FAILED");
                    }
                    Console.WriteLine();
                }
                albumCounter++;
            }
        }


        private static string FormatNumber(int pageNumber)
        {
            if (pageNumber < 10)
            {
                return  string.Format("00{0}", pageNumber);
            }
            else if (pageNumber < 100)
            {
                return string.Format("0{0}", pageNumber);
            }
            else
            {
                return string.Format("{0}", pageNumber);
            }

        }

        private static string Destination(string albumDirectory, int albumNumber, int pageNumber)
        {
            string fileName = string.Format("Album{0}Page{1}.jpg", albumNumber, pageNumber);
            return Path.Join(albumDirectory, fileName);
        }

    }
}
