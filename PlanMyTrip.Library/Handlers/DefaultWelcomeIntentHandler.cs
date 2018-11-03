using PlanMyTrip.Library.Constants;
using PlanMyTrip.Library.Models.InternalModels;

namespace PlanMyTrip.Library.Handlers
{
    public class DefaultWelcomeIntentHandler : IIntentHandler
    {
        public InteractionInternalModel Process(
            InteractionInternalModel interaction
            )
        {
            interaction.Response.Text = Speech.WelcomeReply;
            interaction.Response.Prompt = Speech.WelcomePrompt;
            interaction.Session.EndSession = false;

            return interaction;
        }
    }
}
