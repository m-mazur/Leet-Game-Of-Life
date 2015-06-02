var ViewModel = function (gridService, gridHelpers) {
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
        self.grid(gridHelpers.groupGrid(data));
    }

    function getUpdatedGrid () {
        gridService.post(gridHelpers.unGroupGrid((self.grid()))).done(function (data) {
            populateGrid(data);

            self.generationCount(gridHelpers.incrementGenerationCount(generationCount));
            self.aliveCellCount(gridHelpers.countAliveCells(data));
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
       self.setGridSize(self.selectedRows(), value);
    });

    self.setGridSize = function (y, x) {
        getInitialGrid(y, x)
    };

    getInitialGrid(14,34);
};

ko.applyBindings(new ViewModel(GridService, GridHelpers));