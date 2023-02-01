using EcommerceAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user, IList<string> roles);
    }
}
