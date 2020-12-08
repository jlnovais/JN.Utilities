using System;
using System.Threading.Tasks;
using JN.Utilities.Core.Dto;
using JN.Utilities.Core.Entities;
using JN.Utilities.Core.Repositories;
using System.Text.Json;
using JN.Utilities.Core.Services;


namespace JN.Utilities.Services
{
    public class ProblemSolutionService : IProblemSolutionService
    {
        private readonly IProblemSolutionRepository _repository;

        public ProblemSolutionService(IProblemSolutionRepository repository)
        {
            _repository = repository;
        }


        public async Task<Result> Save(ProblemSolution solution, string user)
        {

            var res = new Result(){Success = false};

            var key = solution.Id.ToString();
            var requestDate = DateTime.Now;

            try
            {
                var item = JsonSerializer.Serialize<ProblemSolution>(solution);
                await _repository.Save(key, item, user, requestDate);

                res.Success = true;
            }
            catch (Exception e)
            {
                res.ErrorCode = -1;
                res.ErrorDescription = e.Message;
            }

            return res;
        }

        public async Task<Result<ProblemSolution>> GetById(string id, string username)
        {
            var res = new Result<ProblemSolution>() { Success = false };

            ProblemSolution item = null;

            try
            {
                var jsonText = await _repository.GetById(id, username);

                if(!string.IsNullOrWhiteSpace(jsonText))
                    item = JsonSerializer.Deserialize<ProblemSolution>(jsonText);

                res.ErrorCode = 0;
                res.Success = true;
                res.ReturnedObject = item;
            }
            catch (Exception e)
            {
                res.ErrorCode = -1;
                res.Success = false;
                res.ErrorDescription = e.Message;
            }

            return res;
        }

    }
}
