
using System;
using System.Collections.Generic;

namespace AudioAgent.Data
{
    public partial class CompanyImageEntity
    {
        public CompanyImageEntity()
        {
            this.TCompany = new CompanyEntity();
            this.CompanyImageList = new HashSet<CompanyImageEntity>();

        }

        public int CompanyImageID { get; set; }
        public int CompanyID { get; set; }
        public int ImageSizeMB { get; set; }
        public string ImageExtension { get; set; }
        public string ImageUrl { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public Nullable<DateTime> DateCreated { get; set; }

        public virtual CompanyEntity TCompany { get; set; }

        public virtual ICollection<CompanyImageEntity> CompanyImageList { get; set; }
    }
}
