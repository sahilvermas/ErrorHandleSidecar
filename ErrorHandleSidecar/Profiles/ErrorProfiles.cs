using AutoMapper;
using ErrorHandleSidecar.Models;

namespace ErrorHandleSidecar.Profiles
{
    public class ErrorProfiles : Profile
    {
        public ErrorProfiles()
        {
            CreateMap<ErrorSchema, Protos.ErrorResponse>().ReverseMap();
        }
    }
}
