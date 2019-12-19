using System;
using System.Collections.Generic;
using System.Text;

namespace IOC.Demo
{
    public class UserServer : IUserServer
    {
        private readonly IUserDAL dal;
        private readonly ILoginDAL loginDAL;

        public UserServer()
        {
            this.loginDAL = new LoginDAL();
            this.dal = new UserDAL(loginDAL);
        }

        public UserServer(IUserDAL dal, ILoginDAL loginDAL)
        {
            this.dal = dal;
            this.loginDAL = loginDAL;
        }

        public User Get(int id)
        {
            if (loginDAL.IsLogin())
                return dal.Get(id);
            else
                return null;
        }
    }
}
