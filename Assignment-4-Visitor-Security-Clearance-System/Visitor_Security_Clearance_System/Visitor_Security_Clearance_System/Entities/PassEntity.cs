using Newtonsoft.Json;
using Visitor_Security_Clearance_System.Common;

namespace Visitor_Security_Clearance_System.Entities
{
    public class PassEntity:BaseEntity

    {
        [JsonProperty(PropertyName = "visitorName", NullValueHandling = NullValueHandling.Ignore)]

        public string VisitorName { get; set; }
        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]

        public string Email { get; set; }

        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]

        public string Status { get; set; }
    }
}
