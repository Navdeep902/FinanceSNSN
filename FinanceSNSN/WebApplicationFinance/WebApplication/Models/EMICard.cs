//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public partial class EMICard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMICard()
        {
            this.Cart = new HashSet<Cart>();
            this.Transactions = new HashSet<Transactions>();
        }
        [DataMember]
        public int Card_Number { get; set; }
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public Nullable<bool> Card_Type { get; set; }
        [DataMember]
        public System.DateTime valid { get; set; }
        [DataMember]
        public bool Active { get; set; }
        [DataMember]
        public string admin_username { get; set; }

        public virtual Admin Admin { get; set; }
        public virtual Card Card { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Cart { get; set; }
        public virtual RegisterBank RegisterBank { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}