using Ninject;
using SmartCity.Domain.Abstract;
using SmartCity.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartCity.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kennel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kennel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kennel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kennel.GetAll(serviceType);
        }
        //绑定
        public void AddBindings()
        {
            //Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>
            //{
            //    new Product { Name="Football",Price=25},
            //    new Product { Name="Surf board",Price=179},
            //    new Product { Name="Running shoes", Price=95}
            //});
            //kennel.Bind<IProductsRepository>().ToConstant(mock.Object);
            //kennel.Bind<IProductsRepository>().To<EFProductRepository>();


            //EmailSettings emailSettings = new EmailSettings
            //{
            //    WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            //};
            //kennel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
            // .WithConstructorArgument("settings", emailSettings);

            //kennel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            kennel.Bind<IProductCheshi>().To<ProductCheshi>();
            kennel.Bind<IUserInfo>().To<UserInfo>();
            kennel.Bind<IManagerInfo>().To<ManagerInfo>();
        }
    }
}