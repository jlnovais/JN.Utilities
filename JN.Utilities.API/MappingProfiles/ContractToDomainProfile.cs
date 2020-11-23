using AutoMapper;
using JN.Utilities.Contracts.V1.Requests;
using JN.Utilities.Core.Entities;

namespace JN.Utilities.API.MappingProfiles
{
    public class ContractToDomainProfile : Profile
    {
        public ContractToDomainProfile()
        {
            CreateMap<ProblemDefinition, ProblemConfiguration>();

            CreateMap<ProductDetails, Product>();
        }
    }
}
