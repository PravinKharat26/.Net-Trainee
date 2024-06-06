using Newtonsoft.Json;

namespace Visitor_Security_Clearance_System.DTO
{
    public class PassDTO
    {
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]

        public string UId { get; set; }
        [JsonProperty(PropertyName = "visitorName", NullValueHandling = NullValueHandling.Ignore)]

        public string VisitorName { get; set; }
        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]

        public string Email { get; set; }

        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]

        public string Status { get; set; }
    }
}
