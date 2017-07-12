using System.Data.Entity;
using System.Web.Mvc;
using AutoMapper;
using TinyIoC;
using WebTemplate.Database;
using WebTemplate.Database.Repositories;
using WebTemplate.IRepositories;
using WebTemplate.IServices;
using WebTemplate.Services;

namespace WebTemplate.MVC
{
    public static class IoCConfig
    {
        public static void Register()
        {
            // Get IoC container
            var container = TinyIoCContainer.Current;

            container.Register<ITestModelService, TestModelService>().AsMultiInstance();
            container.Register<ITestModelRepository, TestModelRepository>().AsMultiInstance();
            container.Register<DbContext, WebTemplateContext>().AsMultiInstance();

            // Set Web API dep resolver
            DependencyResolver.SetResolver(new TinyIocMvcDependencyResolver(container));
        }
    }
}