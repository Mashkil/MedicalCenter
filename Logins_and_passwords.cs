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
    
    public partial class Logins_and_passwords
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Nullable<int> Admin_or_doctor { get; set; }
        public Nullable<int> Id_doc { get; set; }
        public Nullable<int> Id_admin { get; set; }
    
        public virtual Admins Admins { get; set; }
        public virtual Doctors Doctors { get; set; }
    }
}
