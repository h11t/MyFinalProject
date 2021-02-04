using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{                                         // Generic Constraint:generikc kısıt
    //class:referans  tip olabilir demek
    //Ientity:Ientity veya IEntity impemente eden sınıflar olabilirdi
    //new():IEntity gelemesin diye new'lenebilir olduğunu söyleyelim
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        List<T> GetAll(Expression<Func<T,bool>> filter=null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
