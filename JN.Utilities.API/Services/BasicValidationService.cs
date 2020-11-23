using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using JN.Authentication.HelperClasses;
using JN.Authentication.Interfaces;
using JN.Utilities.API.AuthorizationHandlers;
using JN.Utilities.Core.Services;
using Microsoft.AspNetCore.Mvc;
using ChallengeResult = JN.Authentication.HelperClasses.ChallengeResult;

namespace JN.Utilities.API.Services
{



    public class BasicValidationService : IBasicValidationService
    {
        private readonly IUsersService _usersService;

        public BasicValidationService(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public Task<ValidationResult> ValidateUser(string username, string password, string resourceName)
        {

            var userResult = _usersService.GetUser(username, password);


            if (!userResult.Success)
                return Task.FromResult(new ValidationResult()
                    {
                        Success = false,
                        ErrorDescription = "Invalid access details",
                        ErrorCode = (int) ConstantsAuthentication.AuthResult.InvalidUserOrPass
                    }
                );

            var user = userResult.ReturnedObject;

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, user.Email),
            };

 
            if (!string.IsNullOrWhiteSpace(user.Resources) )
            {
                claims.AddRange(user.Resources.Split(';').Select(userRole => new Claim(ClaimTypes.Role, userRole)));
            }

            if(!user.Active)
                return Task.FromResult(new ValidationResult()
                    {
                        Success = false,
                        ErrorDescription = "User not allowed",
                        ErrorCode = (int)ConstantsAuthentication.AuthResult.NotAllowed
                    }
                );

            var res = new ValidationResult()
            {
                Success = true,
                ErrorDescription = "Ok",
                ErrorCode = (int)ConstantsAuthentication.AuthResult.Ok,
                Claims = claims
            };

            return Task.FromResult(res);

        }

        public static Task<ChallengeResult> ChallengeResponse(Exception ex, RequestDetails requestDetails)
        {
            var res = new ChallengeResult();

            switch (ex)
            {
                case null:
                    return Task.FromResult(res);
                case CustomAuthException exception:
                    switch (exception.ErrorCode)
                    {
                        case (int)ConstantsAuthentication.AuthResult.InvalidUserOrPass:
                        case (int)AuthenticationError.AuthenticationFailed:
                            res = GetResult(ex.Message, requestDetails, HttpStatusCode.Unauthorized);
                            break;

                        case (int)ConstantsAuthentication.AuthResult.NotAllowed:
                            res = GetResult(ex.Message, requestDetails, HttpStatusCode.Forbidden);
                            break;

                        case (int)AuthenticationError.MethodNotAllowed:
                            res = GetResult(ex.Message, requestDetails, HttpStatusCode.MethodNotAllowed);
                            break;

                        case (int)AuthenticationError.OtherError:
                            throw exception;

                        default:
                            res.TextToWriteOutput = exception.Message;
                            break;
                    }

                    break;
                default:
                    res.TextToWriteOutput = ex.Message;
                    break;
            }

            return Task.FromResult(res);
        }

        private static ChallengeResult GetResult(string message, RequestDetails requestDetails, HttpStatusCode statusCode)
        {
            var problemDetails = new ProblemDetails
            {
                Instance = requestDetails.Path,
                Status = (int)statusCode,
                Title = "Authentication error. See details.",
                Detail = message,
            };

            var res = new ChallengeResult()
            {
                ContentType = "application/problem+json",
                TextToWriteOutput = JsonSerializer.Serialize(problemDetails),
                StatusCode = (int)statusCode
            };

            return res;
        }
    }
}
