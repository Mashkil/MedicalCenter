using System;
using System.Collections.Generic;
namespace MedicalCenter.Models
{
    internal class Doctor
    {
        public int Id { get; set; }
        public string Fitstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public int? Age { get; set; }
        public string Phone { get; set; }
        public double Salary { get; set; }
        public int Expirience { get; set; }
        public string Education { get; set; }
        public string Position { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
    }
}
