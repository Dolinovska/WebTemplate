using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
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
            container.Register<DbContext, ProjectContext>().AsMultiInstance();


            // Set Web API dep resolver
            DependencyResolver.SetResolver(new TinyIocMvcDependencyResolver(container));
        }
    }

    public class TinyIocMvcDependencyResolver : IDependencyResolver
    {
        private readonly TinyIoCContainer _container;

        public TinyIocMvcDependencyResolver(TinyIoCContainer container)
        {
            _container = container;
        }
        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType, true);
            }
            catch (Exception)
            {
                return Enumerable.Empty<object>();
            }
        }
    }
}