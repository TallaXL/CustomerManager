using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CustomerManager.Repository;
using CustomerManager.Models;

namespace CustomerManager.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomerController : ApiController
    {
        private readonly IRepository _repository;

        public CustomerController(IRepository repo)
        {
            _repository = repo;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetCustomers()
        {
            var customers = _repository.GetCustomers();
            return Request.CreateResponse(HttpStatusCode.OK, customers);
        }

        [HttpGet]
        [Route("search/{inn}")]
        public HttpResponseMessage GetCustomers(string inn)
        {
            var customers = _repository.GetCustomers(inn);
            return Request.CreateResponse(HttpStatusCode.OK, customers);
        }

        [HttpGet]
        [Route("~/api/customer/{inn}")]
        public HttpResponseMessage GetCustomer(string inn)
        {
            var customer = _repository.GetCustomerById(inn);
            return Request.CreateResponse(HttpStatusCode.OK, customer);
        }

        [HttpGet]
        [Route("{inn}/orders")]
        public HttpResponseMessage GetCustomerOrders(string inn)
        {
            var orders = _repository.GetCustomerOrders(inn);
            return Request.CreateResponse(HttpStatusCode.OK, orders);
        }

        [HttpGet]
        [Route("{inn}/summary")]
        public HttpResponseMessage GetCustomerSummary(string inn)
        {
            var orders = _repository.GetCustomerSummary(inn);
            return Request.CreateResponse(HttpStatusCode.OK, orders);
        }

        [HttpPut]
        [Route("putCustomer/{inn}")]
        public HttpResponseMessage PutCustomer(string inn, [FromBody]Customer customer)
        {
            var opStatus = _repository.UpdateCustomer(customer);
            if (opStatus.Code == 1)
            {
                return Request.CreateResponse<Customer>(HttpStatusCode.Accepted, customer);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.Message);
        }

        [HttpPost]
        [Route("postCustomer")]
        public HttpResponseMessage PostCustomer([FromBody]Customer customer)
        {
            var opStatus = _repository.InsertCustomer(customer);
            if (opStatus.Code == 1)
            {
                return Request.CreateResponse<Customer>(HttpStatusCode.Accepted, customer);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.Message);
        }

        [HttpDelete]
        [Route("deleteCustomer/{inn}")]
        public HttpResponseMessage DeleteCustomer(string inn)
        {
            var opStatus = _repository.DeleteCustomer(inn);
            if (opStatus.Code == 1)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.Message);
            }
        }
    }
}
