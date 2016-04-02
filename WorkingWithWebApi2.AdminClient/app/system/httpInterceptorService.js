'use strict';

app.factory('HttpInterceptorService', ['$q', '$injector', 'toaster',

    function ($q, $injector, toaster) {

        /// --------------------------------------------------------------------------
        /// Request Configuration Handler for all http calls
        /// --------------------------------------------------------------------------
        var _request = function (config) {
            config.headers = config.headers || {};
            config.headers['Content-Type'] = 'application/json';
            return config;
        };

        /// --------------------------------------------------------------------------
        /// Response Error Handler
        /// --------------------------------------------------------------------------
        var _responseError = function (rejection) {

            // Errors will be displayed using toaster elements
            if (rejection.status == 400) {
                toaster.pop("warning", rejection.statusText, rejection.data);
            }
            else if (rejection.status == 401) {
                toaster.pop("info", rejection.statusText, "For this exercise, the server is rejecting any non GET requests");
            }
            else if (rejection.status == 404) {
                toaster.pop("info", rejection.statusText, rejection.data);
            }
            else if (rejection.status == 500) {
                toaster.pop("error", rejection.statusText, rejection.data);
            }
            else {
                toaster.pop("info", rejection.statusText, rejection.data);
            }
            return $q.reject(rejection);
        };

        /// --------------------------------------------------------------------------
        /// Define & Return Service Object
        /// --------------------------------------------------------------------------
        var httpService = {};
        httpService.request = _request;
        httpService.responseError = _responseError;
        return httpService;
    }
]);
