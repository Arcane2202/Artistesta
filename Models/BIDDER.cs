//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Artistesta.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BIDDER
    {
        public long BIDID { get; set; }
        public Nullable<int> BIDDERID { get; set; }
        public Nullable<long> ARTID { get; set; }
        public Nullable<long> BIDAMOUNT { get; set; }
    
        public virtual PUBLISHART PUBLISHART { get; set; }
        public virtual USER USER { get; set; }
    }
}