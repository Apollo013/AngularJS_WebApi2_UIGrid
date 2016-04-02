using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;

namespace WorkingWithWebApi2.DataService.Helpers.ActionFilterAttributes
{
    /// <summary>
    /// Custom Action Filter used to validate models 
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                //var errors = string.Join(",", GetErrorListFromModelState(actionContext.ModelState));
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }

        /// <summary>
        /// Grabs a list of error or exception messages
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        [Obsolete]
        private List<string> GetErrorListFromModelState(ModelStateDictionary modelState)
        {
            return modelState.Values
                .SelectMany(v => v.Errors)
                .Select(v => v.ErrorMessage + " " + v.Exception)
                .Distinct()
                .ToList();            
        }
    }
}