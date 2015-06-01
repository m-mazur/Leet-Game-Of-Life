var ViewModel = function (gridService) {
    var self = this,
        update,
        generationCount = 0,
        aliveCellCount = 0,
        sizeOption = function () {
            var sizes = [];

            for (var i = 0; i < 30; i++) {
                sizes.push(i + 1);
            }

            return sizes;
        }

    self.grid = ko.observableArray([]);
    self.generationCount = ko.observable(generationCount);
    self.aliveCellCount = ko.observable(aliveCellCount);
    self.rows = ko.observableArray(sizeOption());
    self.columns = ko.observableArray(sizeOption());
    self.selectedRows = ko.observable(15);
    self.selectedColumns = ko.observable(30);

    function updateGrid(data) {
        self.grid(JSON.parse(data));
    }

    function populateGrid(data) {
        self.grid(groupGrid(data));
    }

    function groupGrid(data) {
        var groupedGrid = _.groupBy(data, function (data) {
            return data.Y;
        });

        return _.chain(groupedGrid).map(function (grid) {
            return grid;
        }).value();
    }

    function unGroupGrid(grid) {
        var ungroupedListOfCells = [];

        grid.forEach(function (row) {
            row.forEach(function (cell) {
                if (!cell.IsDead || (cell === grid[grid.length - 1][row.length - 1])) {
                    ungroupedListOfCells.push(cell);
                }
            });
        });

        return ungroupedListOfCells;
    }

    function countAliveCells(grid) {
        aliveCellCount = 0;
        
        grid.forEach(function (cell) {
            if (!cell.IsDead) {
               aliveCellCount++;
           }
        });

        return aliveCellCount;
    }

    function incrementGenerationCount () {
        generationCount++;
        self.generationCount(generationCount);
    }

    function getUpdatedGrid () {
        gridService.post(unGroupGrid((self.grid()))).done(function (data) {
            populateGrid(data);
            incrementGenerationCount();

            self.aliveCellCount(countAliveCells(data));
        });
    }

    function getInitialGrid (row, col) {
        gridService.get(row, col).done(function (data) {
            populateGrid(data);
        });
    }

    self.changeCellState = function (cell) {
        if (!cell.IsDead) {
            cell.IsDead = true;
        } else {
            cell.IsDead = false;
        }
        
        updateGrid(ko.toJSON(self.grid));
    };

    self.startGame = function () {
        update = setInterval(getUpdatedGrid, 200);
    };

    self.pausGame = function () {
        clearInterval(update);
    };

    self.resetGame = function () {
        self.generationCount(0);
        self.aliveCellCount(0);
        getInitialGrid(14, 34);
    };

    self.selectedRows.subscribe(function (value) {
        self.setGridSize(value, self.selectedColumns())
    });

    self.selectedColumns.subscribe(function (value) {
       self.setGridSize(self.selectedRows, value);
    });

    self.setGridSize = function (y, x) {
        getInitialGrid(y, x)
    };

    getInitialGrid(14,34);
};

ko.applyBindings(new ViewModel(new GridService()));