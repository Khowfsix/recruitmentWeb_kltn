using InsternShip.Data.ViewModels.Department;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    [Authorize]
    public class DepartmentController : BaseAPIController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartment(string? name)
        {
            var departmentList = await _departmentService.GetAllDepartment(name);
            if (departmentList == null)
            {
                return Ok("Not found");
            }
            return Ok(departmentList);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SaveDepartment(DepartmentAddModel request)
        {
            var departmentList = await _departmentService.SaveDepartment(request);
            if (departmentList == null)
            {
                return Ok("Not found");
            }
            return Ok(departmentList);
        }

        [HttpPut("{requestId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDepartment(DepartmentUpdateModel request, Guid requestId)
        {
            var departmentList = await _departmentService.UpdateDepartment(request, requestId);
            if (departmentList == null)
            {
                return Ok("Not found");
            }
            return Ok(departmentList);
        }

        [HttpDelete("{requestId:guid}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteDepartment(Guid requestId)
        {
            var departmentList = await _departmentService.DeleteDepartment(requestId);
            if (departmentList == null)
            {
                return Ok("Not found");
            }
            return Ok(departmentList);
        }
    }
}