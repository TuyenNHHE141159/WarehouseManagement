using WarehouseManagement.Models;

namespace WarehouseManagement.Controllers
{

    public class DAO
    {
        
      
        public Account Login(string username, string password)
        {
            using (var context = new WarehouseContext())
            {
foreach (var account in context.Accounts)
            {
                if(username.Equals(account.Username.Trim())&& password.Equals(account.Pass.Trim()))
                {
                    return account;
                }
                    break;
            }
            }
            
            return null;
        } 
    }
}
