using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Web.Http;
using WorkingWithWebApi2.BusinessServices.Services.Abstract;
using WorkingWithWebApi2.DataService.Controllers.Abstract;
using WorkingWithWebApi2.Models.ViewModels.Products;

namespace WorkingWithWebApi2.DataService.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : BaseApiController
    {
        #region Constructor & Properties      

        private IProductService service;

        public ProductsController(IProductService productService)
        {
            this.service = productService;
        }

        #endregion

        #region Endpoints

        // GET: api/products
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get(int pageNo = 1, int pageSize = 25, string orderBy = null, string filter = null)
        {                        
            try                
            {
                return Ok(service.GetPageResult(pageNo, pageSize, orderBy, filter));
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

        // GET: api/products/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(service.GetById(id));
            }
            catch(NullReferenceException ex)
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

        // DELETE: api/products/5
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (service.Delete(id))
                {
                    return Ok("Product removed");
                }
                else
                {
                    return BadRequest(Request, "Product could not be removed");
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

        // PUT: api/products
        [HttpPut]
        [Route("")]
        [Authorize]
        public IHttpActionResult Put([FromBody] ProductEditVM model)
        {
            try
            {
                if (service.Update(model))
                {
                    return Ok("Product updated");
                }
                else
                {
                    return BadRequest(Request, "Product could not be updated");
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

        // POST: api/products
        [HttpPost]
        [Route("")]
        [Authorize]
        public IHttpActionResult Post(int id, [FromBody] ProductEditVM model)
        {
            try
            {
                if(service.Create(model))
                {
                    return Ok("Product created");
                }
                else
                {
                    return BadRequest(Request, "Product could not be created");
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
