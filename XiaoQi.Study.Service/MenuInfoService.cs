using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XiaoQi.Study.IRepository;
using XiaoQi.Study.IService;
using XiaoQi.Study.Model;
using XiaoQi.Study.Model.DTO;

namespace XiaoQi.Study.Service
{
    public class MenuInfoService : BaseService<MenuInfo>, IMenuInfoService
    {
        private readonly IMenuInfoRepository _mneuInfoRepository;
        public MenuInfoService(IMenuInfoRepository menuInfoRepository)
        {
            _mneuInfoRepository = menuInfoRepository;
            this._baseRepository = _mneuInfoRepository;
        }

        public async Task<List<MenuDTO>> GetMenuTree()
        {
            var menuInfos = await _mneuInfoRepository.QueryAsync();

            var orderData = menuInfos.OrderBy(o => o.FatherId).ToList();
            //这个数据本应该查出来就是这个样子得，但是问题设计得时候没考虑好，先这样处理一下，有空再改
            var resData = new List<MenuDTO>();
            foreach (var item in orderData)
            {
                resData.Add(new MenuDTO
                {
                    menuInfoId = item.MenuInfoId,
                    menuName = item.MenuName,
                    selfId = item.SelfId,
                    fatherId = item.FatherId,
                    menuUrl = item.MenuUrl,
                });
            }
            var finalData = new List<MenuDTO>();
            foreach (var item in resData)
            {
                if (item.fatherId == "0")
                {
                    finalData.Add(item);
                }
                else{
                    var data = resData.Where(o => o.selfId == item.fatherId).FirstOrDefault();
                    if (data.children==null)
                    {
                        data.children = new List<MenuDTO>();
                    }                    
                    data.children.Add(item);

                }
            }

            return finalData;
        }

        public override void SetBaseRepository()
        {
            this._baseRepository = _mneuInfoRepository;
        }
    }
}
