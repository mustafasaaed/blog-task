using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.Core.Interfaces;
using Task.Core.Services;
using Task.Data;
using Task.Data.Repositories;
using Task.Entites;
using TaskUI.Mappings;

namespace TaskUI.App_Start
{
    public static class IocBuilder
    {
        internal static void Build()
        {
            var builder = new ContainerBuilder();
            RegisterComponents(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }

        private static void RegisterComponents(ContainerBuilder builder)
        {
            //Controllers regsiteration
            builder.RegisterControllers(typeof(IocBuilder).Assembly);


            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();

            //Repositoeries
            builder.RegisterType<GenericRepository<Post>>().As<IRepository<Post>>();
            builder.RegisterType<GenericRepository<Category>>().As<IRepository<Category>>();
            builder.RegisterType<GenericRepository<Comment>>().As<IRepository<Comment>>();


            //Services 
            builder.RegisterType<GenericService<Post>>().As<IGenericService<Post>>();
            builder.RegisterType<GenericService<Category>>().As<IGenericService<Category>>();
            builder.RegisterType<GenericService<Comment>>().As<IGenericService<Comment>>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();


            //Mapper
            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                var profile = new MappingProfile();
                cfg.AddProfile(profile);
            }));

            builder.Register(ctx =>
            ctx.Resolve<MapperConfiguration>()
            .CreateMapper()).As<IMapper>().InstancePerLifetimeScope();

        }
    }
}