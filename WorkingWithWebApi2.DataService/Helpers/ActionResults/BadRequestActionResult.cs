﻿using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace WorkingWithWebApi2.DataService.Helpers.ActionResults
{
    /// <summary>
    /// Custom Action Result That Overloads The BadRequest() Action Result to return a plain text message within the response body.
    /// </summary>
    public class BadRequestActionResult : IHttpActionResult
    {
        public string Message { get; private set; }
        public HttpRequestMessage Request { get; private set; }

        public BadRequestActionResult(HttpRequestMessage request, string message)
        {
            this.Request = request;
            this.Message = message;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(ExecuteResult());
        }

        public HttpResponseMessage ExecuteResult()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);            
            response.Content = new StringContent(Message);
            response.RequestMessage = Request;
            return response;
        }
    }
}
