namespace Employee_Management_System.Common
{
    public class Credentials
    {
        public static readonly string containerName = Environment.GetEnvironmentVariable("containerName");
        public static readonly string databaseName = Environment.GetEnvironmentVariable("databaseName");
        public static readonly string CosmosEndpoint = Environment.GetEnvironmentVariable("cosmosUrl");
        public static readonly string PrimaryKey = Environment.GetEnvironmentVariable("primaryKey");
        public static readonly string EmployeeDocumentType = "employee";

        public static readonly string EmployeeUrl = Environment.GetEnvironmentVariable("employeeUrl");
        public static readonly string AddEmployeeBasicEndpoint ="/api/Employee/AddEmployeeBasicDetails";

        public static readonly string GetAllEmployeeBasicEndpoint = "/api/Employee/GetAllEmployeesBasicDetails";

        public static readonly string AddEmployeeAdditionalEndpoint = "/api/Employee/AddEmployeeAdditionalDetails";
        public static readonly string GetAllEmployeeAdditionalEndpoint = "/api/Employee/GetAllEmployeesAdditionalDetails";

        public static readonly string VisitorUrl = Environment.GetEnvironmentVariable("visitorUrl");
        public static readonly string AddSecurityEndpoint = "/api/VisitorSecurityContoller/CreateSecurity";
        public static readonly string GetAllSecurityEndpoint = "/api/VisitorSecurityContoller/ReadAllSecurity";

    }
}