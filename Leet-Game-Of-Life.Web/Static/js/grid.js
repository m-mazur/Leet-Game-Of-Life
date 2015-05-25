var ViewModel = function (gridService) {
    var self = this;

    self.grid = ko.observableArray([]);

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

    function groupGrid(data) {
        var groupedGrid = _.groupBy(data, function (data) {
            return data.Y;
        });

        var mappedGrid = _.chain(groupedGrid).map(function (grid) {
            return grid;
        }).value();

        return mappedGrid;
    }

    function unGroupGrid(data) {
        var ungroupedListOfCells = [];

        data.forEach(function (item) {
            item.forEach(function (object) {
                ungroupedListOfCells.push(object);
            });
        });

        return ungroupedListOfCells;
    }

    self.changeCellState = function (cell) {
        if (!cell.IsDead) {
            cell.IsDead = true;
        } else {
            cell.IsDead = false;
        }

        updateGrid(ko.toJSON(self.grid));
    };

    self.test = function () {
        setInterval(function () {
            gridService.postAndGetUpdateGrid(unGroupGrid((self.grid()))).done(function (data) {
                self.grid.removeAll();
                self.grid(groupGrid(data));
            });
        }, 100);
    };

    gridService.getInitialGrid().done(function (data) {
        self.grid(unGroupGrid(data));
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

ko.applyBindings(new ViewModel(new GridService()));