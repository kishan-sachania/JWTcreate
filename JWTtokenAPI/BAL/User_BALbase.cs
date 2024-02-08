using JWTtokenAPI.Models;
using JWTtokenAPI.DAL;

namespace JWTtokenAPI.BAL
{
    public class User_BALbase
    {
        public List<UserModel> Selectall()
        {
            try
            {
                User_DALbase dal = new User_DALbase();
                List<UserModel> UserModels = dal.Selectall();
                return UserModels;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public UserModel SellectByID(int ConnectionID)
        {
            try
            {
                User_DALbase dal = new User_DALbase();
                UserModel UserModel = dal.SellectByID(ConnectionID);
                return UserModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool DeleteById(int ConnectionID)
        {
            try
            {
                User_DALbase dal = new User_DALbase();
                if (dal.DeleteById(ConnectionID))
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
                User_DALbase dal = new User_DALbase();
                if (dal.Insert(UserModel))
                    return true;
                else
                    return false;
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
                User_DALbase dal = new User_DALbase();
                if (dal.Update(UserModel))
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
