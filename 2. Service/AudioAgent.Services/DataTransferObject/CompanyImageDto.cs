using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AudioAgent.Services.DataTransferObject
{
    [DataContract(Name = "CompanyImage")]
    public class CompanyImageDto : BaseAccessDto
    {
        public CompanyImageDto()
        {
            this.TCompany = new CompanyDto();
            this.CompanyImageList = new HashSet<CompanyImageDto>();
        }
        
        [DataMember]
        public int CompanyImageID { get; set; }
        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        [Display(Name = "ImageSizeMB")]
        public int ImageSizeMB { get; set; }

        [DataMember]
        [Display(Name = "ImageExtension")]
        public string ImageExtension { get; set; }
        
        [DataMember]
        [Display(Name = "ImageUrl")]
        public string ImageUrl { get; set; }

        [DataMember]
        [Display(Name = "Lat")]
        public string Lat { get; set; }

        [DataMember]
        [Display(Name = "Lng")]
        public string Lng { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public virtual CompanyDto TCompany { get; set; }

        [DataMember]
        public virtual ICollection<CompanyImageDto> CompanyImageList { get; set; }

    }
}