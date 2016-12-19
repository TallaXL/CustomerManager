angular.module('customersApp')
    .service('customerService', ['$http', function ($http) {
        var urlBase = '/api/customers/';

        this.getCustomers = function () {
            return $http.get(urlBase);
        };

        this.searchCustomers = function (inn) {
            return $http.get(urlBase + 'search/' + inn);
        };

        this.getCustomerOrders = function (id) {
            return $http.get(urlBase + id + '/orders');
        };

        this.getCustomer = function (id) {
            return $http.get(urlBase + id);
        };

        this.getCustomerSummary = function (id) {
            return $http.get(urlBase + id + '/summary');
        };

        this.insertCustomer = function (customer) {
            return $http.post(urlBase + 'postCustomer', customer);
        };

        this.updateCustomer = function (customer) {
            return $http.put(urlBase + 'putCustomer/' + customer.Inn, customer);
        };

        this.deleteCustomer = function (id) {
            return $http.delete(urlBase + 'deleteCustomer/' + id);
        };
    }]);