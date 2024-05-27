
﻿using ContractNoteCentralizationAPI.Model.ContactNote;
using ContractNoteCentralizationAPI.Model.Log;
﻿using ContractNoteCentralizationAPI.Model.ActionCode;
using ContractNoteCentralizationAPI.Model.LogLogin;
using ContractNoteCentralizationAPI.Model.ManageRole;
using ContractNoteCentralizationAPI.Model.ManagerRegister;
using ContractNoteCentralizationAPI.Model.ManageUser;
using ContractNoteCentralizationAPI.Model.Master;
using ContractNoteCentralizationAPI.Model.MasterSystem;
using ContractNoteCentralizationAPI.Model.ResultCode;
using Microsoft.EntityFrameworkCore;
using ContractNoteCentralizationAPI.Model.CollectorTeamCode;
using ContractNoteCentralizationAPI.Model.ChatBot;

namespace ContractNoteCentralizationAPI.Helper.ContextDb
{
    public class ChatBotDbContext : DbContext
    {
        public ChatBotDbContext(DbContextOptions<ChatBotDbContext> options) : base(options)
        {
        }

        public DbSet<ContractDetailDao> tbt_contract_detail { get; set; }

    }
}
