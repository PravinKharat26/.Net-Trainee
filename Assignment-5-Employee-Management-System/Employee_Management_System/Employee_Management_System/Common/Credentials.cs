namespace Employee_Management_System.Common
{
    public class Credentials
    {
        public static readonly string containerName = Environment.GetEnvironmentVariable("containerName");
        public static readonly string databaseName = Environment.GetEnvironmentVariable("databaseName");
        public static readonly string CosmosEndpoint = Environment.GetEnvironmentVariable("cosmosUrl");
        public static readonly string PrimaryKey = Environment.GetEnvironmentVariable("primaryKey");
        public static readonly string EmployeeDocumentType = "employee";
    }
}
