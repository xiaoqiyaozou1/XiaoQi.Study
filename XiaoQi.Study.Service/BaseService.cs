using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XiaoQi.Study.IRepository;
using XiaoQi.Study.IService;

namespace XiaoQi.Study.Service
{
    public abstract class BaseService<T> : IBaseService<T>
    {
        public  IBaseRepository<T> _baseRepository;

        public BaseService()
        {
            SetBaseRepository();
        }
        public abstract void SetBaseRepository();//强制子类实现赋值
        public async Task<bool> Add(T t)
        {
            return await _baseRepository.Add(t);
        }

        public async Task<bool> Delete(T t)
        {
            return await _baseRepository.Delete(t);
        }

        public IQueryable<T> GetPageInfos<S>(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderByLambda, bool isAsc)
        {
            return _baseRepository.GetPageInfos<S>(pageIndex, pageSize, out total, whereLambda, orderByLambda, isAsc);
        }

        public Task<List<T>> Query(string where)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> QueryAsync()
        {
            return await _baseRepository.QueryAsync();
        }

        public async Task<T> QueryById(object id)
        {
            return await _baseRepository.QueryById(id);
        }

        public async Task<T> QueryByWhereAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await _baseRepository.QueryByWhereAsync(whereExpression);
        }

        public async Task<bool> Update(T t)
        {
            return await _baseRepository.Update(t);
        }
    }
}
