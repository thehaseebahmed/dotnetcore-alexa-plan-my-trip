using Alexa.NET.Request;
using Alexa.NET.Response;
using Microsoft.AspNetCore.Mvc;
using PlanMyTrip.AlexaApi.Helpers;
using PlanMyTrip.Library.Services;

namespace PlanMyTrip.AlexaApi.Controllers
{
    [Route("v1/api")]
    public class DefaultController : Controller
    {
        private readonly IIntentRouterService _routerService;

        public DefaultController(
            IIntentRouterService routerService
        )
        {
            _routerService = routerService;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(SkillResponse), 200)]
        public SkillResponse Post(
            [FromBody]SkillRequest request
        )
        {
            var commonModel = InteractionModelMapper.FromAlexaRequest(request);
            if (commonModel == null) { return null; }

            commonModel = _routerService.Process(commonModel);

            return InteractionModelMapper.ToAlexaResponse(commonModel);
        }
    }
}
