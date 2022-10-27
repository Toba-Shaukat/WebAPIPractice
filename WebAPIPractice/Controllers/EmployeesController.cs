using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWebAPIPractice.Controllers
{
    //this a route for companies and employees both
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public EmployeesController(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeesForCompany(int companyId)
        {
            var company = await _repositoryManager.CompanyRepository.GetCompanyAsync(companyId, trackChanges: false);

            if (company == null)
            {
                _loggerManager.LogInfo($"Company with id: {companyId} doesn't exist in the database");
                return NotFound();
            }

            var employeesFromDb = await _repositoryManager.EmployeeRepository.GetEmployeesAsync(companyId, trackChanges: false);

            var employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
            return Ok(employeeDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeForCompany(int companyId, int id)
        {
            var company = await _repositoryManager.CompanyRepository.GetCompanyAsync(companyId, trackChanges: false);

            if (company == null)
            {
                _loggerManager.LogInfo($"Company with id: {companyId} doesn't exist in the database");
                return NotFound();
            }

            var employeeFromDb = await _repositoryManager.EmployeeRepository.GetEmployeeAsync(companyId, id, trackChanges: false);

            if (employeeFromDb == null)
            {
                _loggerManager.LogInfo($"Employee with id: {id} doesn't exist in the database");
                return NotFound();
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employeeFromDb);
            return Ok(employeeDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeForCompany(int companyId, [FromBody] EmployeeForCreationDto employee)
        {
            if (employee == null)
            {
                _loggerManager.LogError("EmployeeForCreationDto object sent from client is null.");
                return BadRequest("EmployeeForCreationDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _loggerManager.LogError("Invalid model state for the EmployeeForCreationDto object");
                return UnprocessableEntity(ModelState);
            }


            var company = await _repositoryManager.CompanyRepository.GetCompanyAsync(companyId, trackChanges: false);

            if (company == null)
            {
                _loggerManager.LogInfo($"Company with id : {companyId} doesn't exists in the database");
                return NotFound();
            }

            var employeeEntity = _mapper.Map<Employee>(employee);

            _repositoryManager.EmployeeRepository.CreateEmployeeForCompany(companyId, employeeEntity);
            await _repositoryManager.SaveAsync();

            return Created("", "Employee Added");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeForCompany(int companyId, int id)
        {
            var company = await _repositoryManager.CompanyRepository.GetCompanyAsync(companyId, trackChanges: false);
            if (company == null)
            {
                _loggerManager.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }

            var employeeForCompany = await _repositoryManager.EmployeeRepository.GetEmployeeAsync(companyId, id, trackChanges: false);

            if (employeeForCompany == null)
            {
                _loggerManager.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repositoryManager.EmployeeRepository.DeleteEmployee(employeeForCompany);
            await _repositoryManager.SaveAsync();

            return NoContent(); //204
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeForCompany(int companyId, int id, [FromBody] EmployeeForUpdateDto employeeDto)
        {
            if (employeeDto == null)
            {
                _loggerManager.LogError("EmployeeForUpdateDto object sent from client is null.");
                return BadRequest("EmployeeForUpdateDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _loggerManager.LogError("Invalid model state for the EmployeeForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var company = await _repositoryManager.CompanyRepository.GetCompanyAsync(companyId, trackChanges: false);

            if (company == null)
            {
                _loggerManager.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }

            var employeeEntity = await _repositoryManager.EmployeeRepository.GetEmployeeAsync(companyId, id, trackChanges: true);

            if (employeeEntity == null)
            {
                _loggerManager.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(employeeDto, employeeEntity);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(int companyId, int id, [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _loggerManager.LogError("patchDoc object sent from client is null");
                return BadRequest("patchDoc object is null");
            }

            var company = await _repositoryManager.CompanyRepository.GetCompanyAsync(companyId, trackChanges: false);

            if (company == null)
            {
                _loggerManager.LogInfo($"Company with id: {companyId} doesn't exist in the database");
                return NotFound();
            }

            var employeeEntity = await _repositoryManager.EmployeeRepository.GetEmployeeAsync(companyId, id, trackChanges: true);

            if (employeeEntity == null)
            {
                _loggerManager.LogInfo($"Employee with id: {id} doesn't exist in the database");
                return NotFound();
            }

            //dto <- model
            var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);

            patchDoc.ApplyTo(employeeToPatch, ModelState);

            //TryValidateModel
            TryValidateModel(employeeToPatch);

            if (!ModelState.IsValid)
            {
                _loggerManager.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }

            //dto -> model
            _mapper.Map(employeeToPatch, employeeEntity);

            await _repositoryManager.SaveAsync();

            return NoContent(); //204
        }
    }
}
