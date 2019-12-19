using System;
using System.Collections.Generic;
using System.Text;

namespace IOC.Demo
{
    public class UserDAL : IUserDAL
    {
        private readonly ILoginDAL loginDAL;

        public UserDAL(ILoginDAL loginDAL)
        {
            this.loginDAL = loginDAL;
        }

        public User Get(int id)
        {
            if (loginDAL.IsLogin())
                return new User
                {
                    Id = id,
                    Name = "ffg"
                };
            else
                return null;
        }
    }
}
