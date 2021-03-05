

const table = document.getElementById("customer-list-table");

function loadCustomerData() {
    return fetch('/api/customers')
        .then(resp => {
            if (!resp.ok) {
                throw new Error(`failed to get customers (${resp.status})`);
            }
            console.log(resp);
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

