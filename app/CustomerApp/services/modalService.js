angular.module('customersApp')
    .service('modalService', ['$uibModal', function ($uibModal) {

  var modalOptions = {
    closeButtonText: 'Отмена',
    actionButtonText: 'OK',
    headerText: 'Подтверждение',
    bodyText: 'Вы действительно хотите выполнить действие?'
  };

  this.showModal = function (customModalOptions) {

    var tempModalOptions = {};

    angular.extend(tempModalOptions, modalOptions, customModalOptions);

    return $uibModal.open({
        templateUrl: 'app/CustomerApp/partials/modal.html',
        backdrop: 'static',
        controller: function ($scope, $uibModalInstance) {

          $scope.modalOptions = tempModalOptions;

          $scope.modalOptions.ok = function (result) {
            $uibModalInstance.close(result);
          };

          $scope.modalOptions.cancel = function (result) {
            $uibModalInstance.dismiss('cancel');
          };
        }
     }).result;
  };
}]);
