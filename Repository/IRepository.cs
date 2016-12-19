using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerManager.Models;

namespace CustomerManager.Repository
{
    public interface IRepository
    {
        IList<Customer> GetCustomers(string inn = "");
        Customer GetCustomerById(string customerInn);
        IList<Order> GetCustomerOrders(string customerInn);
        IList<OrderSummary> GetCustomerSummary(string customerInn);
        OperationStatus InsertCustomer(Customer customer);
        OperationStatus UpdateCustomer(Customer customer);
        OperationStatus DeleteCustomer(string customerInn);
    }
}
