
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
    using System.Runtime.Serialization;

    [DataContract]
    public partial class sp_CartProducts_Result
    {
        [DataMember]
        public int Product_ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public decimal Cost { get; set; }
        [DataMember]
        public string imagePath { get; set; }
        [DataMember]
        public string Details { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public Nullable<decimal> Product_Total { get; set; }
    }
}