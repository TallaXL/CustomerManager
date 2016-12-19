angular.module('customersApp')
    .controller('customerController', ['customerService', '$timeout', '$q', '$log', customerController]);

function customerController(customerService, modalService, $timeout, $q, $log, $uibModal) {
    var self = this;

    self.simulateQuery = false;
    self.isDisabled = false;
    self.repos = [];
    self.summary = [];
    self.noSummaryFound = true;

    //self.repos = loadAll();
    //customerService.getCustomers()
    //  .success(function (data) {
    //      self.repos = data;
    //  })
    //  .error(function (data, status) {
    //      self.repos = [];
    //  });

    self.querySearch = querySearch;
    self.selectedItemChange = selectedItemChange;
    self.searchTextChange = searchTextChange;

    self.searchCustomers = function (searchText) {
        $log.info(searchForm.autocompleteField);
    }

    function querySearch(query) {
        return customerService.searchCustomers(query)
          .then(function (results) {
              self.repos = results.data;
              return results.data;
          });
    }

    function searchTextChange(text) {
        $log.info('Text changed to ' + text);
    }

    function selectedItemChange(item) {
        //$log.info('Item changed to ' + JSON.stringify(item));
        customerService.getCustomerSummary(item.Inn)
            .success(function (data) {
                self.summary = data;
                self.noSummaryFound = (self.summary.length === 0);
            })
            .error(function (data) {
                self.summary = [];
                self.noSummaryFound = true;
            });
    }

    function loadAll() {
        var repos = [
          {
              'FullName': 'ИП Альфа',
              'Inn': '123456789012'
          },
          {
              'FullName': 'АО Бета',
              'Inn': '12345678'
          },
          {
              'FullName': 'АО Вектор',
              'Inn': '682356364224'
          },
          {
              'FullName': 'АО Импульс',
              'Inn': '369595644848'
          }
        ];

              //return repos.map(function (repo) {
              //repo.value = repo.FullName.toLowerCase();
        return repos;
    }

    function createFilterFor(query) {
        var lowercaseQuery = angular.lowercase(query);

        return function filterFn(item) {
            return (item.Inn.indexOf(lowercaseQuery) === 0);
        };

    }
}