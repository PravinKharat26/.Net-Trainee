﻿using Newtonsoft.Json;

namespace Visitor_Security_Clearance_System.DTO
{
    public class ManagerDTO
    {
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]

        public string UId { get; set; }


        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]

        public string Name { get; set; }
        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]

        public string Email { get; set; }
        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]

        public string Password { get; set; }
        [JsonProperty(PropertyName = "phoneNumber", NullValueHandling = NullValueHandling.Ignore)]

        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]

        public string Address { get; set; }

        [JsonProperty(PropertyName = "age", NullValueHandling = NullValueHandling.Ignore)]

        public string Age { get; set; }

    }
}