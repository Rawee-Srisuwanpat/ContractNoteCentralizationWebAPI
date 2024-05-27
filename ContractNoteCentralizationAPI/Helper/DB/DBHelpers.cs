using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ContractNoteCentralizationAPI.Helper.DB
{
    public class DBHelpers
    {

        private readonly ConnectionStringsModel _connectionStringsModel;

        public SqlConnection con;
        public SqlCommand cmd;
        public SqlTransaction transaction;

        public DBHelpers()
        {
            con = null;
            this._connectionStringsModel = new ConnectionStringsModel();
        }
        public void OpenConnection()
        {
            //ExporterManagementEntity db = new ExporterManagementEntity();

            // con = new SqlConnection(EncryptionHelper.Decrypt(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString));

            con = new SqlConnection(_connectionStringsModel.ContractNoteCentralizationConnection);

            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
            con.Open();
        }
        public void CloseConnection()
        {
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
        }
        public int? DBExecuteScalar(CommandType commandType, string commandTex, List<SqlParameter> Param)
        {
            int? Result = -1;
            try
            {
                this.OpenConnection();
                transaction = con.BeginTransaction();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandTimeout = 180;
                cmd.Transaction = transaction;
                cmd.CommandType = commandType;
                cmd.CommandText = commandTex;
                cmd.Parameters.Clear();
                foreach (SqlParameter p in Param)
                    cmd.Parameters.Add(p);
                Result = (int?)cmd.ExecuteScalar();
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();
                throw ex;
            }
            finally
            {
                this.CloseConnection();
                con.Dispose();
            }
            return Result;
        }
        public int? DBExecuteNonQuery(CommandType commandType, string commandText)
        {
            int? Result = -1;
            try
            {
                this.OpenConnection();
                transaction = con.BeginTransaction();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandTimeout = 180;
                cmd.Transaction = transaction;
                cmd.CommandType = commandType;
                cmd.CommandText = commandText;
                Result = cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();
                throw ex;
            }
            finally
            {
                this.CloseConnection();
                con.Dispose();
            }
            return Result;
        }
        public int? DBExecuteNonQuery(CommandType commandType, string commandTex, List<SqlParameter> Param)
        {
            int? Result = -1;
            try
            {
                this.OpenConnection();
                transaction = con.BeginTransaction();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandTimeout = 180;
                cmd.Transaction = transaction;
                cmd.CommandType = commandType;
                cmd.CommandText = commandTex;
                cmd.Parameters.Clear();
                foreach (SqlParameter p in Param)
                    cmd.Parameters.Add(p);
                Result = cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();
                throw ex;
            }
            finally
            {
                this.CloseConnection();
                con.Dispose();
            }
            return Result;
        }
        public int? DBExecuteNonQuery(CommandType commandType, string commandTex, List<SqlParameter> Param, SqlParameter ParmOutput = null)
        {
            int? Result = -1;
            cmd = new SqlCommand();
            try
            {
                this.OpenConnection();
                transaction = con.BeginTransaction();

                cmd.Connection = con;
                cmd.CommandTimeout = 180;
                cmd.Transaction = transaction;
                cmd.CommandType = commandType;
                cmd.CommandText = commandTex;
                cmd.Parameters.Clear();
                foreach (SqlParameter p in Param)
                    cmd.Parameters.Add(p);
                if (ParmOutput != null)
                    cmd.Parameters.Add(ParmOutput);
                Result = cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();
                throw ex;
            }
            finally
            {
                this.CloseConnection();
                con.Dispose();
            }
            if (ParmOutput != null)
                return (int?)cmd.Parameters[ParmOutput.ParameterName].Value;
            return Result;
        }
        public int? DBExecuteNonQueryInt(CommandType commandType, string commandTex)
        {
            int? Result = -1;
            try
            {
                this.OpenConnection();
                transaction = con.BeginTransaction();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandTimeout = 180;
                cmd.Transaction = transaction;
                cmd.CommandType = commandType;
                cmd.CommandText = commandTex;
                Result = (int?)cmd.ExecuteScalar();
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();
                throw ex;
            }
            finally
            {
                this.CloseConnection();
                con.Dispose();
            }
            return Result;
        }
        public DataTable DBExecuteSelect(CommandType commandType, string commandTex)
        {
            DataTable dt = new DataTable();
            try
            {
                this.OpenConnection();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandTimeout = 180;
                cmd.Transaction = transaction;
                cmd.CommandType = commandType;
                cmd.CommandText = commandTex;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                return dt;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConnection();
                con.Dispose();
            }

        }
        public DataTable DBExecuteSelect(CommandType commandType, string commandText, List<SqlParameter> Param)
        {
            DataTable dt = new DataTable();
            try
            {
                this.OpenConnection();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandTimeout = 180;
                cmd.Transaction = transaction;
                cmd.CommandType = commandType;
                cmd.CommandText = commandText;
                cmd.Parameters.Clear();
                foreach (SqlParameter p in Param)
                    cmd.Parameters.Add(p);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                return dt;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConnection();
                con.Dispose();
            }
        }
        public DataTable DBExecuteSP_ReturnDt(string connstring, string sp_name, List<SqlParameter> Param)
        {

            DataTable dt = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                cmd.CommandText = sp_name;
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter p in Param)
                    cmd.Parameters.AddWithValue(p.ParameterName, p.Value);

                cmd.Connection = sqlConnection;
                cmd.CommandTimeout = 3600;

                sqlConnection.Open();
                reader = cmd.ExecuteReader();

                dt.Load(reader);
            }

            return dt;
        }
        public void DBExecuteSP_NonQuery(string connstring, string sp_name, List<SqlParameter> Param)
        {

            DataTable dt = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = sp_name;
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter p in Param)
                    cmd.Parameters.AddWithValue(p.ParameterName, p.Value);

                cmd.Connection = sqlConnection;
                cmd.CommandTimeout = 3600;
                sqlConnection.Open();
                cmd.ExecuteNonQuery();

            }
        }

        public DataSet DBExecuteSP_ReturnDs(string connstring, string sp_name, List<SqlParameter> Param)
        {

            DataSet ds = new DataSet();

            using (SqlConnection sqlConnection = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter adapter = new SqlDataAdapter();

                cmd.CommandText = sp_name;
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter p in Param)
                    cmd.Parameters.AddWithValue(p.ParameterName, p.Value);

                cmd.Connection = sqlConnection;
                cmd.CommandTimeout = 3600;

                sqlConnection.Open();

                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            }

            return ds;
        }

        
    }
}
