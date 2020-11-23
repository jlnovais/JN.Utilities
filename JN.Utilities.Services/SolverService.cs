using System;
using System.Collections.Generic;
using Google.OrTools.LinearSolver;
using JN.Utilities.Core.Entities;
using JN.Utilities.Core.Services;
using Solver = Google.OrTools.LinearSolver.Solver;

namespace JN.Utilities.Services
{
    public class SolverService : ISolverService
    {
        public ProblemSolution Solve(ProblemConfiguration config)
        {
            var res = new ProblemSolution();
   
            //https://developers.google.com/optimization/mip/mip_var_array#c_1

            if(config.AvailableAmount<=0)
                throw new ArgumentException("Invalid configuration - Available amount");

            if (config.Products == null || config.Products.Count == 0)
                throw new ArgumentException("Invalid configuration - Products");

            // [START solver]
            // Create the linear solver with the CBC backend.
            Solver solver = Solver.CreateSolver(/*"SimpleMipProgram",*/ "CBC");
            // [END solver]

            // [START variables]
            // x, y and z are integer non-negative variables.
            var variables = new List<Variable>();

            foreach (var p in config.Products)
            {
                var variable = solver.MakeIntVar(0.0, double.PositiveInfinity, GetVariableName(p));

                variables.Add(variable);

                if (p.MaxUnits == 0)
                {
                    solver.Add(variable == 0);
                }
                else if (
                  (p.MinUnits >= 0 && p.MaxUnits > 0 && p.MinUnits < p.MaxUnits) ||
                  (p.MaxUnits == p.MinUnits && p.MaxUnits > 0)
              )
                {
                    // other constraints
                    solver.Add(variable >= p.MinUnits);
                    solver.Add(variable <= p.MaxUnits);
                }


            }

            res.NumberVariables = solver.NumVariables();

           
            // [END variables]

            // [START constraints]
            //  (unitPriceX * x) + (unitPriceY * y) + (unitPriceZ *z) <= maxValue.

            var c = solver.MakeConstraint(0,  config.AvailableAmount);

            int i = 0;
            foreach (var variable in variables)
            {
                c.SetCoefficient(variable, (double)config.Products[i].UnitPrice);
                i++;
            }

            res.NumConstraints = solver.NumConstraints();
            // [END constraints]


            Objective objective = solver.Objective();

            i = 0;
            foreach (var variable in variables)
            {
                objective.SetCoefficient(variable, (double)config.Products[i].UnitPrice);
                i++;
            }
            objective.SetMaximization();

            // [START solve]
            Solver.ResultStatus resultStatus = solver.Solve();
            // [END solve]

            // [START print_solution]
            // Check that the problem has an optimal solution.
            if (resultStatus != Solver.ResultStatus.OPTIMAL)
            {
                res.HasOptimalSolution = false;
                return res;
            }

            res.HasOptimalSolution = true;


            res.FinalAmount = (decimal)solver.Objective().Value();
            res.RemainingAmount = Math.Round(config.AvailableAmount - (double)res.FinalAmount, 2);

            i = 0;
            foreach (var variable in variables)
            {
                var itemValue = variable.SolutionValue() * (double)config.Products[i].UnitPrice;
                itemValue = Math.Round(itemValue, 2);

                res.ResponseVariables.Add(
                    new SolutionVariable()
                    {
                        Name = config.Products[i].Name, //variable.Name(),
                        Code = config.Products[i].Code,
                        SolutionValue = variable.SolutionValue(),
                        UnitPrice = config.Products[i].UnitPrice,
                        FinalAmount = itemValue,
                        Description = variable.Name(),
                        Details =
                            $"{variable.Name()} = {variable.SolutionValue()}  ///  {variable.SolutionValue()} * {config.Products[i].UnitPrice} = {itemValue} euros "
                    });

                i++;
            }

            res.SolveTimeMs = solver.WallTime();
            res.Iterations = solver.Iterations();
            res.Nodes = solver.Nodes();

            return res;

        }

        private static string GetVariableName(Product p)
        {
            var name = $"Quantity of {p.Name}";

            if (!string.IsNullOrWhiteSpace(p.Code))
                name += $" [{p.Code}]";

            return name;
        }
    }
}
