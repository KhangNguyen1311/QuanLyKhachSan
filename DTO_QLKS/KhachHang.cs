//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DTO_QLKS
{
    using System;
    using System.Collections.Generic;
    
    public partial class KhachHang
    {
        public KhachHang()
        {
            this.PhieuDatPhongs = new HashSet<PhieuDatPhong>();
        }
    
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public string SDT { get; set; }
        public string NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public bool Phai { get; set; }
        public string MaCMT { get; set; }
        public string QuocTich { get; set; }
    
        public virtual ICollection<PhieuDatPhong> PhieuDatPhongs { get; set; }
    }
}
