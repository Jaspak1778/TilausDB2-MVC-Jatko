//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TilausDB2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Myynnit
    {
        public int Vuosi { get; set; }
        public Nullable<decimal> Kokonaismyynti { get; set; }
    
        public virtual Sivustolla_vierailijat Sivustolla_vierailijat { get; set; }
    }
}
