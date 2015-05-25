var ViewModel = function (gridService) {
    var self = this;

    self.grid = ko.observableArray([]);

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

    function populateGrid (data) {
        self.grid.removeAll();
        self.grid(groupGrid(data));
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
        self.update = setInterval(function () {
            gridService.postAndGetUpdateGrid(unGroupGrid((self.grid()))).done(function (data) {
                populateGrid(data);
            });
        }, 100);
    };

    self.pausGame = function () {
        clearInterval(self.startGame.update());
    }

    gridService.getInitialGrid().done(function (data) {
        populateGrid(data);
    });
};

ko.applyBindings(new ViewModel(new GridService()));