using CIT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CIT.Common;

namespace CIT.DAL
{
    public class CITDatabaseInitializer : DropCreateDatabaseAlways<CITDataContext>
    {
        protected override void Seed(CITDataContext context)
        {
            var users = new List<UserInfo> 
            { 
                new UserInfo{ UserId="Administrator", FirstName="Administrator", LastName="Admin", Email="admin@cit.com", Password="P@ssw0rd1".ToCryptoStringAES(CITConstants.CRYPTO_AES_KEY.GetStringFromByteArray(), CITConstants.CRYPTI_AES_IV.GetStringFromByteArray()) }
            };
            context.Users.AddRange(users);
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
