using System.Collections.Generic;

namespace PlanMyTrip.Library.Models.InternalModels
{
    public class InteractionInternalModel
    {
        public InteractionInternalModel()
        {
            Id = string.Empty;
            Session = new SessionInternalModel();
            Request = new RequestInternalModel();
            Response = new ResponseInternalModel();
        }

        public string Id { get; set; }
        public SessionInternalModel Session { get; set; }
        public RequestInternalModel Request { get; set; }
        public ResponseInternalModel Response { get; set; }
    }

    public class ResponseInternalModel
    {
        public string Text { get; set; }
        public string Ssml { get; set; }
        public string Prompt { get; set; }
        public string Event { get; set; }
        public CardInternalModel Card { get; set; }
    }

    public class CardInternalModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    public class RequestInternalModel
    {
        public string Id { get; set; }
        public string Intent { get; set; }
        public string State { get; set; }
        public string Channel { get; set; }
        public string ConfirmationStatus { get; set; }
        public List<KeyValuePair<string, string>> Parameters { get; set; }
    }

    public class SessionInternalModel
    {
        public string Id { get; set; }
        public bool EndSession { get; set; }
    }
}
