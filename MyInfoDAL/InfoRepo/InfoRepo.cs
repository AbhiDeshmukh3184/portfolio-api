using Microsoft.EntityFrameworkCore;
using MyInfoCommonUtility.Logger;
using MyInfoDAL.DataModel;
using MyInfoDTOModel.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInfoDAL.InfoRepo
{
    public class InfoRepo : IInfoRepo
    {
        private IMyInfoContext _MyInfoContext;
        private ILoggerManager _loggerManager;

        public InfoRepo(IMyInfoContext MyInfoContext, ILoggerManager loggerManager)
        {
            _MyInfoContext = MyInfoContext;
            _loggerManager = loggerManager;

        }

        public async Task<int> AddInfo(NewTable request)
        {
            _loggerManager.LogInfo("Entry InfoRepo=> AddInfo");
            await _MyInfoContext.NewTable.AddAsync(request);
            await _MyInfoContext.SaveChangesAsync();
            _loggerManager.LogInfo("Exit InfoRepo=> AddInfo");
            return 1;
          
        }

        public async Task<List<NewTable>> GetAllInfo()
        {
            _loggerManager.LogInfo("Entry InfoRepo=> GetAllInfo");
            var result = await _MyInfoContext.NewTable.ToListAsync();
            _loggerManager.LogInfo("Exit InfoRepo=> GetAllInfo");
            return result;
            

        }

        public async Task<NewTable> GetAllInfoById(int id)
        {
            _loggerManager.LogInfo("Entry InfoRepo=> GetAllInfo");
            var result =  await _MyInfoContext.NewTable.Where(x => x.id == id).FirstOrDefaultAsync();
            _loggerManager.LogInfo("Exit InfoRepo=> GetAllInfo");
            return result;
        }

        public async Task<NewTable> UpdateInfo(NewTable request)
        {
            _loggerManager.LogInfo("Entry InfoRepo=> GetAllInfo");
            var result = _MyInfoContext.NewTable.Update(request);
            await _MyInfoContext.SaveChangesAsync();
            _loggerManager.LogInfo("Exit InfoRepo=> GetAllInfo");
            return result.Entity;
        }
    }
}
