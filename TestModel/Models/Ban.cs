//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestModel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ban
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ban()
        {
            this.DatBans = new HashSet<DatBan>();
        }
    
        public string MaBan { get; set; }
        public string TenPhong { get; set; }
        public string MaLoai { get; set; }
        public Nullable<int> DienTich { get; set; }
        public Nullable<int> GiaThue { get; set; }
        public string MieuTa { get; set; }
    
        public virtual LoaiBan LoaiBan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatBan> DatBans { get; set; }
    }
}
