function SelectedValue(DataTableID, rowData) {
    var table = document.getElementById(DataTableID);
    var activeRow = table.querySelector(".dtactive");

    if (activeRow) {
        var dataTable = $(table).DataTable();
        var data = dataTable.row(activeRow).data();
        return data[rowData];
    } else {
        return 0;
    }
}

function DateToText(jsonDate) {
    if (jsonDate != null) {

        var date = new Date(jsonDate);
        var mm = (date.getMonth() + 1).toString();
        var dd = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();

        if ((date.getMonth() + 1).toString() < 10) {
            mm = "0" + (date.getMonth() + 1).toString();
        }
        var res = date.getFullYear().toString() + "-" + mm + "-" + dd;
        return res;
    }
    return null;
}
