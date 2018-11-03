using PlanMyTrip.Library.Models.InternalModels;

namespace PlanMyTrip.Library.Handlers
{
    public class DefaultWelcomeIntentHandler : IIntentHandler
    {
        public InteractionInternalModel Process(
            InteractionInternalModel interaction
            )
        {
            interaction.Response.Text = "Hi there!";
            interaction.Session.EndSession = false;

            return interaction;
        }
    }
}
