using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSEP_DLL.Models
{
    public class RegisterBindingModel
    {        
        public String Email { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String ConfirmPassword { get; set; }

        public RegisterBindingModel()
        {

        }

        public RegisterBindingModel(String email,String username, String password, String confirmPassword)
        {
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.ConfirmPassword = confirmPassword;
        }
    }
}
