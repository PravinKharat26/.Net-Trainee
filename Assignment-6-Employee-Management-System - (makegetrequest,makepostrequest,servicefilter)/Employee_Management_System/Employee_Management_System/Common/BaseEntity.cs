﻿using Newtonsoft.Json;

namespace Employee_Management_System.Common
{
    public class BaseEntity
    {
        [JsonProperty(PropertyName = "employeeId", NullValueHandling = NullValueHandling.Ignore)]
        public string EmployeeID { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]

        public string UId { get; set; }

        [JsonProperty(PropertyName = "dType", NullValueHandling = NullValueHandling.Ignore)]

        public string DocumentType { get; set; }
        [JsonProperty(PropertyName = "version", NullValueHandling = NullValueHandling.Ignore)]

        public int Version { get; set; }
        [JsonProperty(PropertyName = "updatedBy", NullValueHandling = NullValueHandling.Ignore)]

        public string UpdatedBy { get; set; }
        [JsonProperty(PropertyName = "updatedOn", NullValueHandling = NullValueHandling.Ignore)]

        public DateTime UpdatedOn { get; set; }
        [JsonProperty(PropertyName = "createdBy", NullValueHandling = NullValueHandling.Ignore)]

        public string CreatedBy { get; set; }
        [JsonProperty(PropertyName = "createdOn", NullValueHandling = NullValueHandling.Ignore)]

        public DateTime CreatedOn { get; set; }
        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]


        public bool Active { get; set; }
        [JsonProperty(PropertyName = "archived", NullValueHandling = NullValueHandling.Ignore)]

        public bool Archived { get; set; }

        public void Initialize(bool isNew, string dtype, string createdOrUpdatedBy, string createdOrUpdatedByName)
        {
            DocumentType = dtype;
            Id = Guid.NewGuid().ToString();
            EmployeeID = Id;
            
            Active = true;
            Archived = false;

            if (isNew)
            {
                //Adding new Record
                UId = Id;
                CreatedBy = createdOrUpdatedBy;
                CreatedOn = DateTime.UtcNow;
                Version = 1;
                UpdatedBy = createdOrUpdatedBy;
                UpdatedOn = CreatedOn;
            }
            else
            {
                //update the record
                UpdatedBy = createdOrUpdatedBy;
                UpdatedOn = DateTime.UtcNow;
                Version++;
            }
        }
    }
}
