using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity,TContext>:IEntityRepository<TEntity> where TEntity:class,IEntity, new()
        where TContext:DbContext,new()
    {
        public void Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                //entry: veri kaynağından gönderdiğim productı bir tane nesneye eşleştir. bu ekleme olacağı için eşleştiremeyecek, direk eklenecek. onu da state ile belirtirim.
                var addedEntity = context.Entry(entity);//referansı yakala
                addedEntity.State = EntityState.Added;//o eklenecek bir nesne
                context.SaveChanges();//şimdi ekle,kaydet
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                //entry: veri kaynağından gönderdiğim productı bir tane nesneyle eşleştir. 
                var deletedEntity = context.Entry(entity);//referansı yakala
                deletedEntity.State = EntityState.Deleted;//o silinecek bir nesne
                context.SaveChanges();//şimdi sil,kaydet
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                //entry: veri kaynağından gönderdiğim productı bir tane nesneyle eşleştir. 
                var updatedEntity = context.Entry(entity);//referansı yakala
                updatedEntity.State = EntityState.Modified;//o değiştirilecek bir nesne
                context.SaveChanges();//şimdi değiştir,kaydet
            }
        }
    }
}
