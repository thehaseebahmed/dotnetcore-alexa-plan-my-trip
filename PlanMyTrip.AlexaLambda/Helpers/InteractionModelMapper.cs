using System;
using System.Collections.Generic;
using System.Linq;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Alexa.NET.Response.Directive;
using PlanMyTrip.Library.Constants;
using PlanMyTrip.Library.Models.InternalModels;

namespace PlanMyTrip.AlexaLambda.Helpers
{
    public class InteractionModelMapper
    {
        internal static InteractionInternalModel FromAlexaRequest(
            SkillRequest skillRequest
            )
        {
            InteractionInternalModel interactionModel = new InteractionInternalModel
            {
                Id = skillRequest.Request.RequestId
            };

            Type requestType = skillRequest.GetRequestType();

            interactionModel.Request.Channel = "alexa";

            if (requestType == typeof(IntentRequest))
            {
                IntentRequest intentRequest = skillRequest.Request as IntentRequest;
                interactionModel.Request.Intent = intentRequest.Intent.Name;
                interactionModel.Request.State = intentRequest.DialogState;
                interactionModel.Request.ConfirmationStatus = intentRequest.Intent.ConfirmationStatus;

                if (intentRequest.Intent.Slots != null)
                    interactionModel.Request.Parameters = intentRequest.Intent.Slots.ToList()
                        .ConvertAll(s => new KeyValuePair<string, string>(s.Value.Name, s.Value.Value));
            }
            else if (requestType == typeof(LaunchRequest))
            {
                interactionModel.Request.Intent = "DefaultWelcomeIntent";
            }
            else if (requestType == typeof(SessionEndedRequest))
            {
                return null;
            }

            return interactionModel;
        }

        internal static SkillResponse ToAlexaResponse(
            InteractionInternalModel commonModel
            )
        {
            SkillResponse response = new SkillResponse
            {
                Version = "1.0",
                Response = new ResponseBody()
            };

            if (commonModel.Request.State == RequestStates.Started || commonModel.Request.State == RequestStates.InProgress)
            {
                DialogDelegate directive = new DialogDelegate
                {
                    UpdatedIntent = new Intent
                    {
                        Name = commonModel.Request.Intent,
                        Slots = commonModel.Request.Parameters?.ToDictionary(p => p.Key, p => new Slot { Name = p.Key, Value = p.Value })
                    }
                };

                response.Response.Directives.Add(directive);
                response.Response.ShouldEndSession = false;

                return response;
            }

            if (string.IsNullOrWhiteSpace(commonModel.Response.Ssml))
            {
                response.Response.OutputSpeech = new PlainTextOutputSpeech
                {
                    Text = commonModel.Response.Text
                };
            }
            else
            {
                response.Response.OutputSpeech = new SsmlOutputSpeech { Ssml = "<speak>" + commonModel.Response.Ssml + "</speak>" };
            }

            if (commonModel.Response.Card != null)
            {
                response.Response.Card = new SimpleCard
                {
                    Title = commonModel.Response.Card.Title,
                    Content = commonModel.Response.Card.Text
                };
            }

            if (!string.IsNullOrWhiteSpace(commonModel.Response.Prompt))
                response.Response.Reprompt = new Reprompt
                {
                    OutputSpeech = new PlainTextOutputSpeech
                    {
                        Text = commonModel.Response.Prompt
                    }
                };

            response.Response.ShouldEndSession = commonModel.Session.EndSession;

            return response;
        }
    }
}
