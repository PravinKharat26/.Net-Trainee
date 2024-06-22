using Newtonsoft.Json;
using Employee_Management_System.Common;
using Employee_Management_System.Services;
using Employee_Management_System.DTO;

namespace Employee_Management_System.Entities
{
    public class EmployeeBasicDetailsEntity : BaseEntity
    {

        [JsonProperty(PropertyName = "salutory", NullValueHandling = NullValueHandling.Ignore)]
        public string Salutory { get; set; }

        [JsonProperty(PropertyName = "firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "middleName", NullValueHandling = NullValueHandling.Ignore)]
        public string MiddleName { get; set; }

        [JsonProperty(PropertyName = "lastName", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "nickName", NullValueHandling = NullValueHandling.Ignore)]
        public string NickName { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "mobile", NullValueHandling = NullValueHandling.Ignore)]
        public string Mobile { get; set; }

        [JsonProperty(PropertyName = "role", NullValueHandling = NullValueHandling.Ignore)]
        public string Role { get; set; }

        [JsonProperty(PropertyName = "reportingManagerUid", NullValueHandling = NullValueHandling.Ignore)]
        public string ReportingManagerUId { get; set; }

        [JsonProperty(PropertyName = "reportingManagerName", NullValueHandling = NullValueHandling.Ignore)]
        public string ReportingManagerName { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }
    }

    public class EmployeeBasicFilterCriteria
    {
        public EmployeeBasicFilterCriteria() 
        {
         Filters=new List<FilterCriteria2>();
         Employees = new List<EmployeeBasicDetailsDTO>();
        }
        public int Page { get; set; }
        
        public int PageSize {  get; set; }  
        public int totalCount {  get; set; }
        public List<FilterCriteria2> Filters { get; set; }   
        public List<EmployeeBasicDetailsDTO> Employees { get; set; }
    }

    public class FilterCriteria2
    {
        public string FieldName { get; set; }

        public string FieldValue { get; set; }
    }
}
