using AutoMapper;
using JN.Utilities.Contracts.V1.Responses;
using JN.Utilities.Core.Entities;

namespace JN.Utilities.API.MappingProfiles
{
    public class DomainToContractProfile: Profile
    {
        public DomainToContractProfile()
        {
            
            CreateMap<ProblemSolution, Solution>()
                //.IncludeMembers(x => x.ResponseVariables)
                .ForMember(dest => dest.SolutionItems, opt =>
                    opt.MapFrom(src => src.ResponseVariables))
                .ForMember(dest => dest.Statistics, opt =>
                    opt.MapFrom(src =>
                        new SolutionStatistics
                        {
                            Iterations = src.Iterations,
                            Nodes = src.Nodes,
                            SolveTimeMs = src.SolveTimeMs,
                            NumConstraints = src.NumConstraints,
                            NumberVariables = src.NumberVariables
                        }
                    ));

            CreateMap<SolutionVariable, SolutionItem>();

            
           
        }
        
    }
}
