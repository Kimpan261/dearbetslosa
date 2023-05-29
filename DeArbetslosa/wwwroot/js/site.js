// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function newArrivalsRequest(date, time) {
    $.ajax({
        url: '/Arrivals/getFlightsJson',
        type: 'GET',
        data: {date: date, time: time},
        dataType: 'json',
        success: function (result) {
            //console.log(result);
            var table = buildTable(result);
            $('#timetable').empty().append(table);
        },
        error: function (error) {
            console.log(error);
        }
    });
}
   function buildTable(data) {
    // Create the table element
    var table = document.createElement('table');
    table.className = 'timetable';

    // Create the table header
    var tableHead = document.createElement('thead');
    var headerRow = document.createElement('tr');

    // Define the property-to-header mapping
    var headerLabels = {
        'arrivalTime.scheduledUtc': 'Arrival time',
        'departureAirportEnglish': 'Arriving from',
        'flightId': 'Flight ID',
        'locationAndStatus.flightLegStatusEnglish': 'Status'
    };

    // Iterate through the headerLabels and create the header cells
    Object.keys(headerLabels).forEach(function (property) {
        var th = document.createElement('th');
        th.textContent = headerLabels[property];
        headerRow.appendChild(th);
    });

    tableHead.appendChild(headerRow);
    table.appendChild(tableHead);

    // Create the table body
    var tableBody = document.createElement('tbody');
    data.forEach(function (item) {
        var row = document.createElement('tr');
        Object.keys(headerLabels).forEach(function (property) {
            var cell = document.createElement('td');
            var properties = property.split('.');
            var value = item;

            properties.forEach(function (prop) {
                value = value[prop];
            });

            if (property === 'arrivalTime.scheduledUtc') {
                var formattedDate = new Date(value).toLocaleTimeString("en-US", { hour12: false, hour: "2-digit", minute: "2-digit" });
                value = formattedDate;
            }

            cell.textContent = value;
            row.appendChild(cell);
        });
        tableBody.appendChild(row);
    });

    table.appendChild(tableBody);

    return table;
}



