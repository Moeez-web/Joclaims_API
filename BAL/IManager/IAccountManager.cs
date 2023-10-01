using MODEL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IManager
{
    public interface IAccountManager
    {
        string Register(User userObj);
        string SaveEmailConfirmationToken(string UserId, string token);
        SignInResponse SignIn(User userObj);
        string VerifyEmail(User user);
        User FindForgotEmail(User userObj);
        string SavePasswordResetToken(string UserId, string token);
        string ResetPassword(User userObj);
        string LogoutUser(int UserID);
        string DeleteAccount(int UserID);
    }
}
