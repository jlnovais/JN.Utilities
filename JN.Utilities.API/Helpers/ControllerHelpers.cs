using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JN.Utilities.API.Helpers
{
    public static class ControllerHelpers
    {
        public static ActionResult GetGenericProblem(this ControllerBase controller, HttpStatusCode httpStatus, string details)
        {
            //controller.HttpContext.Response.ContentType = "application/problem+json";

            return controller.Problem(
                statusCode: (int)httpStatus,
                detail: details,
                instance: controller.HttpContext.Request.Path,
                title: "Error processing request"
                //type: ""
            );

        }
    }
}
