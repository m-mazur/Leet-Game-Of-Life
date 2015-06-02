var GridHelpers = (function () {

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

    return {
        groupGrid: groupGrid,
        unGroupGrid: unGroupGrid,
        countAliveCells: countAliveCells
    }
})();