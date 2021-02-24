using Business.Abstract;
using Business.Constants;
using Business.CrossCuttingConcerns;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Valdiation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entitties.Concrete;
using Entitties.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProducManager : IProductService
    {
        IProductDal _productDal;
        Ilogger _logger;

        public ProducManager(IProductDal productDal,Ilogger logger)
        {
            _productDal = productDal;
            _logger = logger;
        }
        //[ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            //business codes
            _logger.Log();
            try
            {
                _productDal.Add(product);
                return new SuccesResult(Messages.ProductAdded);
            }
            catch (Exception e)
            {

                _logger.Log();
            }
            return new ErrorResult();
        }

        public IDataResult<List<Product>> GetAll()
        {
            //iş kodları
            //yetkisi var mı?
            if (DateTime.Now.Hour == 1)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenancaTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
    }
}
