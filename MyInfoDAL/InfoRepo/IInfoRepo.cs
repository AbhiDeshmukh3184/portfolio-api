using MyInfoDAL.DataModel;
using MyInfoDTOModel.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInfoDAL.InfoRepo
{
    public interface IInfoRepo
    {
        Task<List<DataModel.NewTable>> GetAllInfo();
        Task<int> AddInfo(NewTable request);
        Task<DataModel.NewTable> GetAllInfoById(int id);
        Task<NewTable> UpdateInfo(NewTable request);
    }
}
