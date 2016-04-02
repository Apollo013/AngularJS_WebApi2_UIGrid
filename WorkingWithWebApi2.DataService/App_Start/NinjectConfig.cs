using Ninject;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Http.Dependencies;
using WorkingWithWebApi2.BusinessServices.ModelFactories.Abstract;
using WorkingWithWebApi2.BusinessServices.ModelFactories.Concrete;
using WorkingWithWebApi2.BusinessServices.Services.Abstract;
using WorkingWithWebApi2.BusinessServices.Services.Concrete;

namespace WorkingWithWebApi2.DataService
{
    /// <summary>
    /// Ninject Configuration
    /// </summary>
    public static class NinjectConfig
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            // Model Factory Bindings
            kernel.Bind<IProductModelFactory>().To<ProductModelFactory>();
            kernel.Bind<ICategoryModelFactory>().To<CategoryModelFactory>();
            kernel.Bind<ISupplierModelFactory>().To<SupplierModelFactory>();

            // Service Bindings
            kernel.Bind<IProductService>().To<ProductService>();
            kernel.Bind<ICategoryService>().To<CategoryService>();
            kernel.Bind<ISupplierService>().To<SupplierService>();

            return kernel;
        }
    }

    /// <summary>
    /// Ninject resolver for OWIN middleware
    /// </summary>
    public class NinjectResolver : NinjectDependencyScope, IDependencyResolver
    {
        private IKernel kernel;

        public NinjectResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(kernel);
        }
    }

    public class NinjectDependencyScope : IDependencyScope
    {
        private IResolutionRoot resolver;

        internal NinjectDependencyScope(IResolutionRoot resolver)
        {
            Contract.Assert(resolver != null);

            this.resolver = resolver;
        }

        public void Dispose()
        {
            resolver = null;
        }

        public object GetService(Type serviceType)
        {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed");

            return resolver.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed");

            return resolver.GetAll(serviceType);
        }
    }
}
