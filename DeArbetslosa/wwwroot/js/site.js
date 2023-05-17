// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//ARRIVALS / DEPARTURE
function sortTable() {
    console.log("ye");
    const table = document.getElementById("arrivalsTable"); // TODO for departure later
    const rows = table.getElementsByTagName("tr");
    const dateColumnIndex = 0;
    const sortTableByDate = () => {
        const sortedRows = Array.from(rows).slice(1); // Exclude the table header row
        sortedRows.sort((rowA, rowB) => {
            const dateA = new Date(rowA.cells[dateColumnIndex].textContent);
            const dateB = new Date(rowB.cells[dateColumnIndex].textContent);
            return dateA - dateB;
        });

        while (table.rows.length > 1) {
            table.deleteRow(1); // Remove existing rows, starting from index 1
        }

        sortedRows.forEach((row) => {
            table.appendChild(row); // Append sorted rows back to the table
        });
    };

    // Add click event listener to the date column header
    //const dateHeader = table.rows[0].cells[dateColumnIndex];
    //dateHeader.addEventListener("click", sortTableByDate);
    sortTableByDate();
}

