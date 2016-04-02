'use strict';

productApp.controller('ProductListController', ['$scope', '$filter', '$interval', '$timeout', '$location', 'uiGridConstants', 'ConfirmDialogService', 'ProductApiService', 'pageSizesConstant',
    
    function ($scope, $filter, $interval, $timeout, $location, uiGridConstants, ConfirmDialogService, ProductApiService, pageSizesConstant) {

        /// --------------------------------------------------------------------------
        /// Currently Selected Row
        /// --------------------------------------------------------------------------
        $scope.currentRow = {};

        /// --------------------------------------------------------------------------
        /// Routine declaration used for sorting multiple grid sort selctions
        /// --------------------------------------------------------------------------
        var orderBy = $filter('orderBy');

        /// --------------------------------------------------------------------------
        /// Toggle Flag for Update & Delete Button Enabled States.
        /// --------------------------------------------------------------------------
        $scope.canUpdate = function () {
            return $scope.currentRow !== {};
        };

        /// --------------------------------------------------------------------------
        /// Data Sent With GET Request
        /// --------------------------------------------------------------------------
        $scope.apiParams = {
            pageNo: 1,
            pageSize: 25,
            orderBy: null,
            filter: null
        };

        /// --------------------------------------------------------------------------
        /// Grid Column Definitions
        /// --------------------------------------------------------------------------
        $scope.columnDefs = [{ field: 'productName', enableHiding: false, width: "15%", resizable: false },
                            { field: 'supplier.companyName', enableHiding: false, displayName: 'Supplier', width: "15%", resizable: true },
                            { field: 'category.categoryName', enableHiding: false, displayName: 'Category', width: "10%", resizable: true },
                            { field: 'unitsInStock', enableHiding: false, width: "*", resizable: true },
                            { field: 'unitsOnOrder', enableHiding: false, width: "*", resizable: true },
                            { field: 'reorderLevel', enableHiding: false, width: "*", resizable: true },
                            { field: 'unitPrice', enableHiding: false, width: "*", resizable: true },
                            { field: 'quantityPerUnit', enableHiding: false, width: "10%", resizable: true },
                            { field: 'discontinued', enableHiding: false, resizable: true },
                            { field: 'productID', visible: false }
                            //,{ field: ' ', enableHiding: false, enableFiltering: false, enableSorting: false, cellTemplate: '<div><button ng-click="grid.appScope.delete(row)" class="btn btn-primary btn-sm pull-right">Delete</button><button ng-click="grid.appScope.edit(row)" class="btn btn-primary btn-sm pull-right">Edit</button></div>' }
        ];

        /// --------------------------------------------------------------------------
        /// Grid Configuration
        /// N.B. The row template allows us to add event handling for double clicks, 
        ///      which we want to use to bring up a form of the selected product.
        ///      Under 'appScopeProvider' you can see how this is handled.
        /// --------------------------------------------------------------------------
        $scope.gridOptions = {
            // Pagination
            paginationPageSizes: pageSizesConstant,
            paginationPageSize: 25,
            useExternalPagination: true,
            // Sorting
            enableSorting: true,
            useExternalSorting: true,
            // Filtering
            enableFiltering: true,
            useExternalFiltering: true,
            // Row Selections
            enableRowSelection: true,
            enableFullRowSelection: true,
            enableRowHeaderSelection: false,        
            multiSelect: false,
            noUnselect: true,
            // Columns
            columnDefs: $scope.columnDefs,
            enableColumnResize: true,

            onRegisterApi: function(gridApi){
                //set gridApi on scope
                $scope.gridApi = gridApi;
                // Row selection
                gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                    $scope.currentRow = row;
                });
                // Filtering Handler
                $scope.gridApi.core.on.filterChanged($scope, function(){
                    // Prevent running to the server for every keystroke
                    if (angular.isDefined($scope.filterTimeout)) {
                        $timeout.cancel($scope.filterTimeout);
                    }
                    $scope.filterTimeout = $timeout(function () {
                        $scope.filterChanged($scope.gridApi.grid.columns); // Pass in column definitions
                    }, 700);                
                });
                // Pagination
                gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                    $scope.apiParams.pageNo = newPage;
                    $scope.apiParams.pageSize = pageSize;
                    $scope.getProducts();
                });
                // Sorting
                $scope.gridApi.core.on.sortChanged($scope, $scope.sortChanged);
                $scope.sortChanged($scope.gridApi.grid);
            },
            appScopeProvider: {
                // Register dblclick event handler on 'rowTemplate'
                edit: function (row) {
                    $scope.currentRow = row;
                    $scope.edit();
                },
                delete: function (row) {
                    $scope.currentRow = row;
                    $scope.delete();
                }
            },
            rowTemplate: "<div ng-dblclick=\"grid.appScope.edit(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell ></div>"
        };

        /// --------------------------------------------------------------------------
        /// 'filterChanged' Event Handler
        /// --------------------------------------------------------------------------
        $scope.filterChanged = function (columns) {
            $scope.apiParams.filter = null  ;
            angular.forEach(columns, function (column, key) {
                var term = column.filters[0].term;
                if (angular.isDefined(term) && term !== null && term != '') {
                    var filter = column.name + ':' + column.filters[0].term;
                    $scope.apiParams.filter = ($scope.apiParams.filter == null) ? filter : $scope.apiParams.filter + ',' + filter;
                }
            });
            $scope.getProducts();
        };

        /// --------------------------------------------------------------------------
        /// 'sortChanged' Event Handler
        /// --------------------------------------------------------------------------
        $scope.sortChanged = function (grid) {
            // In the event the user chooses multiple sort options, we need to ensure that they are sent to the api service
            // in the order in which they were selected. To do this, we will push each selected column to a temporary array
            // and sort them based on the 'priority' value.    

            var sortOptions = [];               // Used to collect sort options
            $scope.apiParams.orderBy = null;    // Reset orderBy everytime

            // Collect columns choosen for sort
            angular.forEach(grid.columns, function (column, key) {
                if (angular.isDefined(column.sort.direction)) { //angular.isDefined(column.sort) && 
                    sortOptions.push({ name: column.name, priority: column.sort.priority, direction: column.sort.direction });
                }
            });

            // Sort
            sortOptions = orderBy(sortOptions, 'priority', false);

            // Build orderBy expression
            angular.forEach(sortOptions, function (sortOption, key) {
                var sort = sortOption.name + ':' + sortOption.direction;
                $scope.apiParams.orderBy = ($scope.apiParams.orderBy == null) ? sort : $scope.apiParams.orderBy + ',' + sort
            });

            //console.log(sortOptions);
            $scope.getProducts();
        };

        /// --------------------------------------------------------------------------
        /// Get All Products
        /// --------------------------------------------------------------------------
        $scope.getProducts = function () {
            $scope.currentRow = {}; // Reset currently selected row
            ProductApiService.getAll($scope.apiParams).then(
                function (response) {
                    $scope.gridOptions.data = response.data.items;                                                      // Assign data to grid
                    $scope.gridOptions.totalItems = response.data.totalRecordCount;                                     // Assign total count
                    $interval(function () { $scope.gridApi.selection.selectRow($scope.gridOptions.data[0]); }, 0, 1);   // Select first row
                },
                function (error) {
                    // Reset
                    $scope.gridOptions.data = [];
                    $scope.gridOptions.totalItems = 0;                    
                }
            );
        };

        /// --------------------------------------------------------------------------
        /// Delete Product
        /// --------------------------------------------------------------------------
        $scope.delete = function () {
            // Configure confirmation dialog
            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Product',
                headerText: 'Delete ?',
                bodyText: 'Are you sure you want to delete this product: ' + $scope.currentRow.entity.productName + ' ?'
            };
            // Open confirmation dialog & delete only if user confirms
            ConfirmDialogService.showModal({}, modalOptions).then(
                function (result) {
                    ProductApiService.delete($scope.currentRow.entity.productID).then(
                        function (response) {
                            $scope.getProducts();
                        }
                    );
                }
            );
        };

        /// --------------------------------------------------------------------------
        /// Set-up Editing Product
        /// --------------------------------------------------------------------------
        $scope.edit = function () {
            $location.path(ProductApiService.path + "/" + $scope.currentRow.entity.productID);  // Force routing to change
        };

        /// --------------------------------------------------------------------------
        /// Set-up New Product
        /// --------------------------------------------------------------------------
        $scope.new = function () {
            $location.path(ProductApiService.path + "/0");  // Force routing to change
        };
    }
])

.controller('ProductDetailController', ['$rootScope', '$scope', '$routeParams', 'ProductApiService', '$location',
    function ($rootScope, $scope, $routeParams, ProductApiService, $location) {

        /// --------------------------------------------------------------------------
        /// Product Model Used For Binding
        /// --------------------------------------------------------------------------
        $scope.product = {};

        /// --------------------------------------------------------------------------
        /// List Of Suppliers Used For Binding To <select>
        /// --------------------------------------------------------------------------
        $scope.suppliers = [];

        /// --------------------------------------------------------------------------
        /// List Of Categories Used For Binding To <select>
        /// --------------------------------------------------------------------------
        $scope.categories = [];

        /// --------------------------------------------------------------------------
        /// Get A Single Product
        /// --------------------------------------------------------------------------
        $scope.get = function () {
            var id = $routeParams.id;
            ProductApiService.get(id).then(
                function (response) {
                    $scope.product = response.data;
                    // Assign drop list data and clear them from vm
                    $scope.suppliers = response.data.suppliers;
                    $scope.categories = response.data.categories;
                    // Clear so we don't send with put or post
                    $scope.product.suppliers = [];
                    $scope.product.categories = [];
                    // Set Page Title
                    $rootScope.page.setTitle(response.data.productName);
                }
            );
        };

        /// --------------------------------------------------------------------------
        /// Save A Single Product
        /// --------------------------------------------------------------------------
        $scope.save = function (product) {
            var prod = angular.copy(product);
            ProductApiService.save(prod).then(
                function (response) {
                    $location.path(ProductApiService.path);
                }
            );
        };

        /// --------------------------------------------------------------------------
        /// Cancel Update
        /// --------------------------------------------------------------------------
        $scope.cancel = function () {
            $location.path("/products");
        };

        /// --------------------------------------------------------------------------
        /// Initialize
        /// --------------------------------------------------------------------------
        $scope.get();
    }
]);
