var orderCustomer;
var orderLocation;
var order;

function loadLocationData() {
    return fetch('/api/locations')
        .then(resp => {
            if (!resp.ok) {
                throw new Error(`failed to get locations (${resp.status})`);
            }
            return resp.json();
        });
}

function loadCustomerData() {
    return fetch('/api/customers')
        .then(resp => {
            if (!resp.ok) {
                throw new Error(`failed to get customers (${resp.status})`);
            }
            return resp.json();
        });
}


loadLocationData().then(locs => {
    locations = locs;
    let locSelect = document.getElementById("order-location");
    for (const l of locations) {
        let o = document.createElement("option");
        o.text = `${l.id} - ${l.name}`;
        o.value = l.id;
        locSelect.appendChild(o);
    }
});

var customerList;
loadCustomerData().then(customers => {
    customerList = customers;
    let customerSelect = document.getElementById("order-customer");
    for (const c of customers) {
        let o = document.createElement("option");
        o.text = `${c.firstName} ${c.lastName}`;
        o.value = c.id
        customerSelect.appendChild(o);
    }
});

function selectedCustomer(event) {
    let selectedID = parseInt(event.target.value);
    customerList.forEach(element => {
        if (element.id == selectedID) {
            orderCustomer = element;
        }
    });
}

const inventory = document.getElementById("inventory");
const inventoryTable = document.getElementById("product-list-table");
function selectedLocation(event) {
    let selectedID = parseInt(event.target.value);
    locations.forEach(loc => {
        if (loc.id == selectedID) {
            orderLocation = loc;
            
            let tableHeaderRowCount = 1;
            let rowCount = inventoryTable.rows.length;
            for(let i = tableHeaderRowCount; i < rowCount; i++) {
                inventoryTable.deleteRow(tableHeaderRowCount);
            }

            for(const p of orderLocation.inventory) {
                const row = inventoryTable.insertRow();
                row.innerHTML = `
                <td>${p.name}</td>
                <td>${p.price.toFixed(2)}</td>
                <td class="center-table-data">${p.amount}</td>
                <td class="center-table-data">0</td>`;
            }
            inventory.hidden = false;
            inventoryTable.hidden = false;
        }
    });
}
