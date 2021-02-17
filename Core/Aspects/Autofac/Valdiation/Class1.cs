using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Valdiation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); // reflection - çalışma anında bazı şeyleri çalıştırmamızı sağlar
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //product validatorun generic typeini bul 
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); //parametrelerini bul ve validatorun tipine eşit olanları bul
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);//validaton toolu kullanarak validate et demek
            }
        }
    }
}
