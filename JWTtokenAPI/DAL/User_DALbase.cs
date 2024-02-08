using JWTtokenAPI.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using System.Windows.Input;

namespace JWTtokenAPI.DAL
{
    public class User_DALbase : DAL_Helpers
    {
        public List<UserModel> Selectall()
        {
            try
            {
                SqlDatabase sqldatabase = new SqlDatabase(ConString);
                DbCommand dbCommand = sqldatabase.GetStoredProcCommand("PR_User_SelectAll");
                List<UserModel> UserModel = new List<UserModel>();
                using (IDataReader dr = sqldatabase.ExecuteReader(dbCommand))
                {
                    while (dr.Read())
                    {
                        UserModel UserModels = new UserModel();
                        UserModels.UserId = Convert.ToInt32(dr["UserID"].ToString());
                        UserModels.name = dr["name"].ToString();
                        UserModels.email = dr["email"].ToString();
                        UserModels.password = dr["password"].ToString();
                        UserModels.location = dr["location"].ToString();
                        UserModel.Add(UserModels);
                    }
                }
                return UserModel;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public UserModel SellectByID(int UserID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_User_SelectByPK");
                sqlDatabase.AddInParameter(dbCommand, "@userID", SqlDbType.Int, UserID);
                UserModel UserModel = new UserModel();
                using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand))
                {
                    while (dr.Read())
                    {
                        UserModel.UserId = Convert.ToInt32(dr["UserID"].ToString());
                        UserModel.name = dr["name"].ToString();
                        UserModel.email = dr["email"].ToString();
                        UserModel.password = dr["password"].ToString();
                        UserModel.location = dr["location"].ToString();
                    }
                }

                return UserModel;

            }

            catch (Exception ex)
            {

                return null;
            }
        }

        public bool DeleteById(int UserID)
        {

            try
            {
                SqlDatabase sqldatabase = new SqlDatabase(ConString);
                DbCommand dbCommand = sqldatabase.GetStoredProcCommand("PR_User_DeleteByPK");
                sqldatabase.AddInParameter(dbCommand, "@userID", SqlDbType.Int, UserID);

                if (Convert.ToBoolean(sqldatabase.ExecuteNonQuery(dbCommand)))
                {
                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool Insert(UserModel UserModel)
        {
            try
            {

                SqlDatabase sqlDatabase = new SqlDatabase(ConString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_User_Insert");
                sqlDatabase.AddInParameter(dbCommand, "@name", SqlDbType.VarChar, UserModel.name);
                sqlDatabase.AddInParameter(dbCommand, "@email", SqlDbType.VarChar, UserModel.email);
                sqlDatabase.AddInParameter(dbCommand, "@password", SqlDbType.VarChar, UserModel.password);
                sqlDatabase.AddInParameter(dbCommand, "@location", SqlDbType.VarChar, UserModel.location);


                UserModel personModel = new UserModel();
                if (Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Update(UserModel UserModel)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(ConString);
                DbCommand cmd = sqldb.GetStoredProcCommand("PR_User_UpdateByPK");
                sqldb.AddInParameter(cmd, "@userID", SqlDbType.Int, UserModel.UserId);
                sqldb.AddInParameter(cmd, "@name", SqlDbType.VarChar, UserModel.name);
                sqldb.AddInParameter(cmd, "@email", SqlDbType.VarChar, UserModel.email);
                sqldb.AddInParameter(cmd, "@password", SqlDbType.VarChar, UserModel.password);
                sqldb.AddInParameter(cmd, "@location", SqlDbType.VarChar, UserModel.location);
                if (Convert.ToBoolean(sqldb.ExecuteNonQuery(cmd)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }


    }
}
