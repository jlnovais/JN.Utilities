namespace JN.Utilities.Repositories
{
    public class ProblemSolutionRepository: BaseRepository
    {
        public ProblemSolutionRepository(string connectionString) : base(connectionString)
        {
        }

        public ProblemSolutionRepository(object config) : base(config)
        {
        }
    }
}
