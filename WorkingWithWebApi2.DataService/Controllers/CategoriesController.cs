using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Web.Http;
using WorkingWithWebApi2.BusinessServices.Services.Abstract;
using WorkingWithWebApi2.DataService.Controllers.Abstract;
using WorkingWithWebApi2.Models.ViewModels.Categories;

namespace WorkingWithWebApi2.DataService.Controllers
{
    [RoutePrefix("api/categories")]
    public class CategoriesController : BaseApiController
    {
        #region Constructor & Properties

        private ICategoryService service;

        public CategoriesController(ICategoryService service)
        {
            this.service = service;
        }

        #endregion

        #region Endpoints

        // GET: api/categories
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

        // GET: api/categories/5
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

        // DELETE: api/categories/5
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (service.Delete(id))
                {
                    return Ok("Category removed");
                }
                else
                {
                    return BadRequest(Request, "Category could not be removed");
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

        // PUT: api/categories
        [HttpPut]
        [Route("")]
        [Authorize]
        public IHttpActionResult Put(int id, [FromBody] CategoryEditVM model)
        {
            try
            {
                if (service.Update(model))
                {
                    return Ok("Category updated");
                }
                else
                {
                    return BadRequest(Request, "Category could not be updated");
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

        // POST: api/categories
        [HttpPost]
        [Route("")]
        [Authorize]
        public IHttpActionResult Post([FromBody] CategoryEditVM model)
        {
            try
            {
                if (service.Create(model))
                {
                    return Ok("Category created");
                }
                else
                {
                    return BadRequest(Request, "Category could not be created");
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
