using System;
using System.Collections.Generic;
using System.Text;

namespace Incident_Library.MODELS__Data_
{
    class User
    {
        private int _userId;
        private string _username;
        private string _password;
        private int _roleId;

        public User (int userId, string username, string password, int roleId)
        {
            _userId = userId;
            _username = username;
            _password = password;
            _roleId = roleId;
        }

       
       
    }
}
