var ViewModel = function (gridService, gridHelpers) {
    var self = this,
        update,
        generationCount = 0,
        aliveCellCount = 0,
        media = window.matchMedia( "(max-width: 800px)" );

    self.grid = ko.observableArray([]);
    self.generationCount = ko.observable(generationCount);
    self.aliveCellCount = ko.observable(aliveCellCount);
    self.rows = ko.observableArray();
    self.columns = ko.observableArray();
    self.selectedRows = ko.observable(15);
    self.selectedColumns = ko.observable(30);

    function updateGrid(data) {
        self.grid(gridHelpers.parseGridFromJson(data));
    }

    function populateGrid(data) {
        self.grid(gridHelpers.groupGrid(data));
    }

    function getUpdatedGrid() {
        gridService.post(gridHelpers.unGroupGrid((self.grid()))).done(function (data) {
            populateGrid(gridHelpers.processGrid(data));
            self.generationCount(gridHelpers.incrementGenerationCount(self.generationCount()));
            self.aliveCellCount(gridHelpers.countAliveCells(data));
        });
    }

    function getInitialGrid(row, col) {
        gridService.get(row, col).done(function (data) {
            populateGrid(data);
        });
    }

    self.changeCellState = function (cell) {
        cell.IsDead = !cell.IsDead ? true : false;
        updateGrid(gridHelpers.parseGridToJson(self.grid()));
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
        getInitialGrid(self.selectedRows(), self.selectedColumns());
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

    if (media.matches) {
        getInitialGrid(10, 9);
        self.selectedRows(10);
        self.selectedColumns(9);
    } else {
        getInitialGrid(15, 30);
    }
};

ko.applyBindings(new ViewModel(GridService, GridHelpers));