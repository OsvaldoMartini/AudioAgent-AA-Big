using System;
using System.Runtime.Serialization;

namespace AudioAgent.Services.DataTransferObject
{
    [DataContract(Name = "BaseAccess")]
    public class BaseAccessDto
    {
        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public string NameUserRole { get; set; }
    }
}