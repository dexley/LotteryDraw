using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.MicroKernel;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace SIS.Lottery.Api.WindsorConfiguration
{
    public class ChildDependencyScope : IDependencyScope
    {
        private readonly IParentDependencyScope _parentScope;

        private ConcurrentBag<object> _serviceInstances = new ConcurrentBag<object>();

        public ChildDependencyScope(IParentDependencyScope parentScope)
        {
            if (parentScope == null)
            {
                throw new ArgumentNullException("parentScope");
            }

            _parentScope = parentScope;
        }

        public void Dispose()
        {
            if (_serviceInstances != null)
            {
                foreach (var service in _serviceInstances)
                {
                    _parentScope.ReleaseService(service);
                }
            }
            _serviceInstances = null;
        }

        public object GetService(Type serviceType)
        {
            return RegisterForRelease(_parentScope.GetService(serviceType));
        }


        protected IEnumerable<object> RegisterForRelease(IEnumerable<object> services)
        {
            var registerForRelease = services as IList<object> ?? services.ToList();

            foreach (var service in registerForRelease)
            {
                RegisterForRelease(service);
            }

            return registerForRelease;
        }

        protected object RegisterForRelease(object service)
        {
            if (service != null)
            {
                _serviceInstances.Add(service);
            }

            return service;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return RegisterForRelease(_parentScope.GetServices(serviceType));
        }
    }

    public interface IParentDependencyScope : IDependencyScope
    {
        void ReleaseService(object service);
    }

    public class WindsorDependencyResolver : IDependencyResolver, IParentDependencyScope
    {
        protected IKernel Kernel { get; private set; }

        public WindsorDependencyResolver(IKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }

            Kernel = kernel;
        }

        public virtual void Dispose()
        {
            Kernel.Dispose();
        }

        public object GetService(Type serviceType)
        {
            if (!Kernel.HasComponent(serviceType))
            {
                return null;
            }

            return Kernel.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (!Kernel.HasComponent(serviceType))
            {
                return new object[0];
            }

            return Kernel.ResolveAll(serviceType).Cast<object>();
        }

        public void ReleaseService(object service)
        {
            Kernel.ReleaseComponent(service);
        }

        public IDependencyScope BeginScope()
        {
            return new ChildDependencyScope(this);
        }
    }
}