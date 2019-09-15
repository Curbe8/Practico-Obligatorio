using System;
using System.Runtime.Serialization;

namespace Shared.Entities
{
    [DataContract]
    public abstract class Employee
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }
    }
}
