//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZH.Models.ZHEF
{
    using System;
    using System.Collections.Generic;
    
    public partial class ResturentTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ResturentTable()
        {
            this.FoodTables = new HashSet<FoodTable>();
        }
    
        public int Id { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodTable> FoodTables { get; set; }
    }
}
