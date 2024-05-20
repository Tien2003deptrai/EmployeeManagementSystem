using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserService
    {
        private UserRepository _userDAL;

        public UserService()
        {
            _userDAL = new UserRepository();
        }

        public bool Login(string username, string password)
        {
            return _userDAL.Login(username, password);
        }

        public void RegisterUser(string username, string password)
        {
            _userDAL.RegisterUser(username, password);
        }
    }
}
