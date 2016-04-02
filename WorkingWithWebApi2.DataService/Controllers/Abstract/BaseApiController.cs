using log4net;
using System;
using System.Net.Http;
using System.Web.Http;
using WorkingWithWebApi2.DataService.Helpers.ActionResults;

namespace WorkingWithWebApi2.DataService.Controllers.Abstract
{
    /// <summary>
    /// Base controller class that contains members available for use by all controllers
    /// </summary>
    public class BaseApiController : ApiController, IDisposable
    {
        #region Constructor
        public BaseApiController()
        {
            type = this.GetType();
        }

        #endregion

        #region Logging

        private static Type type;
        private static ILog logger;

        protected static ILog Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = LogManager.GetLogger(type);
                }
                return logger;
            }
        }

        /// <summary>
        /// Creates a formatted exception message for logging
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected static string LogMessage(Exception ex)
        {
            return $"{ex.Source}: {ex.Message}";
        }

        #endregion

        #region Custom Error Action Results
        protected static NotFoundActionResult NotFound(HttpRequestMessage request, string message)
        {
            return new NotFoundActionResult(request, message);
        }

        protected static NotImplementedActionResult NotImplemented(HttpRequestMessage request, string message)
        {
            return new NotImplementedActionResult(request, message);
        }

        protected static BadRequestActionResult BadRequest(HttpRequestMessage request, string message)
        {
            return new BadRequestActionResult(request, message);
        }

        protected static InternalServerErrorActionResult InternalServerError(HttpRequestMessage request, string message)
        {
            return new InternalServerErrorActionResult(request, message);
        }
        #endregion
    }
}
