﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class medcentrDB : DbContext
    {
        public medcentrDB()
            : base("name=medcentrDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Date> Date { get; set; }
        public virtual DbSet<Doctors> Doctors { get; set; }
        public virtual DbSet<Logins> Logins { get; set; }
        public virtual DbSet<Passwords> Passwords { get; set; }
        public virtual DbSet<Patients> Patients { get; set; }
        public virtual DbSet<Time> Time { get; set; }
        public virtual DbSet<Visits> Visits { get; set; }
    }
}
