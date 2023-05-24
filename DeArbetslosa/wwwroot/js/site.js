// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function organizeTable() {
    const table = document.getElementById("timetable");
    const headers = Array.from(table.querySelectorAll("th"));
    const sortingOrder = headers.map(() => 0);

    function compareTime(a, b) {
        const timeA = a.textContent.trim().split(":");
        const timeB = b.textContent.trim().split(":");

        const hourA = parseInt(timeA[0]);
        const hourB = parseInt(timeB[0]);

        const minuteA = parseInt(timeA[1]);
        const minuteB = parseInt(timeB[1]);

        if (hourA < hourB) {
            return -1;
        } else if (hourA > hourB) {
            return 1;
        } else {
            if (minuteA < minuteB) {
                return -1;
            } else if (minuteA > minuteB) {
                return 1;
            } else {
                return 0;
            }
        }
    }

    // Function to sort the table based on a column index
    function sortTable(columnIndex) {
        const rows = Array.from(table.querySelectorAll("tbody tr"));
        if (columnIndex == 0) {
            rows.sort((rowA, rowB) => {
                const cellA = rowA.cells[columnIndex];
                const cellB = rowB.cells[columnIndex];

                const comparison = compareTime(cellA, cellB);

                return sortingOrder[columnIndex] === 0 ? comparison : -comparison;
            });
        } else { 
            //rows.sort((a, b) => a.localeCompare(b));
            console.log(rows);
        }

        sortingOrder[columnIndex] ^= 1; // Toggle the sorting order

        // Update the table with the sorted rows
        rows.forEach((row, index) => {
            table.tBodies[0].appendChild(row);
        });
    }

    // Add click event listeners to the table headers
    headers.forEach((header, index) => {
        header.addEventListener("click", () => sortTable(index));
    });
    sortTable(0);
}


