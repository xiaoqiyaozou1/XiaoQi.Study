using System;
using System.Collections.Generic;
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
        public async Task<bool> Add(T t)
        {
            await _myContext.AddAsync(t);
            var res = await _myContext.SaveChangesAsync();
            return res > 0;
        }

        public Task<bool> Delete(T t)
        {
            throw new NotImplementedException();
        }

        public System.Linq.IQueryable<T> GetPageInfos<S>(int pageIndex, int pageSize, out int total, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> Query()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> Query(string where)
        {
            throw new NotImplementedException();
        }

        public Task<T> QueryById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<T> QueryByWhere(string where)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(T t)
        {
            throw new NotImplementedException();
        }
    }
}
