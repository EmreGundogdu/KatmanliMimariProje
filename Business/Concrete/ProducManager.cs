using Business.Abstract;
using Business.Constants;
using Business.CrossCuttingConcerns;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Valdiation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entitties.Concrete;
using Entitties.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//iş kurallarında aynı kodlar metod içerisinde  yazılmaz
namespace Business.Concrete
{
    public class ProducManager : IProductService
    {
        IProductDal _productDal;
        Ilogger _logger;

        public ProducManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        //[ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product) //business codes        
        {
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName), CheckIfProductCountOfCategoryCorrect(product.CategoryId));
            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccesResult(Messages.ProductAdded);



        }

        public IDataResult<List<Product>> GetAll()
        {
            //iş kodları
            //yetkisi var mı?
            if (DateTime.Now.Hour == 1)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenancaTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
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
        [ValidationAspect(typeof(ProductValidator)]
        public IResult Update(Product product)
        {
            var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
            if (result > 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            throw new NotImplementedException();
        }
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            //bir kategoride en fazla 10 ürün olabilir
            //select count(*) from products where categoryId=1
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result > 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccesResult();
        }
        private IResult CheckIfProductNameExists(string productName)
        {
            //bir kategoride en fazla 10 ürün olabilir
            //select count(*) from products where categoryId=1
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccesResult();
        }
    }
}
