using AutoMapper;
using HRPortal.Model;
using HRPortal.Model.ViewModels;

namespace HRPortal.Web.App_Start
{
    public static class MapperFactory
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<NewsCategories, NewsCategoriesViewModel>();
                //cfg.CreateMap<NewsCategories, NewsCategoriesViewModel>();
            });        
        }
    }
}