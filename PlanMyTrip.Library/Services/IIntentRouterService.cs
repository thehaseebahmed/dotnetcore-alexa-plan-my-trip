using PlanMyTrip.Library.Models.InternalModels;

namespace PlanMyTrip.Library.Services
{
    public interface IIntentRouterService
    {
        InteractionInternalModel Process(InteractionInternalModel interaction);
    }
}