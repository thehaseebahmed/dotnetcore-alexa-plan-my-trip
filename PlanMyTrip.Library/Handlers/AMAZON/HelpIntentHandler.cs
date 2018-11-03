using PlanMyTrip.Library.Constants;
using PlanMyTrip.Library.Models.InternalModels;

namespace PlanMyTrip.Library.Handlers.AMAZON
{
    public class HelpIntentHandler : IIntentHandler
    {
        public InteractionInternalModel Process(
            InteractionInternalModel interaction
            )
        {
            interaction.Response.Text = Speech.HelpReply;
            interaction.Response.Prompt = Speech.HelpPrompt;
            interaction.Session.EndSession = false;

            return interaction;
        }
    }
}
