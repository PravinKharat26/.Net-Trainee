using AutoMapper;
using Employee_Management_System.DTO;
using Employee_Management_System.Interface;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using Employee_Management_System.Entities;
using Employee_Management_System.ServiceFilters;



namespace Employee_Management_System.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        public readonly IEmployeeService _employeeService;
        public readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        //Employee Basic Details

        [HttpPost]
        public async Task<EmployeeBasicDetailsDTO> AddEmployeeBasicDetails(EmployeeBasicDetailsDTO employeeBasicDTO)
        {
            var response = await _employeeService.AddEmployeeBasicDetails(employeeBasicDTO);
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
            var response = await _employeeService.UpdateEmployeeBasicDetails(employeeBasicDetails);
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


        //1.GetAll API for EmployeeBasicDetails by FilterCriteria and Service Filter.
        //ServiceFilter--

        [HttpPost]

        [ServiceFilter(typeof(BuildEmployeeBasicDetailsFilter))]
        public async Task<EmployeeBasicFilterCriteria> GetAllEmployeesBasicDetailsByPagination(EmployeeBasicFilterCriteria employeeBasicFilterCriteria)
        {
            var response = await _employeeService.GetAllEmployeesBasicDetailsByPagination(employeeBasicFilterCriteria);
            return response;
        }


        //1.GetAll API for EmployeeAdditionalDetails by FilterCriteria and Service Filter.

        [HttpPost]

        [ServiceFilter(typeof(BuildEmployeeAdditionalDetailsFilter))]
        public async Task<EmployeeAdditionalFilterCriteria> GetAllEmployeesAdditionalDetailsByPagination(EmployeeAdditionalFilterCriteria employeeAdditionalFilterCriteria)
        {
            var response = await _employeeService.GetAllEmployeesAdditionalDetailsByPagination(employeeAdditionalFilterCriteria);
            return response;
        }


        //2. API which will demonstrate the use of MakePostRequest


        //makepostrequest employee basic details

        [HttpPost]

        public async Task<IActionResult> AddEmployeeBasicDetailByMakePostRequest(EmployeeBasicDetailsDTO employeeBasicDetailsDTO)
        {
            var response = await _employeeService.AddEmployeeBasicDetailByMakePostRequest(employeeBasicDetailsDTO);
            return Ok(response);
        }

        ///makepostrequest employee additional details

        [HttpPost]
        public async Task<IActionResult> AddEmployeeAdditionalDetailByMakePostRequest(EmployeeAdditionalDetailsDTO employeeAdditionalDetailsDTO)
        {
            var response = await _employeeService.AddEmployeeAdditionalDetailByMakePostRequest(employeeAdditionalDetailsDTO);
            return Ok(response);
        }

        //makepostrequest(add security in Visitor Project)

        [HttpPost]
        public async Task<IActionResult> AddSecurityByMakePostRequest(SecurityDTO securityDTO)
        {
            var response = await _employeeService.AddSecurityByMakePostRequest(securityDTO);
            return Ok(response);
        }

        //3.API which will demonstrate the use of MakeGetRequest

        //makegetrequest employee basic details

        [HttpGet]

        public async Task<List<EmployeeBasicDetailsDTO>> GetEmployeeeBasicDetailByMakeGetRequest()
        {
            var response = await _employeeService.GetEmployeeeBasicDetailByMakeGetRequest();
            return response;
        }

        //makegetrequest employee Additional details

        [HttpGet]
        public async Task<List<EmployeeAdditionalDetailsDTO>> GetEmployeeeAdditionalDetailByMakeGetRequest()
        {
            var response = await _employeeService.GetEmployeeeAdditionalDetailByMakeGetRequest();
            return response;
        }

        //makegetrequest (get all security in Visitor Project)

        [HttpGet]
        public async Task<List<SecurityDTO>> GetAllSecurityByMakeGetRequest()
        {
            var response = await _employeeService.GetAllSecurityByMakeGetRequest();
            return response;
        }

        //4.export excel contains all basic details + additional details.

        [HttpPost]

        public async Task<IActionResult> ExportExcel()
        {
            var employeeBasic = await _employeeService.GetAllEmployeesBasicDetails();


            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");

                //Add Header
                //EmployeeBasicDetails

                worksheet.Cells[1, 1].Value = "EmployeeID";
                worksheet.Cells[1, 2].Value = "UId";
                worksheet.Cells[1, 3].Value = "Salutory";
                worksheet.Cells[1, 4].Value = "FirstName";
                worksheet.Cells[1, 5].Value = "MiddleName";
                worksheet.Cells[1, 6].Value = "LastName";
                worksheet.Cells[1, 7].Value = "NickName";
                worksheet.Cells[1, 8].Value = "Email";
                worksheet.Cells[1, 9].Value = "Role";
                worksheet.Cells[1, 10].Value = "ReportingManagerUId";
                worksheet.Cells[1, 11].Value = "ReportingManagerName";
                worksheet.Cells[1, 12].Value = "Address";

                //EmployeeAdditionalDetails
                worksheet.Cells[1, 13].Value = "AdditionalDetailUId";
                worksheet.Cells[1, 14].Value = "employeeBasicDetailsUid";
                worksheet.Cells[1, 15].Value = "alternateEmail";
                worksheet.Cells[1, 16].Value = "alternateMobile";
                worksheet.Cells[1, 17].Value = "designationName";
                worksheet.Cells[1, 18].Value = "departmentName";
                worksheet.Cells[1, 19].Value = "locationName";
                worksheet.Cells[1, 20].Value = "employeeStatus";
                worksheet.Cells[1, 21].Value = "sourceOfHire";
                worksheet.Cells[1, 22].Value = "dateOfJoining";
                worksheet.Cells[1, 23].Value = "dateOfBirth";
                worksheet.Cells[1, 24].Value = "age";

                worksheet.Cells[1, 25].Value = "gender";
                worksheet.Cells[1, 26].Value = "religion";
                worksheet.Cells[1, 27].Value = "caste";
                worksheet.Cells[1, 28].Value = "maritalStatus";
                worksheet.Cells[1, 29].Value = "BloodGroup";
                worksheet.Cells[1, 30].Value = "height";
                worksheet.Cells[1, 31].Value = "weight";
                worksheet.Cells[1, 32].Value = "pan";
                worksheet.Cells[1, 33].Value = "aadhaar";
                worksheet.Cells[1, 34].Value = "nationality";
                worksheet.Cells[1, 35].Value = "passportNo";
                worksheet.Cells[1, 36].Value = "PFno";


                //set header style

                using (var range = worksheet.Cells[1, 1, 1, 36])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(color: System.Drawing.Color.LightBlue);
                }

                //Add employee data

                int i = 0;
                var employeesAdditional = await _employeeService.GetAllEmployeesAdditionalDetails();

                while (i < employeeBasic.Count)
                {
                    var employee = employeeBasic[i];

                    var employeeAdditional = employeesAdditional.FirstOrDefault();

                    worksheet.Cells[i + 2, 1].Value = employee.EmployeeID;
                    worksheet.Cells[i + 2, 2].Value = employee.UId;
                    worksheet.Cells[i + 2, 3].Value = employee.Salutory;
                    worksheet.Cells[i + 2, 4].Value = employee.FirstName;
                    worksheet.Cells[i + 2, 5].Value = employee.MiddleName;
                    worksheet.Cells[i + 2, 6].Value = employee.LastName;
                    worksheet.Cells[i + 2, 7].Value = employee.NickName;
                    worksheet.Cells[i + 2, 8].Value = employee.Email;
                    worksheet.Cells[i + 2, 9].Value = employee.Role;
                    worksheet.Cells[i + 2, 10].Value = employee.ReportingManagerUId;
                    worksheet.Cells[i + 2, 11].Value = employee.ReportingManagerName;
                    worksheet.Cells[i + 2, 12].Value = employee.Address;

                    worksheet.Cells[i + 2, 13].Value = employeeAdditional.UId;
                    worksheet.Cells[i + 2, 14].Value = employeeAdditional.EmployeeBasicDetailsUId;
                    worksheet.Cells[i + 2, 15].Value = employeeAdditional.AlternateEmail;
                    worksheet.Cells[i + 2, 16].Value = employeeAdditional.AlternateMobile;
                    worksheet.Cells[i + 2, 17].Value = employeeAdditional.WorkInformation.DesignationName;
                    worksheet.Cells[i + 2, 18].Value = employeeAdditional.WorkInformation.DepartmentName;
                    worksheet.Cells[i + 2, 19].Value = employeeAdditional.WorkInformation.LocationName;
                    worksheet.Cells[i + 2, 20].Value = employeeAdditional.WorkInformation.EmployeeStatus;
                    worksheet.Cells[i + 2, 21].Value = employeeAdditional.WorkInformation.SourceOfHire;
                    worksheet.Cells[i + 2, 22].Value = employeeAdditional.WorkInformation.DateOfJoining;
                    worksheet.Cells[i + 2, 23].Value = employeeAdditional.PersonalDetails.DateOfBirth;
                    worksheet.Cells[i + 2, 24].Value = employeeAdditional.PersonalDetails.Age;

                    worksheet.Cells[i + 2, 25].Value = employeeAdditional.PersonalDetails.Gender;
                    worksheet.Cells[i + 2, 26].Value = employeeAdditional.PersonalDetails.Religion;
                    worksheet.Cells[i + 2, 27].Value = employeeAdditional.PersonalDetails.Caste;
                    worksheet.Cells[i + 2, 28].Value = employeeAdditional.PersonalDetails.MaritalStatus;
                    worksheet.Cells[i + 2, 29].Value = employeeAdditional.PersonalDetails.BloodGroup;
                    worksheet.Cells[i + 2, 30].Value = employeeAdditional.PersonalDetails.Height;
                    worksheet.Cells[i + 2, 31].Value = employeeAdditional.PersonalDetails.Weight;
                    worksheet.Cells[i + 2, 32].Value = employeeAdditional.IdentityInformation.PAN;
                    worksheet.Cells[i + 2, 33].Value = employeeAdditional.IdentityInformation.Aadhar;
                    worksheet.Cells[i + 2, 34].Value = employeeAdditional.IdentityInformation.Nationality;
                    worksheet.Cells[i + 2, 35].Value = employeeAdditional.IdentityInformation.PassportNumber;
                    worksheet.Cells[i + 2, 36].Value = employeeAdditional.IdentityInformation.PFNumber;


                    i++;

                }

                var stream = new System.IO.MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var filename = "Employees.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);

            }
        }


        //5.GetEmployeeAdditionalDetailsByBasicDetailsUId using filterAttribute in payload.

        [HttpPost]
        public async Task<EmployeeAdditionalDetailsDTO> GetEmployeeAdditionalDetailsByBasicDetailsUIdUsingFilterAttribute(FilterCriteria filterCriteria)
        {
            var response = await _employeeService.GetEmployeeAdditionalDetailsByBasicDetailsUIdUsingFilterAttribute(filterCriteria);
            return response;
        }


        [HttpGet]
        public async Task<EmployeeAdditionalDetailsDTO> GetEmployeeAdditionalDetailsByBasicDetailsUId(string employeeBasicDetailsUid)
        {
            var response = await _employeeService.GetEmployeeAdditionalDetailsByBasicDetailsUId(employeeBasicDetailsUid);
            return response;
        }



        //import excel
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
                                Mobile = GetStringFromCell(worksheet, row, 4),
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


    }
}
