using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CURDWinformDI.Services
{
    public interface ICustomerService
    {
        DataTable GetAllCustomers();
        void AddOrUpdateCustomer(string id, string name, string email, string phoneNumber);
        void DeleteCustomer(string id);
    }
}
