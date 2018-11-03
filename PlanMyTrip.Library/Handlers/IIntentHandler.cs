using PlanMyTrip.Library.Models.InternalModels;

namespace PlanMyTrip.Library.Handlers
{
    public interface IIntentHandler
    {
        InteractionInternalModel Process(InteractionInternalModel interaction);
    }
}