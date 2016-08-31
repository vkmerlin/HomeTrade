using HRPortal.Business;
using HRPortal.Model;
using HRPortal.Model.ViewModels;
using HRPortal.Web.Exceptions;
using HRPortal.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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
        public async Task<IHttpActionResult> CreateNewProduct()
        {
            int catId;
            decimal price;
            if (!Int32.TryParse(HttpContext.Current.Request["CategoryId"], out catId) || !decimal.TryParse(HttpContext.Current.Request["Price"], out price)
                || string.IsNullOrEmpty(HttpContext.Current.Request["Name"]) || string.IsNullOrEmpty(HttpContext.Current.Request["Description"]))
                return Content(HttpStatusCode.BadRequest, "Model state invalid");

            try
            {
                await tradeService.CreateProductAsync(GetNewsWithAttachmentsFromRequest(null, catId, price));
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
            var selectedItem = tradeService.GetProductById(item.Id);
            if (selectedItem != null)
            {
                if (selectedItem.Attachments != null && selectedItem.Attachments.Any())
                {
                    foreach (var file in selectedItem.Attachments)
                    {
                        try
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(file.AttachementPath));
                        }
                        catch (Exception exp)
                        {
                            logService.LogFatal(exp.Message);
                            continue;
                        }
                    }
                }
                tradeService.RemoveProduct(item);
            }
            return Ok();
        }

        public IHttpActionResult UpdateProduct(TradeItem item)
        {
            tradeService.UpdateProduct(item);
            return Ok();
        }

        private TradeItem GetNewsWithAttachmentsFromRequest(int? id, int categoryId, decimal price)
        {
            List<TradeItemAttachment> attachedFiles = new List<TradeItemAttachment>();
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(AppConfig.UploadItemImagesTo)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(AppConfig.UploadItemImagesTo));
            }

            for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
                if (i > 4)
                    break;

                var fileContent = HttpContext.Current.Request.Files[i];
                if (!AppConfig.IsAttachmentAcceptable(fileContent.ContentType))
                    throw new ImageException("File format is wrong!");

                if (fileContent.ContentLength > 5242880)
                    throw new ImageException("Image size is invalid");

                if (fileContent != null && fileContent.ContentLength > 0)
                {
                    var fileName = Path.GetRandomFileName();
                    fileName = Path.ChangeExtension(fileName, Path.GetExtension(fileContent.FileName));

                    var path = Path.Combine(HttpContext.Current.Server.MapPath(AppConfig.UploadItemImagesTo), fileName);
                    using (var fileStream = File.Create(path))
                    {
                        fileContent.InputStream.CopyTo(fileStream);
                    }
                    TradeItemAttachment attachedFile = new TradeItemAttachment { AttachementPath = $"{AppConfig.UploadItemImagesTo}{fileName}" };
                    attachedFiles.Add(attachedFile);
                }
            }
            return new TradeItem
            {
                Id = id.HasValue ? id.Value : 0,
                CategoryId = categoryId,
                Name = HttpContext.Current.Request["Name"],
                Description = HttpContext.Current.Request["Description"],
                Price = price,
                CreateDate = DateTime.UtcNow,
                Attachments = attachedFiles
            };
        }
    }
}
