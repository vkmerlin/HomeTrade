using HRPortal.Business;
using HRPortal.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace HRPortal.Web.Controllers
{
    public class ItemApiController : ApiController
    {
        ITradeService tradeService;
        INLogManager logService;
        public ItemApiController(ITradeService tradeService, INLogManager logService)
        {
            this.tradeService = tradeService;
            this.logService = logService;
        }

        public IEnumerable<Category> GetCategories()
        {
            return tradeService.GetAllCategories();
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateNewCategory(Category newCategory)
        {
            try
            {
                await tradeService.CreateCategoryAsync(newCategory);
            }
            catch (Exception exp) {
                logService.LogFatal(exp.Message);
                return Content(HttpStatusCode.BadRequest, "An error occurred");
            }
            return Ok();
        }

        public IHttpActionResult RemoveCategory(Category category)
        {
            tradeService.RemoveCategory(category);
            return Ok();
        }

        public IHttpActionResult EditCategory(Category category)
        {
            tradeService.UpdateCategory(category);
            return Ok();
        }        
    }
}
