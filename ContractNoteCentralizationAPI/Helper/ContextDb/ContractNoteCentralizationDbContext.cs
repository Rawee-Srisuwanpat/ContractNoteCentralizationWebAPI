
using ContractNoteCentralizationAPI.Model.ContactNote;
using ContractNoteCentralizationAPI.Model.Jwt;
using ContractNoteCentralizationAPI.Model.Log;

using ContractNoteCentralizationAPI.Model.ActionCode;
using ContractNoteCentralizationAPI.Model.ContactNote;

using ContractNoteCentralizationAPI.Model.ActionCode;
using ContractNoteCentralizationAPI.Model.ContactNote;
using ContractNoteCentralizationAPI.Model.LogLogin;
using ContractNoteCentralizationAPI.Model.ManageRole;
using ContractNoteCentralizationAPI.Model.ManagerRegister;
using ContractNoteCentralizationAPI.Model.ManageUser;
using ContractNoteCentralizationAPI.Model.Master;
using ContractNoteCentralizationAPI.Model.MasterSystem;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using ContractNoteCentralizationAPI.Model.ResultCode;

using ContractNoteCentralizationAPI.Model.ResultCode;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ContractNoteCentralizationAPI.Model.CollectorTeamCode;
using ContractNoteCentralizationAPI.Model.Contract;

namespace ContractNoteCentralizationAPI.Helper.ContextDb
{
    public class ContractNoteCentralizationDbContext : DbContext
    {
        public ContractNoteCentralizationDbContext(DbContextOptions<ContractNoteCentralizationDbContext> options) : base(options)
        {
        }

        public DbSet<MasterDao> tbm_master { get; set; }
        public DbSet<MasterSystemDto> tbm_master_system { get; set; }

        public DbSet<TbmRoleDoa> tbm_role { get; set; }


        //public DbSet<ContactNoteDao> tbt_contact_note { get; set; }
        public DbSet<LogDao> tbt_log { get; set; }

        public DbSet<ManageRegisterDao> tbt_register { get; set; }

        public DbSet<ManageUserDao> tbt_manage_user { get; set; }

        public DbSet<ManageRoleDao> tbt_manage_role { get; set; }

        public DbSet<LogLginDao> tbt_log_login { get; set; }

        public DbSet<CollectorDetailDao> tbm_collector { get; set; }

        public DbSet<ContractDao> tbt_contract { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactNoteDao>(entity =>
            {
                entity.ToTable("tbt_contact_note").HasKey(p => p.contact_note_id);
              
                // entity.ToTable("tbt_contact_note").HasIndex(e => e.contract_no)
                //.IsUnique(false) // Optional: Include this line if you want to make the index non-unique
                //.IsClustered(false); // Set to false to make it non-clustered

            });

            modelBuilder.Entity<ContactNoteIquiryDao>(entity =>
            {
                entity.ToTable("tbt_contact_note_inquiy").HasKey(p => p.contact_note_id);
            });


            modelBuilder.Entity<CollectorDetailDao>(entity =>
            {
                entity.ToTable("tbm_collector").HasKey(p => p.collector_id);

            });


            modelBuilder.Entity<LogDao>(entity =>
            {
                entity.ToTable("tbt_log").HasKey(p => p.id);
            });
        }

        public DbSet<ContactNoteDao> tbt_contact_note { get; set; }

        public DbSet<ActionCodeDao> tbm_action_code { get; set; }

        public DbSet<ResultCodeDao> tbm_result_code { get; set; }
        //public DbSet<CollectorCodeDao> tbm_collector { get; set; }
        public DbSet<CollectorTeamDao> tbm_collector_team { get; set; }

        public DbSet<ContactNoteIquiryDao> tbt_contact_note_inquiy { get; set; }

        
    }
}
