//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookStore.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class NoiDungSach
    {
        public string maSach { get; set; }
        public string noiDung { get; set; }
    
        public virtual Sach Sach { get; set; }
    }
}
