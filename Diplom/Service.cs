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
    
    public partial class Service
    {
        public Service()
        {
            this.Service_Done = new HashSet<Service_Done>();
        }
    
        public int ID { get; set; }
        public string Naming { get; set; }
        public int Cost { get; set; }
    
        public virtual ICollection<Service_Done> Service_Done { get; set; }
    }
}
