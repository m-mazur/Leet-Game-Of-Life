var ViewModel = function (gridService) {
    var self = this,
        update,
        generationCount = 0,
        aliveCellCount = 0;

    self.grid = ko.observableArray([]);
    self.generationCount = ko.observable(generationCount);
    self.aliveCellCount = ko.observable(aliveCellCount);

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

    function unGroupGrid(data) {
        var ungroupedListOfCells = [];

        data.forEach(function (item) {
            item.forEach(function (object) {
                if (!object.IsDead) {
                    ungroupedListOfCells.push(object);
                }
            });
        });

        return ungroupedListOfCells;
    }

    function countAliveCells(data) {
        aliveCellCount = 0;
        
        data.forEach(function (item) {
           if (!item.IsDead) {
               aliveCellCount++;
           }
        });

        return aliveCellCount;
    }

    function getUpdatedGrid () {
        gridService.postAndGetUpdateGrid(unGroupGrid((self.grid()))).done(function (data) {
            generationCount++;
            populateGrid(data);
            self.aliveCellCount(countAliveCells(data));
            self.generationCount(generationCount);
        });
    }

    function getInitialGrid () {
        gridService.getInitialGrid().done(function (data) {
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
        getInitialGrid();
    };

    getInitialGrid();
};

ko.applyBindings(new ViewModel(new GridService()));