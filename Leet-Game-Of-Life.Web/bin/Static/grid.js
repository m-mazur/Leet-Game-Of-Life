var ViewModel = function () {
    var self = this;

    self.grid = ko.observableArray(createGrid(14, 22));
    //self.jsonFromView = ko.observable();

    function createGrid (rows, columns) {
        var columnList = [null],
            cellList = [null];

        for(var row = 0; row < rows; row++) {

            for (var column = 0; column < columns; column++) {
                columnList[column] = {
                    x: row,
                    y: column,
                    isDead: true,
                    cellName: (row + 1) + ", " + (column + 1)
                };
            }

            cellList[row] = columnList;
            columnList = [null];
         }

        return cellList;
    }

    function updateGrid (gridAsJson) {
        self.grid.removeAll();
        self.grid(JSON.parse(gridAsJson));

        console.log(gridAsJson);
        //self.jsonFromView(gridAsJson);
    }

    self.changeCellState = function (cell) {
        if (!cell.isDead) {
            cell.isDead = true;
        } else {
            cell.isDead = false;
        }

        self.grid.remove();

        updateGrid(ko.toJSON(self.grid));
    };

    /*self.jsonFromView.subscribe(function(newValue) {
        updateGrid(newValue);
    });*/
};

ko.applyBindings(new ViewModel());