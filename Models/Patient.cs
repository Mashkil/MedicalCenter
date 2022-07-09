using System;
using System.Collections.Generic;


namespace MedicalCenter.Models
{
    internal class Patient
    {
        public int Id { get; set; }
        public string Fitstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public int? Age { get; set; }
        public string Phone { get; set; }
        public string Chronic_deseases { get; set; }
        
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
