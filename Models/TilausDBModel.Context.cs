﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TilausDB2.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TilauksetEntity : DbContext
    {
        public TilauksetEntity()
            : base("name=TilauksetEntity")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Asiakkaat> Asiakkaat { get; set; }
        public virtual DbSet<Henkilot> Henkilot { get; set; }
        public virtual DbSet<Logins> Logins { get; set; }
        public virtual DbSet<Postitoimipaikat> Postitoimipaikat { get; set; }
        public virtual DbSet<Tilaukset> Tilaukset { get; set; }
        public virtual DbSet<Tilausrivit> Tilausrivit { get; set; }
        public virtual DbSet<Tuotteet> Tuotteet { get; set; }
        public virtual DbSet<Myynnit> Myynnit { get; set; }
        public virtual DbSet<Sivustolla_vierailijat> Sivustolla_vierailijat { get; set; }
        public virtual DbSet<Vierailija_kohde> Vierailija_kohde { get; set; }
    }
}
