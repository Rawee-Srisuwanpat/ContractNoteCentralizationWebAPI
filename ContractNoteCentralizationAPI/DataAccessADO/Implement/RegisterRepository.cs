using ContractNoteCentralizationAPI.DataAccess.Interface;
using ContractNoteCentralizationAPI.Helper.DB;
using ContractNoteCentralizationAPI.Model.UsersAuthentication;
using System.Data;
using System.Data.SqlClient;

namespace ContractNoteCentralizationAPI.DataAccess.Implement
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly ConnectionStringsModel _connectionStringsModel;
        private readonly DBHelpers _dBHelpers;


        public RegisterRepository(DBHelpers _dBHelpers, ConnectionStringsModel _connectionStringsModel)
        {
            this._connectionStringsModel = _connectionStringsModel;
            this._dBHelpers = _dBHelpers;
        }
        public int Delete()
        {
            throw new NotImplementedException();
        }

        public int GetById()
        {
            throw new NotImplementedException();
        }

        public DataSet Insert()
        {
            List<SqlParameter> paramCollection = new List<SqlParameter>();

            //paramCollection.Add(new SqlParameter("@p_req", req));

            return this._dBHelpers.DBExecuteSP_ReturnDs(_connectionStringsModel.ContractNoteCentralizationConnection, StoredProcedureContractNoteCentralization.spInsertRegister, paramCollection);
        }

        public int Update()
        {
            throw new NotImplementedException();
        }
    }
}
