using Employee_Management_System.Common;
using Employee_Management_System.DTO;
using Newtonsoft.Json;

namespace Employee_Management_System.Entities
{
    public class EmployeeAdditionalDetailsEntity:BaseEntity
    {
        [JsonProperty(PropertyName = "employeeBasicDetailsUid", NullValueHandling = NullValueHandling.Ignore)]
        public string EmployeeBasicDetailsUId { get; set; }

        [JsonProperty(PropertyName = "alternateEmail", NullValueHandling = NullValueHandling.Ignore)]
        public string AlternateEmail { get; set; }

        [JsonProperty(PropertyName = "alternateMobile", NullValueHandling = NullValueHandling.Ignore)]
        public string AlternateMobile { get; set; }

        [JsonProperty(PropertyName = "workInformation", NullValueHandling = NullValueHandling.Ignore)]
        public WorkInfo_ WorkInformation { get; set; }

        [JsonProperty(PropertyName = "personnalDetails", NullValueHandling = NullValueHandling.Ignore)]
        public PersonalDetails_ PersonalDetails { get; set; }

        [JsonProperty(PropertyName = "identityInformation", NullValueHandling = NullValueHandling.Ignore)]
        public IdentityInfo_ IdentityInformation { get; set; }
    }
    public class WorkInfo_
    {
        [JsonProperty(PropertyName = "designationName", NullValueHandling = NullValueHandling.Ignore)]
        public string DesignationName { get; set; }

        [JsonProperty(PropertyName = "departmentName", NullValueHandling = NullValueHandling.Ignore)]
        public string DepartmentName { get; set; }

        [JsonProperty(PropertyName = "locationName", NullValueHandling = NullValueHandling.Ignore)]
        public string LocationName { get; set; }

        [JsonProperty(PropertyName = "employeeStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string EmployeeStatus { get; set; } // Terminated, Active, Resigned etc

        [JsonProperty(PropertyName = "sourceOfHire", NullValueHandling = NullValueHandling.Ignore)]
        public string SourceOfHire { get; set; }

        [JsonProperty(PropertyName = "dateOfJoining", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime DateOfJoining { get; set; }
    }
    public class PersonalDetails_
    {
        [JsonProperty(PropertyName = "dateOfBirth", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty(PropertyName = "age", NullValueHandling = NullValueHandling.Ignore)]
        public string Age { get; set; }

        [JsonProperty(PropertyName = "gender", NullValueHandling = NullValueHandling.Ignore)]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "religion", NullValueHandling = NullValueHandling.Ignore)]
        public string Religion { get; set; }

        [JsonProperty(PropertyName = "caste", NullValueHandling = NullValueHandling.Ignore)]
        public string Caste { get; set; }

        [JsonProperty(PropertyName = "maritalStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string MaritalStatus { get; set; }

        [JsonProperty(PropertyName = "bloodGroup", NullValueHandling = NullValueHandling.Ignore)]
        public string BloodGroup { get; set; }

        [JsonProperty(PropertyName = "height", NullValueHandling = NullValueHandling.Ignore)]
        public string Height { get; set; }

        [JsonProperty(PropertyName = "weight", NullValueHandling = NullValueHandling.Ignore)]
        public string Weight { get; set; }
    }

    public class IdentityInfo_
    {
        [JsonProperty(PropertyName = "pan", NullValueHandling = NullValueHandling.Ignore)]
        public string PAN { get; set; }

        [JsonProperty(PropertyName = "aadhar", NullValueHandling = NullValueHandling.Ignore)]
        public string Aadhar { get; set; }

        [JsonProperty(PropertyName = "nationality", NullValueHandling = NullValueHandling.Ignore)]
        public string Nationality { get; set; }

        [JsonProperty(PropertyName = "passportNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PassportNumber { get; set; }

        [JsonProperty(PropertyName = "pfNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PFNumber { get; set; }
    }
    public class EmployeeAdditionalFilterCriteria
    {
        public EmployeeAdditionalFilterCriteria()
        {
            Filters = new List<FilterCriteria>();
            Employees = new List<EmployeeAdditionalDetailsDTO>();
        }
        public int Page { get; set; }

        public int PageSize { get; set; }
        public int totalCount { get; set; }
        public List<FilterCriteria> Filters { get; set; }
        public List<EmployeeAdditionalDetailsDTO> Employees { get; set; }
    }

    public class FilterCriteria
    {
        public string FieldName { get; set; }

        public string FieldValue { get; set; }
    }
}
