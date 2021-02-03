using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            ProducManager producManager = new ProducManager(new EfProductDal());

            foreach (var product in producManager.GetAll())
            {
                Console.WriteLine(product.ProductName);
            }
        }
    }
}
