using InsternShip.Data.ViewModels.CategoryQuestion;
using InsternShip.Data.ViewModels.Department;
using InsternShip.Data.ViewModels.Language;
using InsternShip.Data.ViewModels.Question;
using InsternShip.Data.ViewModels.Result;
using InsternShip.Data.ViewModels.Room;
using InsternShip.Data.ViewModels.SecurityQuestion;
using InsternShip.Data.ViewModels.Skill;
using InsternShip.Data.ViewModels.UploadFileFromForm;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace InsternShip.Api.Controllers
{
    [Authorize(Roles = "Admin, Interviewer, Recruiter")]
    public class ImportController : BaseAPIController
    {
        private readonly ISkillService _serviceSkill;
        private readonly ISecurityQuestionService _serviceSecurityQuestion;
        private readonly IRoomService _serviceRoom;
        private readonly IResultService _serviceResult;
        private readonly IQuestionService _serviceQuestion;
        private readonly ILanguageService _serviceLanguage;
        private readonly IDepartmentService _serviceDepartment;
        private readonly ICategoryQuestionService _serviceCategoryQuestion;

        public ImportController(
            ISkillService serviceSkill,
            ISecurityQuestionService serviceSecurityQuestion,
            IRoomService serviceRoom,
            IResultService serviceResult,
            IQuestionService serviceQuestion,
            ILanguageService serviceLanguage,
            IDepartmentService serviceDepartment,
            ICategoryQuestionService serviceCategoryQuestion)
        {
            _serviceSkill = serviceSkill;
            _serviceSecurityQuestion = serviceSecurityQuestion;
            _serviceRoom = serviceRoom;
            _serviceResult = serviceResult;
            _serviceQuestion = serviceQuestion;
            _serviceLanguage = serviceLanguage;
            _serviceDepartment = serviceDepartment;
            _serviceCategoryQuestion = serviceCategoryQuestion;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ImportSkill([FromForm] UploadFileFromForm form, CancellationToken cancellationToken)
        {
            if (form == null)
                return NotFound();

            var file = form.File;

            if (file == null || file.Length <= 0)
                return NotFound();

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Not Support file extension");

            var list = new List<SkillAddModel>();
            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream, cancellationToken);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault()!;
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            list.Add(new SkillAddModel
                            {
                                SkillName = worksheet.Cells[row, 1].Value?.ToString() ?? "",
                                Description = worksheet.Cells[row, 2].Value?.ToString() ?? "",
                                //IsDeleted = (bool)(worksheet.Cells[row, 3].Value ?? false)
                            });
                        }
                    }
                }

                foreach (var item in list)
                {
                    await _serviceSkill.SaveSkill(item);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(list);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ImportCategoryQuestion([FromForm] UploadFileFromForm form, CancellationToken cancellationToken)
        {
            if (form == null)
                return NotFound();

            var file = form.File;

            if (file == null || file.Length <= 0)
                return NotFound();

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Not Support file extension");

            var list = new List<CategoryQuestionAddModel>();
            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream, cancellationToken);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault()!;
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            list.Add(new CategoryQuestionAddModel
                            {
                                CategoryQuestionName = worksheet.Cells[row, 1].Value?.ToString()!,
                                Weight = double.Parse(worksheet.Cells[row, 2].Value?.ToString()!.Trim()!),
                            });
                        }
                    }
                }

                foreach (var item in list)
                {
                    await _serviceCategoryQuestion.SaveCategoryQuestion(item);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(list);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ImportQuestion([FromForm] UploadFileFromForm form, CancellationToken cancellationToken)
        {
            if (form == null)
                return NotFound();

            var file = form.File;

            if (file == null || file.Length <= 0)
                return NotFound();

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Not Support file extension");

            var list = new List<QuestionAddModel>();
            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream, cancellationToken);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault()!;
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            list.Add(new QuestionAddModel
                            {
                                QuestionString = worksheet.Cells[row, 1].Value?.ToString()!,
                                CategoryQuestionId = Guid.Parse(worksheet.Cells[row, 2].Value?.ToString()!.Trim()!)
                            });
                        }
                    }
                }

                foreach (var item in list)
                {
                    await _serviceQuestion.AddQuestion(item);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(list);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ImportLanguage([FromForm] UploadFileFromForm form, CancellationToken cancellationToken)
        {
            if (form == null)
                return NotFound();

            var file = form.File;

            if (file == null || file.Length <= 0)
                return NotFound();

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Not Support file extension");

            var list = new List<LanguageAddModel>();
            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream, cancellationToken);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault()!;
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            list.Add(new LanguageAddModel
                            {
                                LanguageName = worksheet.Cells[row, 1].Value?.ToString()!,
                            });
                        }
                    }
                }

                foreach (var item in list)
                {
                    await _serviceLanguage.AddLanguage(item);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(list);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ImportDepartment([FromForm] UploadFileFromForm form, CancellationToken cancellationToken)
        {
            if (form == null)
                return NotFound();

            var file = form.File;

            if (file == null || file.Length <= 0)
                return NotFound();

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Not Support file extension");

            var list = new List<DepartmentAddModel>();
            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream, cancellationToken);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault()!;
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            list.Add(new DepartmentAddModel
                            {
                                DepartmentName = worksheet.Cells[row, 1].Value?.ToString()!,
                                Address = worksheet.Cells[row, 2].Value?.ToString()!,
                                Email = worksheet.Cells[row, 3].Value?.ToString()!,
                                Phone = worksheet.Cells[row, 4].Value?.ToString()!,
                                Website = worksheet.Cells[row, 5].Value?.ToString()!,
                            });
                        }
                    }
                }

                foreach (var item in list)
                {
                    await _serviceDepartment.SaveDepartment(item);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(list);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ImportResult([FromForm] UploadFileFromForm form, CancellationToken cancellationToken)
        {
            if (form == null)
                return NotFound();

            var file = form.File;

            if (file == null || file.Length <= 0)
                return NotFound();

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Not Support file extension");

            var list = new List<ResultAddModel>();
            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream, cancellationToken);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault()!;
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            list.Add(new ResultAddModel
                            {
                                ResultString = worksheet.Cells[row, 1].Value?.ToString()!,
                            });
                        }
                    }
                }

                foreach (var item in list)
                {
                    await _serviceResult.SaveResult(item);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(list);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ImportRoom([FromForm] UploadFileFromForm form, CancellationToken cancellationToken)
        {
            if (form == null)
                return NotFound();

            var file = form.File;

            if (file == null || file.Length <= 0)
                return NotFound();

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Not Support file extension");

            var list = new List<RoomAddModel>();
            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream, cancellationToken);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault()!;
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            list.Add(new RoomAddModel
                            {
                                RoomName = worksheet.Cells[row, 1].Value?.ToString()!,
                            });
                        }
                    }
                }

                foreach (var item in list)
                {
                    await _serviceRoom.SaveRoom(item);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(list);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ImportSecurityQuestion([FromForm] UploadFileFromForm form, CancellationToken cancellationToken)
        {
            if (form == null)
                return NotFound();

            var file = form.File;

            if (file == null || file.Length <= 0)
                return NotFound();

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Not Support file extension");

            var list = new List<SecurityQuestionAddModel>();
            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream, cancellationToken);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault()!;
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            list.Add(new SecurityQuestionAddModel
                            {
                                QuestionString = worksheet.Cells[row, 1].Value?.ToString()!,
                            });
                        }
                    }
                }

                foreach (var item in list)
                {
                    await _serviceSecurityQuestion.SaveSecurityQuestion(item);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(list);
        }
    }
}