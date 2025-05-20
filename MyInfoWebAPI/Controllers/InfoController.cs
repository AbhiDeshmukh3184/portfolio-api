using Microsoft.AspNetCore.Mvc;
using MyInfoBAL.MyInfo;
using MyInfoCommonUtility.Logger;
using MyInfoCommonUtility.Response;
using MyInfoDTOModel.Info;

namespace MyInfoWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IInfoService _infoService;

        public InfoController(ILoggerManager loggerManager, IInfoService infoService)
        {
            _loggerManager = loggerManager;
            _infoService = infoService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddInfo(InfoRequestDTO request)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };
            if (request == null)
            {
                resultWithDataDTO.IsSuccessful = false;
                resultWithDataDTO.BusinessErrorMessage = "Error, No data provided";
                return BadRequest(resultWithDataDTO);
            }

            _loggerManager.LogInfo("Entry InfoController=> AddInfo");
            resultWithDataDTO = await _infoService.AddInfo(request);
            _loggerManager.LogInfo("Exit InfoController=> AddInfo");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }

        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetInfoList()
        {
            ResultWithDataDTO<List<InfoResponseDTO>> resultWithDataDTO =
                new ResultWithDataDTO<List<InfoResponseDTO>> { IsSuccessful = false };
            _loggerManager.LogInfo("Entry InfoController=> GetInfoList");
            resultWithDataDTO = await _infoService.GetAllInfo();
            _loggerManager.LogInfo("Exit InfoController=> GetInfoList");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> getInfoByID(int id)
        {
            ResultWithDataDTO<InfoResponseDTO> resultWithDataDTO =
                new ResultWithDataDTO<InfoResponseDTO> { IsSuccessful = false };
            if (id == 0)
            {
                resultWithDataDTO.IsSuccessful = false;
                resultWithDataDTO.BusinessErrorMessage = "Error, No data provided";
                return BadRequest(resultWithDataDTO);
            }

            _loggerManager.LogInfo("Entry InfoController=> AddInfo");
            resultWithDataDTO = await _infoService.getInfoById(id);
            _loggerManager.LogInfo("Exit InfoController=> AddInfo");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }

        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> updateInfo(InfoUpdateRequestDTO requestDTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };
            if (requestDTO == null)
            {
                resultWithDataDTO.IsSuccessful = false;
                resultWithDataDTO.BusinessErrorMessage = "Error, No data provided";
                return BadRequest(resultWithDataDTO);
            }

            _loggerManager.LogInfo("Entry InfoController=> updateInfo");
            resultWithDataDTO = await _infoService.updateInfo(requestDTO);
            _loggerManager.LogInfo("Exit InfoController=> updateInfo");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }

        }
    }
}
