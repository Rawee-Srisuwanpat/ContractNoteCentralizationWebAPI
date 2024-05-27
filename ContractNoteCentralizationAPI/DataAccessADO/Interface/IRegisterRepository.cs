using ContractNoteCentralizationAPI.Model.UsersAuthentication;
using System.Data;

namespace ContractNoteCentralizationAPI.DataAccess.Interface
{
    public interface IRegisterRepository
    {
        public DataSet Insert();
        public int Update();
        public int Delete();
        public int GetById();
    }
}
