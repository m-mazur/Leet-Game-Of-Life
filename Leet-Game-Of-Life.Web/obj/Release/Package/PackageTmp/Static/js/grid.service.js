var GridService = function () {
    var gridPostUri = '../api/game/',
        gridGetUri = function (row, col) {
        return '../api/game?pRow=' + row + '&pCol=' + col;
    }

    this.get = function (row, col) {
        return ajaxHelper(gridGetUri(row, col), 'GET');
    }

    this.post = function (gridSnapshot) {
        return ajaxHelper(gridPostUri, 'POST', gridSnapshot);
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
}