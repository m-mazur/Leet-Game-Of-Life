/*var GridService = (function () {
    var gridPostUri = '../api/game/',
        gridGetUri = function (row, col) {
        return '../api/game?row=' + row + '&column=' + col;
    };

    function ajaxHelper(uri, method, data) {
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        });
    }

    function get (row, col) {
        return ajaxHelper(gridGetUri(row, col), 'GET');
    }

    function post (gridSnapshot) {
        return ajaxHelper(gridPostUri, 'POST', gridSnapshot);
    }

    return {
        get: get,
        post: post
    }
})();*/

define(['jquery', 'underscore'], function ($, _) {
    var gridPostUri = '../api/game/',
        gridGetUri = function (row, col) {
            return '../api/game?row=' + row + '&column=' + col;
        };

    function ajaxHelper(uri, method, data) {
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        });
    }

    function get (row, col) {
        return ajaxHelper(gridGetUri(row, col), 'GET');
    }

    function post (gridSnapshot) {
        return ajaxHelper(gridPostUri, 'POST', gridSnapshot);
    }

    return {
        get: get,
        post: post
    }
});