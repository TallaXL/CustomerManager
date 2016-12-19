angular.module('customersApp')
    .controller('customerEditController', ['customerService', 'modalService', '$uibModal', function (customerService, modalService, $uibModal) {
        var self = this;

        self.customers = [];

        customerService.getCustomers()
          .success(function (data) {
              self.customers = data;
          })
          .error(function (data, status) {
              self.customers = [];
          });

        self.addCustomer = function () {
            var newCustomer = { Inn: "", FullName: "" };

            var modalAddCustomer = $uibModal.open({
                templateUrl: 'app/CustomerApp/partials/modalCustomer.html',
                controller: 'ModalCustomerController',
                size: '',
                resolve: {
                    customer: function () {
                        return newCustomer;
                    }
                }
            });

            modalAddCustomer.result.then(function (customer) {
                customerService.insertCustomer(customer).then(function (data) {
                    self.customers.push(customer);
                });
            }, function () { });
        }

        self.editCustomer = function (customer) {
            var currCustomer = customer;

            var modalEditCustomer = $uibModal.open({
                templateUrl: 'app/CustomerApp/partials/modalCustomer.html',
                controller: 'ModalCustomerController',
                size: '',
                resolve: {
                    customer: function () {
                        return angular.copy(customer);
                    }
                }
            });

            modalEditCustomer.result.then(function (customer) {
                customerService.updateCustomer(customer).then(function (data) {
                    currCustomer.Inn = customer.Inn;
                    currCustomer.FullName = customer.FullName;
                });
            }, function () { });
        }

        self.removeCustomer = function (id) {
            modalService.showModal({}).then(function (result) {
                customerService.deleteCustomer(id)
                  .success(function (data) {
                      var index = -1;
                      var customersArray = eval(self.customers);
                      for (var i = 0; i < customersArray.length; i++) {
                          if (customersArray[i].Inn === id) {
                              index = i;
                              break;
                          }
                      }
                      if (index > -1) {
                          self.customers.splice(index, 1);
                      }
                  })
                  .error(function (data, status) {
                      return null;
                  });
            }, {});
        }
    }])

  .controller('ModalCustomerController', ["$scope", "$uibModalInstance", "customer", function ($scope, $uibModalInstance, customer) {

      $scope.customer = customer;

      $scope.ok = function () {
          $uibModalInstance.close($scope.customer);
      };

      $scope.cancel = function () {
          $uibModalInstance.dismiss('cancel');
      };

  }]);