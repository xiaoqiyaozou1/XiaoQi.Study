using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XiaoQi.Study.Model;
using XiaoQi.Study.Model.DTO;

namespace XiaoQi.Study.IService
{
    public interface IMenuInfoService : IBaseService<MenuInfo>
    {
        Task<List<MenuDTO>> GetMenuTree();
    }
}
