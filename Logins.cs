//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MedicalCenter
{
    using System;
    using System.Collections.Generic;
    
    public partial class Logins
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public int PassId { get; set; }
    
        public virtual Passwords Passwords { get; set; }
    }
}
