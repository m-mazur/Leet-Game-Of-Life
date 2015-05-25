var GridService = function () {
    var gridUri = '../api/game/';

    this.getInitialGrid = function () {
        return ajaxHelper(gridUri, 'GET');
    }

    function ajaxHelper(uri, method, data) {
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        });
    };
};