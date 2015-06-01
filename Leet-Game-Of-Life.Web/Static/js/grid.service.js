var GridService = function () {
    var gridPostUri = '../api/game/';
    
    function gridGetUri(row, col) {
        return '../api/game?pRow=' + row + '&pCol=' + col;
    }

    this.getInitialGrid = function (row, col) {
        return ajaxHelper(gridGetUri(row,col), 'GET');
    }

    this.postAndGetUpdateGrid = function (gridSnapshot) {
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