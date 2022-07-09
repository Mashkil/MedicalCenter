using System;
using System.Data.Entity;

namespace MedicalCenter.Models
{
    internal class ContextDb : DbContext
    {
        protected ContextDb() : base("ConnectionString")
        {

        }
    }
}
