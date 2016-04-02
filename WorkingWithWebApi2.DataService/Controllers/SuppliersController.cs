using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Web.Http;
using WorkingWithWebApi2.BusinessServices.Services.Abstract;
using WorkingWithWebApi2.DataService.Controllers.Abstract;
using WorkingWithWebApi2.Models.ViewModels.Suppliers;

namespace WorkingWithWebApi2.DataService.Controllers
{
    [RoutePrefix("api/suppliers")]
    public class SuppliersController : BaseApiController
    {
        #region Constructor & Properties

        private ISupplierService service;

        public SuppliersController(ISupplierService service)
        {
            this.service = service;
        }

        #endregion

        #region Endpoints

        // GET: api/suppliers
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get(string orderBy = null, string filter = null)
        {
            try
            {
                return Ok(service.GetAll(orderBy, filter));
            }
            catch (NullReferenceException ex)
            {
                Logger.Info(LogMessage(ex));
                return NotFound(Request, ex.Message);
            }
            catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is ArgumentException)
            {
                Logger.Info(LogMessage(ex));
                return BadRequest(Request, ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error(LogMessage(ex));
                return InternalServerError(Request, ex.Message);
            }
        }

        // GET: api/suppliers/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(service.GetById(id));
            }
            catch (NullReferenceException ex)
            {
                Logger.Info(LogMessage(ex));
                return NotFound(Request, ex.Message);
            }
            catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is InvalidOperationException)
            {
                Logger.Info(LogMessage(ex));
                return BadRequest(Request, ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error(LogMessage(ex));
                return InternalServerError(Request, ex.Message);
            }
        }

        // DELETE: api/suppliers/5
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (service.Delete(id))
                {
                    return Ok("Supplier removed");
                }
                else
                {
                    return BadRequest(Request, "Supplier could not be removed");
                }
            }
            catch (Exception ex) when (ex is DbEntityValidationException || ex is NullReferenceException)
            {
                Logger.Info(LogMessage(ex));
                return BadRequest(Request, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                Logger.Warn(LogMessage(ex));
                return BadRequest(Request, ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error(LogMessage(ex));
                return InternalServerError(Request, ex.Message);
            }
        }

        // PUT: api/suppliers
        [HttpPut]
        [Route("")]
        [Authorize]
        public IHttpActionResult Put(int id, [FromBody] SupplierEditVM model)
        {
            try
            {
                if (service.Update(model))
                {
                    return Ok("Supplier updated");
                }
                else
                {
                    return BadRequest(Request, "Supplier could not be updated");
                }
            }
            catch (Exception ex) when (ex is DbEntityValidationException || ex is NullReferenceException)
            {
                Logger.Info(LogMessage(ex));
                return BadRequest(Request, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                Logger.Warn(LogMessage(ex));
                return BadRequest(Request, ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error(LogMessage(ex));
                return InternalServerError(Request, ex.Message);
            }
        }

        // POST: api/suppliers
        [HttpPost]
        [Route("")]
        [Authorize]
        public IHttpActionResult Post([FromBody] SupplierEditVM model)
        {
            try
            {
                if (service.Create(model))
                {
                    return Ok("Supplier created");
                }
                else
                {
                    return BadRequest(Request, "Supplier could not be created");
                }
            }
            catch (Exception ex) when (ex is DbEntityValidationException || ex is DbUpdateException)
            {
                Logger.Info(LogMessage(ex));
                return BadRequest(Request, ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error(LogMessage(ex));
                return InternalServerError(Request, ex.Message);
            }
        }

        #endregion
    }
}
