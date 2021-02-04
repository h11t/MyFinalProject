using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFProductDal : IProductDal
    {

        public void Add(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                //entry: veri kaynağından gönderdiğim productı bir tane nesneye eşleştir. bu ekleme olacağı için eşleştiremeyecek, direk eklenecek. onu da state ile belirtirim.
                var addedEntity = context.Entry(entity);//referansı yakala
                addedEntity.State = EntityState.Added;//o eklenecek bir nesne
                context.SaveChanges();//şimdi ekle,kaydet
            }
        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                //entry: veri kaynağından gönderdiğim productı bir tane nesneyle eşleştir. 
                var deletedEntity = context.Entry(entity);//referansı yakala
                deletedEntity.State = EntityState.Deleted;//o silinecek bir nesne
                context.SaveChanges();//şimdi sil,kaydet
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return filter == null ? context.Set<Product>().ToList() : context.Set<Product>().Where(filter).ToList();
            }
        }

        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                //entry: veri kaynağından gönderdiğim productı bir tane nesneyle eşleştir. 
                var updatedEntity = context.Entry(entity);//referansı yakala
                updatedEntity.State = EntityState.Modified;//o değiştirilecek bir nesne
                context.SaveChanges();//şimdi değiştir,kaydet
            }
        }
    }
}
