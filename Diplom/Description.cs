//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Diplom
{
    using System;
    using System.Collections.Generic;
    
    public partial class Description
    {
        public Description()
        {
            this.Order = new HashSet<Order>();
            this.Service_Done = new HashSet<Service_Done>();
            this.Spares_Used = new HashSet<Spares_Used>();
        }
    
        public int ID { get; set; }
        public string Description1 { get; set; }
        public int MechanicID { get; set; }
        public int CarID { get; set; }
    
        public virtual Car Car { get; set; }
        public virtual Mechanic Mechanic { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Service_Done> Service_Done { get; set; }
        public virtual ICollection<Spares_Used> Spares_Used { get; set; }
    }
}