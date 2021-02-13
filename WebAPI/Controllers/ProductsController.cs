using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entitties.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Loseely coupled
        //naming convention
        //IoC Container  -- Inversion of Control = Bellekteki bir yer içine referanslar koyup kim ihtyac duyuyorsa kullanılmasına denir
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public List<Product> Get()
        {            
            var result = _productService.GetAll();
            return result.Data;
        }
    }
}
