using Newtonsoft.Json;

namespace Visitor_Security_Clearance_System.DTO
{
    public class OfficeDTO
    {
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]

        public string UId { get; set; }
        [JsonProperty(PropertyName = "officeName", NullValueHandling = NullValueHandling.Ignore)]

        public string OfficeName { get; set; }
        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]

        public string Email { get; set; }
        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]

        public string Password { get; set; }
        [JsonProperty(PropertyName = "phoneNumber", NullValueHandling = NullValueHandling.Ignore)]

        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]

        public string Address { get; set; }

        [JsonProperty(PropertyName = "floor", NullValueHandling = NullValueHandling.Ignore)]

        public string Floor { get; set; }

    }
}
