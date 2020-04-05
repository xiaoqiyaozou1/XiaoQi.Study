using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XiaoQi.Study.IRepository
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> Query();

        Task<List<T>> Query(string where);

        Task<T> QueryByWhere(string where);

        Task<T> QueryById(Object id);


        IQueryable<T> GetPageInfos<S>(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> whereLambda);

        Task<bool> Add(T t);

        Task<bool> Update(T t);

        Task<bool> Delete(T t);
    }
}
