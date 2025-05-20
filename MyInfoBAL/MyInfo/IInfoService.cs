using MyInfoCommonUtility.Response;
using MyInfoDTOModel.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInfoBAL.MyInfo
{
    public interface IInfoService
    {
        Task<ResultWithDataDTO<List<InfoResponseDTO>>> GetAllInfo();
        Task<ResultWithDataDTO<int>> AddInfo(InfoRequestDTO requestDTO);
        Task<ResultWithDataDTO<InfoResponseDTO>> getInfoById(int id);
        Task<ResultWithDataDTO<int>> updateInfo(InfoUpdateRequestDTO requestDTO);
    }
}
