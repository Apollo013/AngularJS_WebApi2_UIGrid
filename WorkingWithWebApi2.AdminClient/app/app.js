'use strict';

var app = angular.module('app', ['ngRoute', 'angular-loading-bar', 'toaster', 'ngAnimate', 'ui.bootstrap', 'productApp', 'categoryApp']);
var productApp = angular.module('productApp', ['ngTouch', 'ngResource', 'ui.grid', 'ui.grid.selection', 'ui.grid.autoResize', 'ui.grid.pagination']);
var categoryApp = angular.module('categoryApp', ['ngResource', 'ui.grid']);


