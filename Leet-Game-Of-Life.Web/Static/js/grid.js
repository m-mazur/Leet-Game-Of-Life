var ViewModel = function (gridService) {
    var self = this,
        update,
        generationCount;

    self.grid = ko.observableArray([]);
    self.generationCount = ko.observable();

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

        var mappedGrid = _.chain(groupedGrid).map(function (grid) {
            return grid;
        }).value();

        return mappedGrid;
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

    function getUpdatedGrid () {
        generationCount = 0;

        gridService.postAndGetUpdateGrid(unGroupGrid((self.grid()))).done(function (data) {
            generationCount++;
            self.generationCount(generationCount);
            populateGrid(data);
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
        update = setInterval(getUpdatedGrid, 150);
    };

    self.pausGame = function () {
        clearInterval(update);
    };

    self.resetGame = function () {
        getInitialGrid();
    };

    getInitialGrid();
};

ko.applyBindings(new ViewModel(new GridService()));