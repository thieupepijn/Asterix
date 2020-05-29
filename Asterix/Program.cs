using System;
using System.IO;
using System.Net;
using System.Text;

namespace Asterix
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient webClient = new WebClient();
            string originCommon = "https://www.omgbeaupeep.com/comics/mangas/Asterix/043%20-%20Asterix%20and%20the%20Chariot%20Race%20(2017)/read-asterix-and-the-chariot-race-comic-online-";
            string destinationDirectory = @"D:\Matthieu\Comics\AstrixPages\AsterixAndTheChariotRace";
            string destinationCommonFilePart = "Asterix43_Page";

            for (int pageNumber=0; pageNumber<100; pageNumber++)
            {
                string origin = GetOriginFileName(originCommon, pageNumber);
                string destination = GetDestinationFileName(destinationDirectory, destinationCommonFilePart, pageNumber);

       
                string message = string.Format("Downloading {0} to {1}", origin, destination);
                Console.Write(message);

                try
                {
                    webClient.DownloadFile(origin, destination);
                    Console.Write(" succes");
                }
                catch(Exception exception)
                {
                    Console.Write(exception.Message);
                }
                Console.WriteLine(string.Empty);

            }
        }

        private static string GetOriginFileName(string commonPart, int pageNumber)
        {
            string pagePart = string.Empty;
            if (pageNumber < 10)
            {
                pagePart = string.Format("00{0}", pageNumber);
            }
            else if (pageNumber < 100)
            {
                pagePart = string.Format("0{0}", pageNumber);
            }
            else
            {
                pagePart = string.Format("{0}", pageNumber);
            }

            string imagefilename = string.Format("{0}{1}{2}", commonPart, pagePart, ".jpg");
            return imagefilename;
        }

        private static string GetDestinationFileName(string destinationDirectory, string destinationCommonFilePart, int pageNumber)
        {
            string imageFileName = string.Format("{0}{1}{2}{3}.jpg", destinationDirectory, Path.DirectorySeparatorChar, destinationCommonFilePart, pageNumber);
            return imageFileName;
        }

    }
}
