using System;

namespace PlanMyTrip.Library.Constants
{
    public class Speech
    {
        public const string HelpReply = "You can demonstrate the delegate directive by saying \"plan a trip\".";
        public const string HelpPrompt = "Try saying \"plan a trip\".";
        public const string UnknownIntentReply = "Sorry, I don't understand that. Please try again.";
        public const string WelcomeReply = "Let's plan a trip. Where would you like to go?";
        public const string WelcomePrompt = "Let me know where you\'d like to go or when you\'d like to go on your trip.";

        private static readonly string[] _tripIntro = {
            "This sounds like a cool trip. ",
            "This will be fun.",
            "Oh, I like this trip."
        };
        public static string TripIntro
        {
            get
            {
                Random random = new Random((int)DateTime.Now.Ticks);
                int choice = random.Next(0, _tripIntro.Length - 1);
                return _tripIntro[choice];
            }
        }
    }
}
