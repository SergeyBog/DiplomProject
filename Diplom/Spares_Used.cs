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
    
    public partial class Spares_Used
    {
        public int ID { get; set; }
        public int DecriptionID { get; set; }
        public int SpareID { get; set; }
    
        public virtual Description Description { get; set; }
        public virtual Spare Spare { get; set; }
    }
}
