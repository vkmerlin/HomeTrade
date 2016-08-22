using HRPortal.Business;
using HRPortal.Model;
using HRPortal.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HRPortal.Web.Controllers
{
    public class AdminApiController : ApiController
    {
        ITradeService tradeService;
        INLogManager logService;
        public AdminApiController(ITradeService tradeService, INLogManager logService)
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
            catch (Exception exp)
            {
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

        public CreateProductViewModel GetProducts()
        {
            return new CreateProductViewModel
            {
                Categories = tradeService.GetAllCategories(),
                Products = tradeService.GetAllProducts()
            };
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateNewProduct(TradeItem newItem)
        {
            try
            {
                await tradeService.CreateProductAsync(newItem);
            }
            catch (Exception exp)
            {
                logService.LogFatal(exp.Message);
                return Content(HttpStatusCode.BadRequest, "An error occurred");
            }
            return Ok();
        }

        public IHttpActionResult RemoveProduct(TradeItem item)
        {
            tradeService.RemoveProduct(item);
            return Ok();
        }

        public IHttpActionResult UpdateProduct(TradeItem item)
        {
            tradeService.UpdateProduct(item);
            return Ok();
        }
    }
}
