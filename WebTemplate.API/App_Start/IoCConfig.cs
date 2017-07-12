using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dependencies;
using TinyIoC;

namespace WebTemplate.API
{
    public static class IoCConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Get IoC container
            var container = TinyIoCContainer.Current;
            container.AutoRegister(DuplicateImplementationActions.Fail);

            // Set Web API dep resolver
            GlobalConfiguration.Configuration.DependencyResolver = new TinyIocWebApiDependencyResolver(container);
        }
    }

    public class TinyIocWebApiDependencyResolver : IDependencyResolver
    {
        private bool _disposed;
        private TinyIoCContainer _container;

        public TinyIocWebApiDependencyResolver(TinyIoCContainer container)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));

            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            if (_disposed)
                throw new ObjectDisposedException("this", "This scope has already been disposed.");

            return new TinyIocWebApiDependencyResolver(_container.GetChildContainer());
        }

        public object GetService(Type serviceType)
        {
            if (_disposed)
                throw new ObjectDisposedException("this", "This scope has already been disposed.");

            try
            {
                return _container.Resolve(serviceType);
            }
            catch (TinyIoCResolutionException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (_disposed)
                throw new ObjectDisposedException("this", "This scope has already been disposed.");

            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (TinyIoCResolutionException)
            {
                return Enumerable.Empty<object>();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _container.Dispose();

            _disposed = true;
        }
    }
}