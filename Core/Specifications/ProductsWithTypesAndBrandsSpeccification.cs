using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpeccification : BaseSpecifcation<Product>
    {
        public ProductsWithTypesAndBrandsSpeccification(string sort)
        {
            AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
            AddOrderBy(x=>x.Name);
            if (!string.IsNullOrEmpty(sort)){
                switch (sort){
                    case "priceAsc":
                    AddOrderBy(p=>p.Price);
                    break;
                    case "priceDesc":
                        AddOrderByDesc(p=>p.Price);
                        break;
                    default:
                        AddOrderBy(p=>p.Name);
                        break;
                }
            }
        }
        public ProductsWithTypesAndBrandsSpeccification(int id):base(x=>x.Id==id)
        {
            AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
        }
    }
}