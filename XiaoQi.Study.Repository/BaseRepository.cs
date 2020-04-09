using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XiaoQi.Study.EF;
using XiaoQi.Study.IRepository;

namespace XiaoQi.Study.Repository
{
    #region where的含义
    //where T: struct T必须是一个结构类型

    //where T : class                                       T必须是一个类(class)类型，不是结构(structure)类型

    //where T : new()                                      T必须要有一个无参构造函数

    //where T : NameOfBaseClass                     T必须继承名为NameOfBaseClass的类

    //where T : NameOfInterface                      T必须实现名为NameOfInterface的接口
    #endregion


    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        private readonly MyContext _myContext;
        public BaseRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> Add(T t)
        {
            await _myContext.Set<T>().AddAsync(t);
            var res = await _myContext.SaveChangesAsync();
            return res > 0;
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> Delete(T t)
        {
            _myContext.Set<T>().Remove(t);
            return await _myContext.SaveChangesAsync() > 0;

        }
        /// <summary>
        /// 得到分页的数据
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderByLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public IQueryable<T> GetPageInfos<S>(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderByLambda,
                                                    bool isAsc)
        {
            total = _myContext.Set<T>().Where(whereLambda).Count();
            if (isAsc)
            {
                var res = _myContext.Set<T>()
                    .Where(whereLambda)
                    .OrderBy<T, S>(orderByLambda)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize).AsQueryable();
                return res;
            }
            else
            {
                var res = _myContext.Set<T>()
                          .Where(whereLambda)
                          .OrderBy<T, S>(orderByLambda)
                          .Skip(pageSize * (pageIndex - 1))
                          .Take(pageSize).AsQueryable();
                return res;
            }


        }

        /// <summary>
        /// 异步查询到一个表的所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> QueryAsync()
        {
            var res = await _myContext.Set<T>().ToListAsync();
            return res;
        }

        public Task<List<T>> Query(string where)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据ID 查询到实体的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> QueryById(object id)
        {
            var res = await _myContext.Set<T>().FindAsync(id);
            return res;
        }
        /// <summary>
        /// 根据linq表达式查询树
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<T> QueryByWhereAsync(Expression<Func<T, bool>> whereExpression)
        {
            var res = await _myContext.Set<T>().Where(whereExpression).FirstOrDefaultAsync();
            return res;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> Update(T t)
        {
            _myContext.Set<T>().Update(t);
            return await _myContext.SaveChangesAsync() > 0;
        }
    }
}
