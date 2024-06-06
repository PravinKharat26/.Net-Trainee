using Newtonsoft.Json;

namespace Visitor_Security_Clearance_System.DTO
{
    public class LoginDTO
    {
        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]

        public string Email { get; set; }

        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]

        public string Password { get; set; }
    }
}
