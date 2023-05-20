using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebSignup.CommonUtility;
using WebSignup.DAL;
using WebSignup.DBContext;

namespace WebSignup.RegistrationDAL
{
    public sealed class DataAccessLayer
    {
        private readonly MyConnection _connection;
        private static DataAccessLayer _dataAccessLayer = null;
        private DataAccessLayer(MyConnection myConnection) { 
            _connection = myConnection;
        }
        public static DataAccessLayer getInstance(MyConnection connection)
        {
            if (_dataAccessLayer == null)
            {
                _dataAccessLayer = new DataAccessLayer(connection);
            }
           // UpdateDemographics(1, "", "Test");
            return _dataAccessLayer;
        }
        public int dbAddUser(UserEntity lUser)
        {
            _connection._Users.Add(lUser);
            _connection.SaveChanges();
            return lUser.UserID;
        }
        public void dbCreateUserCredentials(UsersCred usersCred)
        {
            _connection._UsersCred.Add(usersCred);
            _connection.SaveChanges();
        }
        public UserEntity dbFindUserByUserName(string userName)
        {
            return _connection._Users.Find(userName);
        }
        public bool dbAuthenticateUser(string userName,string astrPassword)
        {
            bool lblnResult = false;
            UsersCred lobjCred = _connection._UsersCred.Find(userName);
            if(lobjCred == null)
            {
                return lblnResult;
            }
            else
            {
                string lstrDecryptPassword = EncryptUtility.DecodeFrom64(lobjCred.Password);
                if(astrPassword == lstrDecryptPassword)
                {
                    lblnResult = true;
                }
            }
            return lblnResult;
        }
        private static void UpdateDemographics(Int32 customerID, string demoXml, string connectionString)
        {
            // Update the demographics for a store, which is stored
            // in an xml column.
            string commandText = "UPDATE Sales.Store SET Demographics = @demographics "
                + "WHERE CustomerID = @ID;";
            connectionString = "Server=SID\\SQLEXPRESS;Database=AuthenicationServiceDB;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@ID", SqlDbType.Int);
                command.Parameters["@ID"].Value = customerID;

                // Use AddWithValue to assign Demographics.
                // SQL Server will implicitly convert strings into XML.
                command.Parameters.AddWithValue("@demographics", demoXml);

                try
                {
                    connection.Open();
                    Int32 rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("RowsAffected: {0}", rowsAffected);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
