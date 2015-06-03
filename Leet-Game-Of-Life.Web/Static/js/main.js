require.config({
    baseUrl: '../',
    paths: {
        require: '../bower_components/requirejs/require',
        jquery: '../bower_components/jquery/dist/jquery',
        knockout: '../bower_components/knockout/dist/knockout',
        underscore: '../bower_components/underscore/underscore',
        viewmodel: 'grid.viewmodel'
    }
});

require(['viewmodel', 'knockout'], function (vm, ko) {
    ko.applyBindings(vm);
});