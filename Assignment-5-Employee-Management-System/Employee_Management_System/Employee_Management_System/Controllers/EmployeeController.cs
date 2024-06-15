using AutoMapper;
using Employee_Management_System.DTO;
using Employee_Management_System.Interface;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using Employee_Management_System.Entities;


namespace Employee_Management_System.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class EmployeeController:Controller
    {
        public readonly IEmployeeService _employeeService;
        public readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService,IMapper mapper) 
        { 
            _employeeService = employeeService; 
            _mapper = mapper;
        }

        //Section1:CRUD Operations

        //Employee Basic Details

        [HttpPost]
        public async Task<EmployeeBasicDetailsDTO> AddEmployeeBasicDetails(EmployeeBasicDetailsDTO employeeBasicDTO)
        {
            var response=await _employeeService.AddEmployeeBasicDetails(employeeBasicDTO);
            return response;
        }

        [HttpGet]
        public async Task<List<EmployeeBasicDetailsDTO>> GetAllEmployeesBasicDetails()
        {
            var response = await _employeeService.GetAllEmployeesBasicDetails();
            return response;

        }

        [HttpGet]
        
        public async Task<EmployeeBasicDetailsDTO> GetEmployeeBasicDetailByUId(string UId)
        {
            var response = await _employeeService.GetEmployeeBasicDetailByUId(UId);
            return response;
        }

        [HttpPost]

        public async Task<EmployeeBasicDetailsDTO> UpdateEmployeeBasicDetails(EmployeeBasicDetailsDTO employeeBasicDetails)
        {
            var response=await _employeeService.UpdateEmployeeBasicDetails(employeeBasicDetails);
            return response;
        }

        [HttpPost]
        public async Task<string> DeleteEmployeeBasicDetails(string UId)
        {
            var response = await _employeeService.DeleteEmployeeBasicDetails(UId);
            return response;
        }

        [HttpGet]

        public async Task<List<EmployeeBasicDetailsDTO>> GetAllEmployeeBasicDetailsByReportingManagerName(string ReportingManagerName)
        {
            var response = await _employeeService.GetAllEmployeeBasicDetailsByReportingManagerName(ReportingManagerName);
            return response;
        }

        [HttpPost]
        public async Task<EmployeeBasicFilterCriteria> GetAllEmployeesBasicDetailsByPagination(EmployeeBasicFilterCriteria employeeBasicFilterCriteria)
        {
            var response= await _employeeService.GetAllEmployeesBasicDetailsByPagination(employeeBasicFilterCriteria);
            return response;
        }

        //Employee Additional Details

        [HttpPost]
        public async Task<EmployeeAdditionalDetailsDTO> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsDTO employeeDTO)
        {
            var response = await _employeeService.AddEmployeeAdditionalDetails(employeeDTO);
            return response;
        }

         [HttpGet]
         public async Task<List<EmployeeAdditionalDetailsDTO>> GetAllEmployeesAdditionalDetails()
         {
             var response = await _employeeService.GetAllEmployeesAdditionalDetails();
             return response;

         }
         
         [HttpGet]

         public async Task<EmployeeAdditionalDetailsDTO> GetEmployeeAdditionalDetailByUId(string UId)
         {
             var response = await _employeeService.GetEmployeeAdditionalDetailByUId(UId);
             return response;
         }
       
         [HttpPost]

         public async Task<EmployeeAdditionalDetailsDTO> UpdateEmployeeAdditionalDetails(EmployeeAdditionalDetailsDTO employeeAdditionalDetails)
         {
             var response = await _employeeService.UpdateEmployeeAdditionalDetails(employeeAdditionalDetails);
             return response;
         }
       
        [HttpPost]
        public async Task<string> DeleteEmployeeAdditionalDetails(string UId)
        {
            var response = await _employeeService.DeleteEmployeeAdditionalDetails(UId);
            return response;
        }

        [HttpGet]

        public async Task<List<EmployeeAdditionalDetailsDTO>> GetAllEmployeeAdditionalDetailsByDesignationName(string designationName)
        {
            var response = await _employeeService.GetAllEmployeeAdditionalDetailsByDesignationName(designationName);
            return response;
        }

        [HttpPost]
        public async Task<EmployeeAdditionalFilterCriteria> GetAllEmployeesAdditionalDetailsByPagination(EmployeeAdditionalFilterCriteria employeeAdditionalFilterCriteria)
        {
            var response = await _employeeService.GetAllEmployeesAdditionalDetailsByPagination(employeeAdditionalFilterCriteria);
            return response;
        }

        //import and export excel
        private string GetStringFromCell(ExcelWorksheet worksheet, int row, int column)
        {
            var cellValue = worksheet.Cells[row, column];
            return cellValue?.Text?.Trim();
        }

        [HttpPost]

        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty or null");

            var employees = new List<EmployeeBasicDetailsDTO>();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {

                            var employee = new EmployeeBasicDetailsDTO()
                            {
                                FirstName = GetStringFromCell(worksheet, row, 1),
                                LastName = GetStringFromCell(worksheet, row, 2),
                                Email = GetStringFromCell(worksheet, row, 3),
                                Mobile= GetStringFromCell(worksheet, row, 4),
                                ReportingManagerName = GetStringFromCell(worksheet, row, 5)

                            };
                            await AddEmployeeBasicDetails(employee);

                            employees.Add(employee);
                        }
                    }
                }
                return Ok((employees));
            }
        }

        [HttpPost]

        public async Task<IActionResult> ExportExcel()
        {
            var employeeBasic = await _employeeService.GetAllEmployeesBasicDetails();
            var employeesAdditional = await _employeeService.GetAllEmployeesAdditionalDetails();

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Students");


                //Add Header
                
                worksheet.Cells[1, 1].Value = "Firstname";
                worksheet.Cells[1, 2].Value = "LastName";
                worksheet.Cells[1, 3].Value = "Email";
                worksheet.Cells[1, 4].Value = "Mobile";
                worksheet.Cells[1, 5].Value = "ReportingManagerName";
               /* worksheet.Cells[1, 6].Value = "DateOfBirth";
                worksheet.Cells[1, 7].Value = "DateOfJoining";*/
               
                
                //set header style

                using (var range = worksheet.Cells[1, 1, 1, 7])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(color: System.Drawing.Color.LightGreen);
                }

                //Add employee data

             
                for (int i = 0; i < employeeBasic.Count; i++ )
                {
                    var employee = employeeBasic[i];
             
                    worksheet.Cells[i + 2, 1].Value = employee.FirstName;
                    worksheet.Cells[i + 2, 2].Value = employee.LastName;
                    worksheet.Cells[i + 2, 3].Value = employee.Email;
                    worksheet.Cells[i + 2, 4].Value = employee.Mobile;
                    worksheet.Cells[i + 2, 5].Value = employee.ReportingManagerName;
                  
                }

                var stream = new System.IO.MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var filename = "Employees.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);

            }
        }
    }
}
