'use strict';

app

.config(['$routeProvider', '$locationProvider',

    /// --------------------------------------------------------------------------
    /// Configure Routing
    /// --------------------------------------------------------------------------
    function ($routeProvider, $locationProvider) {

        $routeProvider
        .when('/products', {
            title: 'Products',
            templateUrl: 'views/products/product-list.html',
            controller: 'ProductListController'
        })
        .when('/products/:id', {
            templateUrl: 'views/products/product-detail.html',
            controller: 'ProductDetailController'
        })
        .otherwise({
            redirectTo: '/products'
        });

        // Enable html5Mode for pushstate ('#'-less URLs)
        $locationProvider.html5Mode(true);
        $locationProvider.hashPrefix('!')
    }
   
])

.config(['$httpProvider', function ($httpProvider) {
    /// --------------------------------------------------------------------------
    /// Inject Incerceptor Service for http requests & responses
    /// --------------------------------------------------------------------------
    $httpProvider.interceptors.push('HttpInterceptorService');
}])

.run(['$rootScope', function ($rootScope) {    
    /// --------------------------------------------------------------------------
    /// Allows us to change the page title dynamically from any controller
    /// --------------------------------------------------------------------------
    $rootScope.page = {
        setTitle: function (title) {
            this.title = 'UI Grid Demo | ' + title;
        }
    }

    /// --------------------------------------------------------------------------
    /// Changes the page title dynamically based on the 'title' $routeProvider 
    /// attribute if specified in the route config code block above.
    /// --------------------------------------------------------------------------
    $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
        $rootScope.page.setTitle(current.$$route.title || 'Products');
    });
}]);