﻿namespace Candidate_Test_Task.Abstraction
{
    public static class ResultExtenstions
    {
        public static ObjectResult ToProblem(this Result result, int statusCode)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException("Cannot convert success result to a problem");

       
            var problem = Results.Problem(statusCode: statusCode);

         
            var problemDetails = problem.GetType().GetProperty(nameof(ProblemDetails))!.GetValue(problem) as ProblemDetails;
            problemDetails!.Extensions = new Dictionary<string, object?>
                {

                    {

                    "errors",new[] {result.Error}
                    }


                };


            return new ObjectResult(problemDetails);
        }
    }
}
