using System;
using System.Net.Mime;
using AutoMapper;
using JN.Authentication.Scheme;
using JN.Utilities.API.Helpers;
using JN.Utilities.Contracts.V1.Requests;
using JN.Utilities.Contracts.V1.Responses;
using JN.Utilities.Core.Entities;
using JN.Utilities.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JN.Utilities.API.Controllers.V1
{
    /// <summary>
    /// Quantity optimization problem - find the optimal quantity of items to buy with a given amount of money.
    /// </summary>
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = BasicAuthenticationDefaults.AuthenticationScheme, Policy = "OptimizationAccess")]
    [ApiController]
    public class OptimizationController : ControllerBase
    {
        private readonly ISolverService _solverService;
        private readonly IMapper _mapper;

        public OptimizationController(ISolverService solverService, IMapper mapper)
        {
            _solverService = solverService;
            _mapper = mapper;
        }


        /// <summary>
        /// List methods available for this resource.
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpOptions]
        public IActionResult GetOptions()
        {
            //Response.Headers.Add("Allow", "GET,OPTIONS,POST,DELETE,PUT,PATCH");
            Response.Headers.Add("Allow", "GET,POST");
            return Ok();
        }

        /// <summary>
        /// Calculate a solution for a given problem
        /// </summary>
        /// <param name="definition"></param>
        /// <returns></returns>
        /// <response code="200">Returns a solution to the problem.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="422">Validation errors</response>
        /// <response code="401">Unauthorized</response>
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesCustom(MediaTypeNames.Application.Json, "application/problem+json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public ActionResult<Solution> Post([FromBody] ProblemDefinition definition)
        {
            var problemConfiguration = _mapper.Map<ProblemConfiguration>(definition);

            try
            {
                var solution = _solverService.Solve(problemConfiguration);

                var res = _mapper.Map<Solution>(solution);

                res.Id = Guid.NewGuid().ToString();

                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get a previous solution by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = BasicAuthenticationDefaults.AuthenticationScheme, Policy = "OptimizationReaderAccess")]
        [HttpGet("{id}")]
        public ActionResult<Solution> Get([FromRoute]string id)
        {
            return Ok("Not implemented");
        }


    }
}
