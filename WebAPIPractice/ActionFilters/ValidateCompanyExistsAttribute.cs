using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace AWebAPIPractice.ActionFilters
{
    public class ValidateCompanyExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;

        public ValidateCompanyExistsAttribute(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //HTTP Request: GET,POST,PUT,DELETE

            bool trackChanges = context.HttpContext.Request.Method.Equals("PUT"); //==
            int id = (int)context.ActionArguments["id"];

            var company = await _repositoryManager.CompanyRepository.GetCompanyAsync(id, trackChanges);

            if(company == null)
            {
                _loggerManager.LogInfo($"Company with Id: {id} doesn't exist in the database");
                context.Result = new NotFoundResult();
            }
            else
            {
                //We will put the company object data in the HttpContext.Items(k and v pair)
                context.HttpContext.Items.Add("company", company);
                await next();
            }
        }
    }
}
