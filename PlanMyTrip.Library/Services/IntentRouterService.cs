using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using PlanMyTrip.Library.Constants;
using PlanMyTrip.Library.Handlers;
using PlanMyTrip.Library.Handlers.AMAZON;
using PlanMyTrip.Library.Models.InternalModels;

namespace PlanMyTrip.Library.Services
{
    public class IntentRouterService : IIntentRouterService
    {
        public IServiceProvider ServiceProvider { get; }

        public IntentRouterService(
            IServiceProvider serviceProvider
            )
        {
            ServiceProvider = serviceProvider;
        }

        public InteractionInternalModel Process(
            InteractionInternalModel interaction
            )
        {

            try
            {
                if (interaction.Request.State == null ||
                    interaction.Request.State == RequestStates.Completed ||
                    interaction.Request.ConfirmationStatus == ConfirmationStatus.Confirmed)
                {
                    interaction = GetIntentHandler(interaction.Request.Intent).Process(interaction);
                }
            }
            catch (Exception ex)
            {
                // LOG ERROR TO APP INSIGHTS PERHAPS.
            }
            finally
            {
                if (string.IsNullOrWhiteSpace(interaction.Response.Text))
                {
                    interaction.Response.Text = Speech.UnknownIntentReply;
                    interaction.Session.EndSession = true;
                }
            }

            return interaction;
        }

        /// <summary>
        /// This methods determines the appropriate intent handler class based on the intent name.
        /// For example, it'll look for the WelcomeIntentHandler class to handle the WelcomeIntent.
        /// </summary>
        /// <param name="intentName">Name of the intent.</param>
        /// <returns></returns>
        private IIntentHandler GetIntentHandler(
            string intentName
            )
        {
            // TODO: THINK LATER IF REFLECTION IS THE RIGHT APPROACH BUT IT MOST CERTAINLY
            // ISN'T A RECOMMENDED THING TO DO.
            //string intentHandlerName = $"{intentName}Handler";

            //Assembly assembly = Assembly.GetExecutingAssembly();
            //TypeInfo typeInfo = assembly.DefinedTypes.First(t => t.Name.EndsWith(intentHandlerName));

            Dictionary<string, Type> intentHandlerMapping = new Dictionary<string, Type>
            {
                {Intents.AmazonHelpIntent, typeof(HelpIntentHandler)},
                {Intents.DefaultWelcome, typeof(DefaultWelcomeIntentHandler)},
                {Intents.PlanMyTrip, typeof(PlanMyTripIntentHandler)}
            };

            // LETS RESOLVE THAT TYPE TO GET THE APPROPRIATE HANDLER.
            return (IIntentHandler)ServiceProvider.GetRequiredService(
                intentHandlerMapping[intentName]
                );
        }
    }
}
