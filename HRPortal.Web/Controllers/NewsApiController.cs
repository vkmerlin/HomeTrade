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
    public class NewsApiController : ApiController
    {
        INewsService newsService;
        INLogManager logService;

        public NewsApiController(INewsService newsService, INLogManager logService)
        {
            this.newsService = newsService;
            this.logService = logService;
        }

        public IEnumerable<News> GetNews(int pageNum = 0)
        {
            return newsService.GetNews(pageNum);
        }

        public IEnumerable<News> GetNewsWithPosition(int pageNum)
        {
            return newsService.GetNews(pageNum, true);
        }

        public IEnumerable<NewsCategoriesViewModel> GetCategories()
        {
            return AutoMapper.Mapper.Map<IEnumerable<NewsCategoriesViewModel>>(newsService.GetNewsCategories());
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateNews()
        {
            int catId;
            bool isPin = false;
            if (!Int32.TryParse(HttpContext.Current.Request["NewsCategoryId"], out catId) || !bool.TryParse(HttpContext.Current.Request["IsPin"], out isPin)
                || string.IsNullOrEmpty(HttpContext.Current.Request["Title"]) || string.IsNullOrEmpty(HttpContext.Current.Request["Message"]))
                return Content(HttpStatusCode.BadRequest, "Model state invalid");
            try
            {
                var news = GetNewsWithAttachmentsFromRequest(null, catId, isPin);
                await newsService.CreateNews(news);
            }
            catch (ImageException exp)
            {
                logService.LogError(exp.Message);
                return Content(HttpStatusCode.NotAcceptable, exp.Message);
            }
            catch (Exception exp)
            {
                logService.LogFatal(exp.Message);
                return Content(HttpStatusCode.BadRequest, "An error occurred");
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> EditNews()
        {
            int id;
            int catId;
            bool isPin = false;
            if (!Int32.TryParse(HttpContext.Current.Request["Id"], out id) || !Int32.TryParse(HttpContext.Current.Request["NewsCategoryId"], out catId) || !bool.TryParse(HttpContext.Current.Request["IsPin"], out isPin)
                || string.IsNullOrEmpty(HttpContext.Current.Request["Title"]) || string.IsNullOrEmpty(HttpContext.Current.Request["Message"]))
                return Content(HttpStatusCode.BadRequest, "Model state invalid");
            try
            {
                var news = GetNewsWithAttachmentsFromRequest(id, catId, isPin);
                await newsService.EditNews(news);
            }
            catch (ImageException exp)
            {
                logService.LogError(exp.Message);
                return Content(HttpStatusCode.NotAcceptable, exp.Message);
            }
            catch (Exception exp)
            {
                logService.LogFatal(exp.Message);
                return Content(HttpStatusCode.BadRequest, "An error occurred");
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> RemoveAttachment(NewsAttachedFiles attachment)
        {
            try
            {
                File.Delete(HttpContext.Current.Server.MapPath(attachment.FilePath));
            }
            catch (Exception exp)
            {
                logService.LogFatal(exp.Message);
            }

            await newsService.RemoveAttachment(attachment.Id);
            return Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> RemoveNews(News news)
        {
            var selectedNews = newsService.GetById(news.Id);
            if (selectedNews != null)
            {
                if (selectedNews.AttachedFiles != null && selectedNews.AttachedFiles.Any())
                {
                    foreach (var file in selectedNews.AttachedFiles)
                    {
                        try
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(file.FilePath));
                        }
                        catch (Exception exp)
                        {
                            logService.LogFatal(exp.Message);
                            continue;
                        }
                    }
                }
                await newsService.RemoveNews(selectedNews);
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddNewsComment(NewsComments newReply)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Validation error.");
            }
            newReply.CreateDate = DateTime.UtcNow;
            await newsService.CreateNewsComment(newReply);
            return Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddReply(NewsReply newReply)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Validation error.");
            }
            newReply.CreateDate = DateTime.UtcNow;
            await newsService.CreateNewsReply(newReply);
            return Ok();
        }
        private News GetNewsWithAttachmentsFromRequest(int? id, int catId, bool isPin)
        {
            List<NewsAttachedFiles> attachedFiles = new List<NewsAttachedFiles>();
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(AppConfig.UploadFilesTo)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(AppConfig.UploadFilesTo));
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
                    var fileName = string.Format("{0}{1}", Path.GetRandomFileName(), Path.GetExtension(fileContent.FileName));
                    var path = Path.Combine(HttpContext.Current.Server.MapPath(AppConfig.UploadFilesTo), fileName);
                    using (var fileStream = File.Create(path))
                    {
                        fileContent.InputStream.CopyTo(fileStream);
                    }
                    NewsAttachedFiles attachedFile = new NewsAttachedFiles { FilePath = string.Format("{0}{1}", AppConfig.UploadFilesTo, fileName) };
                    attachedFiles.Add(attachedFile);
                }
            }
            return new News
            {
                Id = id.HasValue ? id.Value : 0,
                NewsCategoryId = catId,
                Title = HttpContext.Current.Request["Title"],
                Message = HttpContext.Current.Request["Message"],
                CreateDate = DateTime.UtcNow,
                IsPin = isPin,
                AttachedFiles = attachedFiles
            };
        }
    }
}
