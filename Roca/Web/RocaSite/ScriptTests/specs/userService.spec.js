///<reference path="../references.js"/>


describe("UserService Tests", function () {
    var userService;
    var httpBackend;
    var helper = TestHelper();
    var $q;
    var $rootScope;

    //para poder debuggear con resharper haciendo reload de la pagina
    //jasmine.getEnv().currentRunner_.finishCallback = function () { };

    beforeEach(function () {
        angular.mock.module('app');

        angular.mock.inject(function (_UserService_, _$httpBackend_, _$q_, _$rootScope_) {
            userService = _UserService_;
            httpBackend = _$httpBackend_;
            $q = _$q_;
            $rootScope = _$rootScope_;
        });
    });


    it("promise test", function(done) {
        getAsync().then(function (data) {
            done();  //retarda la terminacion del test hasta que se ejecuta done();
        });
        apply();  //sin apply no dispara nunca el then de la promise
    });


    function getAsync() {
        var promise = $q.when('holis');
        return promise;
    }

    function apply() {
        if (!$rootScope.$$phase) {
            $rootScope.$apply();
        } 
    }

    it("should return all users", function () {

        httpBackend.expect("GET", "User/GetAllUsers").respond(helper.getUsers());
        var users;
        userService.getAllUsers().then(function(data) {
            users = data;
            //console.log("primer get");
        });
        httpBackend.flush();

        httpBackend.verifyNoOutstandingExpectation();
        expect(users).toBeDefined();
        expect(users.length).toBe(helper.getUsers().length);

        //console.log(angular.mock.dump(users));
    });

    it("should cache users", function () {

        httpBackend.expect("GET", "User/GetAllUsers").respond(helper.getUsers());
        var users;
        userService.getAllUsers().then(function (data) {
            users = data;
            //console.log("primer get");
        });
        httpBackend.flush();

        httpBackend.verifyNoOutstandingExpectation();
        expect(users).toBeDefined();
        expect(users.length).toBe(helper.getUsers().length);


        httpBackend.expect("GET", "User/GetAllUsers").respond(helper.getUsers());
        userService.getAllUsers().then(function (data) {
            users = data;
            //console.log("segundo get");
        });

        expect(httpBackend.flush).toThrow();
        expect(httpBackend.verifyNoOutstandingExpectation).toThrow();


        //console.log(angular.mock.dump(users));
    });



});