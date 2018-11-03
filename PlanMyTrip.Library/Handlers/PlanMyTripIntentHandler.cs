using System.Text;
using PlanMyTrip.Library.Constants;
using PlanMyTrip.Library.Models.InternalModels;

namespace PlanMyTrip.Library.Handlers
{
    public class PlanMyTripIntentHandler : IIntentHandler
    {
        public InteractionInternalModel Process(
            InteractionInternalModel interaction
            )
        {
            string travelMode = interaction.Request.Parameters
                .Find(p => p.Key.Equals("travelModel"))
                .Value;
            string fromCity = interaction.Request.Parameters
                .Find(p => p.Key.Equals("fromCity"))
                .Value;
            string toCity = interaction.Request.Parameters
                .Find(p => p.Key.Equals("toCity"))
                .Value;
            string travelDate = interaction.Request.Parameters
                .Find(p => p.Key.Equals("travelDate"))
                .Value;
            string activity = interaction.Request.Parameters
                .Find(p => p.Key.Equals("activity"))
                .Value;

            StringBuilder finalResponse = new StringBuilder();
            finalResponse.Append(Speech.TripIntro);

            if (!string.IsNullOrEmpty(travelMode)) { finalResponse.Append(travelMode); }
            else { finalResponse.Append("You'll go "); }

            finalResponse.Append($"from {fromCity} to {toCity} on {travelDate}");

            if(!string.IsNullOrEmpty(activity)) { finalResponse.Append($" to go {activity}"); }

            interaction.Response.Text = finalResponse.ToString();
            interaction.Session.EndSession = true;

            return interaction;
        }
    }
}
