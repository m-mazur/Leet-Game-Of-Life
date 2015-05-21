var ViewModel = function () {
    var self = this;

    self.grid = ko.observableArray(createGrid(5, 5));

    function createGrid(rows, columns) {
        var columnList = [null],
            cellList = [null];

        for (var row = 0; row < rows; row++) {

            for (var column = 0; column < columns; column++) {
                columnList[column] = {
                    X: row,
                    Y: column,
                    IsDead: true,
                    cellName: (row + 1) + ", " + (column + 1)
                };
            }

            cellList[row] = columnList;
            columnList = [null];
        }

        return cellList;
    }

    function updateGrid(gridAsJson) {
        self.grid.removeAll();
        self.grid(JSON.parse(gridAsJson));
    }

    function groupAndSortGrid(data) {
        var groupedGrid = _.groupBy(data, function (data) {
            return data.Y;
        });

        var mappedGrid = _.chain(groupedGrid).map(function (grid) {
            return grid;
        }).value();

        return mappedGrid;
    }

    self.changeCellState = function (cell) {
        if (!cell.IsDead) {
            cell.IsDead = true;
        } else {
            cell.IsDead = false;
        }

        console.log(ko.toJSON(self.grid));
        updateGrid(ko.toJSON(self.grid));
        ajaxHelper("../api/game/", 'POST', ko.toJSON(self.grid));
    };

    ajaxHelper("../api/game/", 'GET').done(function (data) {
        self.grid(groupAndSortGrid(data));
    });

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

ko.applyBindings(new ViewModel());