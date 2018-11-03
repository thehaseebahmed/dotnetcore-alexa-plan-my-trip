using PlanMyTrip.Library.Models.InternalModels;

namespace PlanMyTrip.Library.Handlers.AMAZON
{
    public class HelpIntentHandler : IIntentHandler
    {
        public InteractionInternalModel Process(
            InteractionInternalModel interaction
            )
        {
            interaction.Response.Text = "Hi there, the replenium skill can help you add items to your replenishments, cancel existing replenishments or check your upcoming orders. What would you like to do?";

            interaction.Response.Prompt = "If you want to add items to your replenishments, just say add replenishments.";

            interaction.Session.EndSession = false;

            return interaction;
        }
    }
}
