

const table = document.getElementById("customer-list-table");

function loadCustomerData() {
    return fetch('/api/customers')
        .then(resp => {
            if (!resp.ok) {
                throw new Error(`failed to get customers (${resp.status})`);
            }
            return resp.json();
        });
}

loadCustomerData()
    .then(customers => {
        for (const c of customers) {
            const r = table.insertRow();
            r.innerHTML = `
            <td>${c.id}</td>
            <td>${c.firstName}</td>
            <td>${c.lastName}</td>
            <td>${c.defaultLocationID}</td>`;
        }
        table.hidden = false;
    })
    .catch(e => {
        // handle the error
    });

var locations;
function loadLocationData() {
    return fetch('/api/locations')
        .then(resp => {
            if (!resp.ok) {
                throw new Error(`failed to get locations (${resp.status})`);
            }
            return resp.json();
        });
}

loadLocationData()
    .then(locs => {
        console.log(locs);
        locations = locs;
        console.log(locations);
        let locIDSelect = document.getElementById("defaultLocId");
        for (const l of locations) {
            console.log(l);
            let o = document.createElement("option");
            o.text = l.name;
            o.value = `${l.id}`;
            locIDSelect.appendChild(o);
        }
    });

const submitBtn = document.getElementById("addCustomer");
addCustomer.addEventListener('click', function(event) {
    event.preventDefault();

    let c = {
        firstName: document.getElementById("customer-firstName").textContent,
        lastName: document.getElementById("customer-lastName").textContent,
        defaultLocationId: document.getElementById("defaultLocId").value
    }

    console.log(c);
});
