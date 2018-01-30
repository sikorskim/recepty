namespace recepty
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using System.Reflection;

    public class Model1 : DbContext
    {
        // Your context has been configured to use a 'Model1' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'recepty.Model1' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model1' 
        // connection string in the application configuration file.
        public Model1()
            : base("name=Model1")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Gabinet> Gabinet { get; set; }
        public virtual DbSet<Lek> Lek { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<PrescriptionNumber> PrescriptionNumber { get; set; }
        public virtual DbSet<OddzialNFZ> NFZDepartament { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<PulaRecept> PulaRecept { get; set; }
        public virtual DbSet<Prescription> Prescription { get; set; }
        public virtual DbSet<Refundacja> Refundacja { get; set; }
        public virtual DbSet<Diagnostics> Diagnostics { get; set; }
        public virtual DbSet<PrescriptionItem> PrescriptionItem { get; set; }
        public virtual DbSet<Uprawnienie> Uprawnienie { get; set; }

    }
}