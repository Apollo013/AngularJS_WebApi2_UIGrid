'use strict';

productApp

.factory('ProductApiService', ['$http', '$q', 'baseUrl',

    function ($http, $q, baseUrl) {

        /// --------------------------------------------------------------------------
        /// Base path For Products
        /// --------------------------------------------------------------------------
        var path = '/products';

        /// --------------------------------------------------------------------------
        /// Base Url For Products
        /// --------------------------------------------------------------------------
        var url = baseUrl + path;

        /// --------------------------------------------------------------------------
        /// Get All Products
        /// --------------------------------------------------------------------------
        var _getAll = function (apiParams) {
            var req = {
                method: 'GET',
                url: url,
                params: apiParams
            }

            return $http(req).then(
                function (response) {
                    return response;
                }
            );
        };

        /// --------------------------------------------------------------------------
        /// Get A Single Product
        /// --------------------------------------------------------------------------
        var _get = function (id) {
            var req = {
                method: 'GET',
                url: url + '/' + id
            }

            return $http(req).then(
                function (response) {
                    return response;
                }
            );
        };

        /// --------------------------------------------------------------------------
        /// Delete A Single Product
        /// --------------------------------------------------------------------------
        var _delete = function (id) {
            var req = {
                method: 'DELETE',
                url: url + '/' + id
            }

            return $http(req).then(
                function (response) {
                    return response;
                }
            );
        };

        /// --------------------------------------------------------------------------
        /// Save A Single Product
        /// --------------------------------------------------------------------------
        var _save = function (product) {
            if (product.productID == 0) {
                return _insert(product);
            }
            else {
                return _update(product);
            }
        };

        /// --------------------------------------------------------------------------
        /// Insert A Single Product
        /// --------------------------------------------------------------------------
        var _insert = function (product) {
            var req = {
                method: 'POST',
                url: url + '/' + product.productID,
                data: product
            }

            return $http(req).then(
                function (response) {
                    return response;
                }
            );
        };

        /// --------------------------------------------------------------------------
        /// Update A Single Product
        /// --------------------------------------------------------------------------
        var _update = function (product) {
            var req = {
                method: 'PUT',
                url: url ,//+ '/' + product.productID,
                data: product
            }

            return $http(req).then(
                function (response) {
                    return response;
                }
            );
        }

        /// --------------------------------------------------------------------------
        /// Define & Return Service
        /// --------------------------------------------------------------------------
        var service = {};
        service.getAll = _getAll;
        service.get = _get;
        service.delete = _delete;
        service.save = _save;
        service.path = path;
        return service;
    }
]);
