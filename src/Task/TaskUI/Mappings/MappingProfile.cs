using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task.Entites;
using TaskUI.Models.ViewModels;

namespace TaskUI.Mappings
{
    public class MappingProfile : AutoMapper.Profile 
    {
        public MappingProfile()
        {
            // Model => ViewModel
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Post, PostViewModel>().ForMember(c => c.Photo, c => c.Ignore());

            // ViewModel => Model
            CreateMap<Category, CategoryViewModel>();
            CreateMap<PostViewModel, Post>();
        }
    }
}