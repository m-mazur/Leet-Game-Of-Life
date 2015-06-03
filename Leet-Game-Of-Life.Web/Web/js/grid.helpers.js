var GridHelpers = (function () {

    var Cell = function (x, y, isDead) {
        this.X = x,
        this.Y = y,
        this.IsDead = isDead;
    };

    function processGrid (data) {
        var refCell = data[data.length - 1];
        var grid = [];

        for (var i = 0; i < refCell.Y + 1; i++) {
            for (var j = 0; j < refCell.X + 1; j++) {
                grid.push(new Cell(j, i, true));

                data.forEach(function (cell) {
                    if (cell.X === j && cell.Y === i) {
                        grid.push(cell);
                        var index = grid.indexOf(cell);
                        grid.splice(index - 1, 1);
                    }
                });
            }
        }

        return grid;
    }

    function groupGrid (data) {
        return _.map(_.groupBy(data, function (data) {
            return data.Y;
        }));
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

    function countAliveCells (grid) {
        var aliveCellCount = 0;

        grid.forEach(function (cell) {
            if (!cell.IsDead) {
                aliveCellCount++;
            }
        });

        return aliveCellCount;
    }

    function incrementGenerationCount (generationCount) {
        generationCount++;
        return generationCount;
    }

    function parseGridToJson (grid) {
        return ko.toJSON(grid);
    }

    function parseGridFromJson (data) {
        return JSON.parse(data);
    }

    return {
        processGrid: processGrid,
        groupGrid: groupGrid,
        unGroupGrid: unGroupGrid,
        countAliveCells: countAliveCells,
        incrementGenerationCount: incrementGenerationCount,
        parseGridToJson: parseGridToJson,
        parseGridFromJson: parseGridFromJson
    }
})();