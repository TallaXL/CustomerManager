using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomerManager.Repository;
using CustomerManager.Models;

namespace CustomerManager.Repository
{
    public class MemoryRepository : IRepository
    {
        private static Dictionary<string, Customer> _customers = new Dictionary<string, Customer>()
        {
            {"123456789012", new Customer { Inn = "123456789012",  FullName="ИП Альфа"} },
            {"12345678", new Customer { Inn = "12345678",  FullName="АО Бета"} },
            {"682356364224", new Customer { Inn = "682356364224",  FullName="АО Вектор"} },
            {"369595644848", new Customer { Inn = "369595644848",  FullName="АО Импульс"} }
        };
        private static Dictionary<string, Order> _orders = new Dictionary<string, Order>()
        {
            {"П0001", new Order {Id = "П0001", Currency = 810, Sum = 40000, CustomerInn = "123456789012" }},
            {"П0002", new Order {Id = "П0002", Currency = 840, Sum = 1200, CustomerInn = "123456789012" }},
            {"П0003", new Order {Id = "П0003", Currency = 810, Sum = 1000, CustomerInn = "123456789012" }},
            {"i1-1", new Order {Id = "i1-1", Currency = 810, Sum = 23000, CustomerInn = "12345678" }},
            {"i1-2", new Order {Id = "i1-2", Currency = 840, Sum = 890, CustomerInn = "12345678" }}
        };
        public IList<Customer> GetCustomers(string inn = "")
        {
            if (String.IsNullOrEmpty(inn))
            {
                return _customers.Values.ToList();
            }
            else
            {
                return _customers.Values.Where(c => c.Inn.StartsWith(inn)).ToList();
            }
            
        }
        public Customer GetCustomerById(string customerInn)
        {
            return _customers[customerInn];
        }
        public IList<Order> GetCustomerOrders(string customerInn)
        {
            return _orders.Values.Where(c => String.Compare(c.CustomerInn, customerInn) == 0).ToList();
        }
        public IList<OrderSummary> GetCustomerSummary(string customerInn)
        {
            var orders = _orders.Values
                .Where(o => String.Compare(o.CustomerInn, customerInn) == 0)
                .GroupBy(o => o.Currency)
                .Select(g => new OrderSummary
                {
                    Currency = g.First().Currency,
                    Count = g.Count(),
                    Sum = g.Sum(c => c.Sum)
                });

            return orders.ToList();
        }
        public OperationStatus InsertCustomer(Customer customer)
        {
            var opStatus = new OperationStatus { Code = 1 };

            if (_customers.ContainsKey(customer.Inn))
            {
                opStatus.Code = -2;
                opStatus.Message = "Customer already exists";

                return opStatus;
            }
            
            try
            {
                _customers.Add(customer.Inn, customer);
            }
            catch (Exception exp)
            {
                opStatus.Code = -1;
                opStatus.Message = exp.Message;
            }
            return opStatus;
        }
        public OperationStatus DeleteCustomer(string customerInn)
        {
            var opStatus = new OperationStatus { Code = 1 };

            Customer cust;

            if (_customers.TryGetValue(customerInn, out cust))
            {
                try
                {
                    _customers.Remove(cust.Inn);
                    foreach (var k in _orders.Where(k => k.Value.CustomerInn == customerInn).Select(p => p.Key).ToList())
                        _orders.Remove(k);
                }
                catch (Exception exp)
                {
                    opStatus.Code = -1;
                    opStatus.Message = exp.Message;
                }
            } else
            {
                opStatus.Code = -2;
                opStatus.Message = "Customer doesn't exist";
            }
            return opStatus;
        }
        public OperationStatus UpdateCustomer(Customer customer)
        {
            var opStatus = new OperationStatus { Code = 1 };

            Customer cust;

            if (_customers.TryGetValue(customer.Inn, out cust))
            {
                try
                {
                    cust.FullName = customer.FullName;
                }
                catch (Exception exp)
                {
                    opStatus.Code = -1;
                    opStatus.Message = exp.Message;
                }
            }
            else
            {
                opStatus.Code = -2;
                opStatus.Message = "Customer doesn't exist";
            }
            return opStatus;
        }
    }
}