using AutoMapper;
using MyInfoDAL.DataModel;
using MyInfoDTOModel.Info;

namespace MyInfoWebAPI.AutoMapperProfile
{
    public class MapperProfileDeclaration : Profile
    {
        public MapperProfileDeclaration() 
        {
            #region Info
            CreateMap<NewTable, InfoResponseDTO>();
            CreateMap<InfoRequestDTO, NewTable>();
            CreateMap<InfoUpdateRequestDTO, NewTable>();
            #endregion
        }
    }
}
