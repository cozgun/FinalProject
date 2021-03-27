using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]  // c#:attribute   java:annotation  bir class ile ilgili bilgi verme, onu imzalama.
    public class ProductsController : ControllerBase
    {
        //Loose coupling/loosely coupled.  Gevşek bağımlılık.  (abstract a bağımlı)
        // IoC Container -- Inversion of control
        IProductService _productService;  // buna field dedi.  bunu araştıralım.

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            // Dependency chain-- IProductService ProductManager a ihtiyaç duyuyor.  O da EfProductDal a.  
            //IProductService productService = new ProductManager(new EfProductDal());

            Thread.Sleep(1000);
            
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest (result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        //interntional programming.  controllerdan kodlamaya başlıyoruz
        [HttpGet("getbycategory")]
        public IActionResult GetByCategory(int categoryId)
        {
            var result = _productService.GetAllByCategoryId(categoryId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }


        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
