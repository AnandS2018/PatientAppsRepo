using FluentValidation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.UnitOfWork
{
   public class UnityValidatorFactory: ValidatorFactoryBase
    {
        private readonly IUnityContainer _container;

        public UnityValidatorFactory(IUnityContainer container)
        {
            _container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return _container.Resolve(validatorType) as IValidator;
        }

    }
}
