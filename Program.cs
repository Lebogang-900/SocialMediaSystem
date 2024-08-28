using System;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Extensions.Configuration;
using Facebook;
using System.IO;

namespace SocialMediaSystem
{
    public class Program
    {
        private static Timer _timer;
        private static IConfiguration _configuration;

        static async Task Main(string[] args)
        {
            // Load configuration from appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            _configuration = builder.Build();

            // Initialize the timer with an initial interval
            _timer = new Timer(GetIntervalToNextPost());
            _timer.Elapsed += async (sender, e) => await PostContent();

            // Post content immediately to test
            await PostContent();

            // Start the timer to schedule future posts
            _timer.Start();

            Console.WriteLine("Social Media System is running. Press [Enter] to exit.");
            Console.ReadLine();
        }

        // Generates simple content from a text file
        static string GenerateContent()
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "content.txt");
            string[] contentOptions = File.ReadAllLines(filePath);

            if (contentOptions.Length == 0)
            {
                throw new Exception("The content file is empty.");
            }

            var random = new Random();
            int index = random.Next(contentOptions.Length);
            return contentOptions[index];
        }

        // Post content to Facebook
        static async Task PostContent()
        {
            string content = GenerateContent();
            Console.WriteLine($"Generated Content: {content}");

            var fb = new FacebookClient(_configuration["Facebook:AccessToken"]);
            var messageData = new { message = content };

            try
            {
                var result = await fb.PostTaskAsync($"{_configuration["Facebook:PageId"]}/feed", messageData);
                Console.WriteLine($"Successfully posted to Facebook at {DateTime.Now}");

                
            }
            catch (FacebookOAuthException ex)
            {
                Console.WriteLine($"Failed to post to Facebook: {ex.Message}");

               
            }

            // Update the interval and restart the timer
            _timer.Interval = GetIntervalToNextPost();
            _timer.Start();
        }

        // Calculates the time interval until the next post (unchanged)
        static double GetIntervalToNextPost()
        {
            DateTime now = DateTime.Now;
            DateTime nextPostTime = new DateTime(now.Year, now.Month, now.Day, 10, 0, 0).AddDays(1);
            return (nextPostTime - now).TotalMilliseconds;
        }
    }
}