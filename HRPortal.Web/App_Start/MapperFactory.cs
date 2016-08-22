using AutoMapper;
using HRPortal.Model;
using HRPortal.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRPortal.Web.App_Start
{
    public static class MapperFactory
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<NewsCategories, NewsCategoriesViewModel>();
            });        
        }
    }
}