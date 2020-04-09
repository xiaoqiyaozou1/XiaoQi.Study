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
        Task<List<T>> QueryAsync();

        Task<List<T>> Query(string where);

        Task<T> QueryByWhereAsync(Expression<Func<T, bool>> whereExpression);

        Task<T> QueryById(Object id);


        IQueryable<T> GetPageInfos<S>(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderByLambda,
                                                    bool isAsc);

        Task<bool> Add(T t);

        Task<bool> Update(T t);

        Task<bool> Delete(T t);
    }
}
