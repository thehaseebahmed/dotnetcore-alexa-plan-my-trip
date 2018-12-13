using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using PlanMyTrip.AlexaLambda.Helpers;
using PlanMyTrip.Library;
using PlanMyTrip.Library.Services;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace PlanMyTrip.AlexaLambda
{
    public class Function
    {
        private readonly IIntentRouterService _routerService;

        public Function()
        {
            // 1. REGISTERING SERVICES
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<PlanMyTripLibraryModule>();

            // 2. BUILDING CONTAINER AND REPLACING
            IContainer container = containerBuilder.Build();
            var serviceProvider = new AutofacServiceProvider(container);

            // TODO: AVOID THIS new, USE THE serviceProvider INSTEAD.
            _routerService = new IntentRouterService(serviceProvider);
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            var commonModel = InteractionModelMapper.FromAlexaRequest(input);
            if (commonModel == null) { return null; }

            commonModel = _routerService.Process(commonModel);

            return InteractionModelMapper.ToAlexaResponse(commonModel);
        }
    }
}
