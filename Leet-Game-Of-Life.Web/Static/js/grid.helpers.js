var GridHelpers = function () {
    function groupGrid(data) {
        var groupedGrid = _.groupBy(data, function (data) {
            return data.Y;
        });

        return _.chain(groupedGrid).map(function (grid) {
            return grid;
        }).value();
    }

    return {
        groupGrid: groupGrid(data)
    }
}