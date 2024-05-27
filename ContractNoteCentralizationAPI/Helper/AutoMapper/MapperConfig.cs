using AutoMapper;
using ContractNoteCentralizationAPI.Model.ContactNote;
using ContractNoteCentralizationAPI.Model.ManageRole;
using ContractNoteCentralizationAPI.Model.ManagerRegister;
using ContractNoteCentralizationAPI.Model.ManageUser;
using ContractNoteCentralizationAPI.Model.MasterSystem;
using ContractNoteCentralizationAPI.Model.UsersAuthentication;

namespace ContractNoteCentralizationAPI.Helper.AutoMapper
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            //Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {
                //Configuring Employee and EmployeeDTO
                cfg.CreateMap<ManageRegisterReq, ManageRegisterDao>();

                cfg.CreateMap<ManageUserReq, ManageUserDao>();

                cfg.CreateMap<MasterSystemReq, MasterSystemDto>();

                cfg.CreateMap<ManageRoleReq, ManageRoleDao>();

                cfg.CreateMap<InquiryContactNote, ContactNoteDto>();

                cfg.CreateMap<AddContactNote, ContactNoteDto>();

                cfg.CreateMap<List<AddContactNote>, List<ContactNoteDto>>()
                .ConvertUsing((sourceList, destinationList, context) =>
               {
                   if (sourceList == null) return null;

                   var result = new List<ContactNoteDto>();

                   foreach (var source in sourceList)
                   {
                       result.Add(context.Mapper.Map<AddContactNote, ContactNoteDto>(source));
                   }

                   return result;
               });




                //cfg.CreateMap<InquiryContactNote, ContactNoteDto>();


                //Provide Mapping Configuration of FullName and Name Property
                //.ForMember(dest => dest.FullName, act => act.MapFrom(src => src.Name))

                //Provide Mapping Dept of FullName and Department Property
                //.ForMember(dest => dest.Dept, act => act.MapFrom(src => src.Department));
                //Any Other Mapping Configuration ....
            });
            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
