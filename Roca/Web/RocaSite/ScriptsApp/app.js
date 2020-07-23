angular.module('app', ['ui.router', 'ui.utils', 'ngAnimate','ngSanitize', 'myModule', 'ngGrid', 'smart-table', 'ui.bootstrap'])

    .config(['$httpProvider', function ($httpProvider) {
        $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';
    }])

    .config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
                $stateProvider
                    .state('home', {
                        url: '/',
                        templateUrl: 'User/Welcome',
                        controller: 'User.Welcome.Controller'
                    })
                    .state('login', {
                        url: '/User/Login',
                        templateUrl: 'User/Login',
                        controller: 'User.Login.Controller'
                    })
//                    .state('home', {
//                        url: '/',
//                        templateUrl: 'Autoplant/List',
//                        controller: 'Autoplant.List.Controller'
//                    })
                    .state('error', {
                        url: '/Error/:type',
                        templateUrl: function (stateParams) { return 'Error/' + stateParams.type; }
                    })
                    .state('itemList', {
                        abstract: true,
                        url: '/MaterialList/ItemList/:id/:mode',
                        templateUrl: function(stateParams) {
                            if (stateParams.mode == 'Edit') {
                                return 'MaterialList/EditItemList/' + stateParams.id;
                            }
                            return 'MaterialList/ItemList/' + stateParams.id;
                        },
                        controller: 'MaterialList.ItemList.Controller'
                    })
                    .state('itemList.childs', {
                        url: '',                  
                        views: {
                            header: {
                                templateUrl: 'MaterialList/Header',
                                controller: 'MaterialList.Header.Controller'
                            },
                            materials: {
                                templateUrl: 'Material/List',
                                controller: 'Material.ListForMl.Controller'
                            }
                        }                        
                    })
                    .state('projectSelection', {
                        url: '/Project/Selection?from',
                        templateUrl: 'Project/Selection',
                        controller:'Project.Selection.Controller'
                    })
                    .state('generic', {
                        url: '/:controller/:action',
                        templateUrl: function (stateParams) { return stateParams.controller + '/' + stateParams.action; },
                        controllerProvider: ['$stateParams', function($stateParams) {
                             return $stateParams.controller + '.' + $stateParams.action + '.' + 'Controller';
                        }]
                    })
                    .state('generic_id', {
                        url: '/:controller/:action/:id',
                        templateUrl: function (stateParams) { return stateParams.controller + '/' + stateParams.action + '/' + stateParams.id; },
                        controllerProvider: ['$stateParams', function ($stateParams) { return $stateParams.controller + '.' + $stateParams.action + '.' + 'Controller'; }]
                    })
                    .state('generic_controllerless', {
                        url: '/:controller/:action',
                        templateUrl: function (stateParams) { return stateParams.controller + '/' + stateParams.action; }
                    });


                $urlRouterProvider.otherwise('/');
            }
    ])

    .config(['$httpProvider', function($httpProvider) {
            $httpProvider.interceptors.push(['$q', '$injector','$location', function($q, $injector, $location) {
                return {
                    // optional method
                    'request': function (config) {
                        // do something on success
                        return config || $q.when(config);
                    },

                    // optional method
                    'requestError': function (rejection) {
                        // do something on error
                        //if (canRecover(rejection)) {
                        //    return responseOrNewPromise
                        //}
                        return $q.reject(rejection);
                    },

                    // optional method
                    'response': function (response) {
                        // do something on success
                        return response || $q.when(response);
                    },

                    // optional method
                    'responseError': function (rejection) {
                        // do something on error
                        var status = rejection.status;
                        if ((status == 403 || status == 500) && rejection.headers('roca-redirection')) {
                            var redirUrl = rejection.headers('roca-redirection');
                            var params = redirUrl.split('/');
                            var $state = $injector.get('$state');
                            if (redirUrl == 'Project/Selection') {
                                $state.go('projectSelection', { from: $location.path() });
                            } else if (params[0] == "Error" || redirUrl == "User/Unauthorized") {
                                //$state.go('error', { type: params[1] });
                                $state.go('generic_controllerless', { controller: params[0], action: params[1] });
                            } else {
                                $state.go('generic', { controller: params[0], action: params[1] });
                            }
                        }
                        return $q.reject(rejection);
                        
                    }
                };
            }]);
        }
    ])

    .filter('jsonDate', ['$filter', function ($filter) {
        return function (input, format) {
            if (angular.isUndefined(input) || input === null) return null;
            var dateInt = parseInt(input.substr(6));
            var out = $filter('date')(dateInt, format);
            return out;
        };
    }])

    .run(['$rootScope', '$location', function($rootScope, $location) {

            $rootScope.RootModel = {};
            $rootScope.RootModel.Title = "Odebrecht";

            $rootScope.$on("$locationChangeStart", function (event, next, current) {
                var x = 3;
                //debugger;
                //do your validations here
                //prevent the location change.
                //event.preventDefault();
            });

            $rootScope.$on("$locationChangeSuccess", function (event, next, current) {
                var x = 3;
                //debugger;
                //do your validations here
                //prevent the location change.
                //event.preventDefault();
            });
        }
    ])
    ;






