using AutoMapper;
using iText.Kernel.Pdf.Filters;
using MyInfoCommonUtility.Logger;
using MyInfoCommonUtility.Response;
using MyInfoDAL.EmailRepo;
using MyInfoDAL.InfoRepo;
using MyInfoDTOModel.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace MyInfoBAL.MyInfo
{
    public class InfoService : IInfoService
    {
        readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        private readonly IInfoRepo _infoRepo;
        private readonly IEmailRepository _emailRepository;
        public InfoService(ILoggerManager loggerManager, IMapper mapper, IInfoRepo infoRepo, IEmailRepository emailRepository)
        {
            _loggerManager = loggerManager;
            _mapper = mapper;
            _infoRepo = infoRepo;
            _emailRepository = emailRepository;
        }

        public async Task<ResultWithDataDTO<List<InfoResponseDTO>>> GetAllInfo()
        {
            _loggerManager.LogInfo("Entry InfoService=> GetAllInfo");
            ResultWithDataDTO<List<InfoResponseDTO>> resultWithDataBO = new ResultWithDataDTO<List<InfoResponseDTO>>
            {
                IsSuccessful = false
            };
            try
            {
                var dataResult = await _infoRepo.GetAllInfo();
                if (dataResult != null)
                {
                    resultWithDataBO.Data = _mapper.Map<List<InfoResponseDTO>>(dataResult);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"Info Retrieved Successfully.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
                else
                {
                    resultWithDataBO.IsBusinessError = true;
                    resultWithDataBO.BusinessErrorMessage = $"Failed to Save Product Data'.\nKindly retry or contact System Administrator.";
                    _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                }

            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to add Product Detail-Error observed.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"InfoService => GetInfo: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message}";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                if (resultWithDataBO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(resultWithDataBO.SystemErrorMessage, $"GetProductList");
                }
            }
            return resultWithDataBO;
        }


    public async Task<ResultWithDataDTO<int>> AddInfo(InfoRequestDTO requestDTO)
        {
            _loggerManager.LogInfo("Entry InfoService=> AddInfo");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            try
            {
                var newData = _mapper.Map<MyInfoDAL.DataModel.NewTable>(requestDTO);
                var dataResult = await _infoRepo.AddInfo(newData);
                if (dataResult == 1)
                {
                    resultWithDataBO.Data = _mapper.Map<int>(dataResult);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"Info Added Successfully.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
                else
                {
                    resultWithDataBO.IsBusinessError = true;
                    resultWithDataBO.BusinessErrorMessage = $"Failed to Save Info Data'.\nKindly retry or contact System Administrator.";
                    _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                }

            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to add Info Detail-Error observed.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"InfoService => AddInfo: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message}";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                if (resultWithDataBO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(resultWithDataBO.SystemErrorMessage, $"GetProductList");
                }
            }
            return resultWithDataBO;
        }

        public async Task<ResultWithDataDTO<InfoResponseDTO>> getInfoById(int id)
        {
            _loggerManager.LogInfo("Entry InfoService=> AddInfo");
            ResultWithDataDTO<InfoResponseDTO> resultWithDataBO = new ResultWithDataDTO<InfoResponseDTO>
            {
                IsSuccessful = false
            };
            if(id == 0)
            {
                resultWithDataBO.IsSuccessful = false;
                resultWithDataBO.BusinessErrorMessage = "Id not found";
                _loggerManager.LogInfo(resultWithDataBO.Message);
            }

            try
            {

                var dataResult = await _infoRepo.GetAllInfoById(id);
                if (dataResult != null)
                {
                    resultWithDataBO.Data = _mapper.Map<InfoResponseDTO>(dataResult);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"Info Retrived Successfully.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
                else
                {
                    resultWithDataBO.Data = _mapper.Map<InfoResponseDTO>(dataResult);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"No data found for this id : {id}";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }

            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to get Info Detail-Error observed.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"InfoService => getInfoById: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message}";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                if (resultWithDataBO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(resultWithDataBO.SystemErrorMessage, $"GetProductList");
                }
            }
            return resultWithDataBO;

        }

        public async Task<ResultWithDataDTO<int>> updateInfo(InfoUpdateRequestDTO requestDTO)
        {
            _loggerManager.LogInfo("Entry InfoService=> updateInfo");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            if (requestDTO == null)
            {
                resultWithDataBO.IsSuccessful = false;
                resultWithDataBO.BusinessErrorMessage = "No data to update";
                _loggerManager.LogInfo(resultWithDataBO.Message);
            }
            try
            {

                var dataResult = await _infoRepo.GetAllInfoById(requestDTO.Id);
                if (dataResult != null)
                {
                    var updatedData = await _infoRepo.UpdateInfo(_mapper.Map(requestDTO,dataResult));
                    if(updatedData != null)
                    {
                        resultWithDataBO.Data = _mapper.Map<int>(1);
                        resultWithDataBO.IsSuccessful = true;
                        resultWithDataBO.Message = $"Data updated successfully";
                        _loggerManager.LogInfo(resultWithDataBO.Message);
                    }
                    else
                    {
                        resultWithDataBO.Data = _mapper.Map<int>(0);
                        resultWithDataBO.IsSuccessful = false;
                        resultWithDataBO.Message = $"No data found for this id : {requestDTO.Id}";
                        _loggerManager.LogInfo(resultWithDataBO.Message);
                    }
                }
                else
                {
                    resultWithDataBO.Data = _mapper.Map<int>(0);
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"No data found for this id : {requestDTO.Id}";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }

            }
            catch (Exception ex)
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to get Info Detail-Error observed.\nKindly retry or contact System Administrator.";
                resultWithDataBO.SystemErrorMessage = $"InfoService => updateInfo: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message}";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                if (resultWithDataBO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(resultWithDataBO.SystemErrorMessage, $"GetProductList");
                }
            }
            return resultWithDataBO;
        }
    }
}
