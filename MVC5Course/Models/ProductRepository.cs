using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        public override IQueryable<Product> All()
        {
            return base.All().Where(p => p.IsDeleted == false);
        }
        public Product Find(int id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }
        public IQueryable<Product> GetAllData_with_Head(int size)
        {
            return this.All().OrderByDescending(p => p.ProductId).Take(size);
        }
        public override void Delete(Product product)
        {
            product.IsDeleted = true;
        }
    }

	public  interface IProductRepository : IRepository<Product>
	{

	}
}