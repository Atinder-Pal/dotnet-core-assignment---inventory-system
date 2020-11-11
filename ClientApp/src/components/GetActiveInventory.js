import React, { useState, useEffect } from 'react';
import axios from 'axios';

function GetActiveInventory(props) {
    const displayName = GetActiveInventory.name;

    // Configure our state, and our setState standin methods.
    const [products, setProducts] = useState([]);
    const [loading, setLoading] = useState(true);

    const [statusCode, setStatusCode] = useState(0);
    const [response, setResponse] = useState([]);
    const [waiting, setWaiting] = useState(false);
    function handleClick(event) {
        event.preventDefault();
        setWaiting(true);
        let id = event.target.value;

        axios(
            {
                // Specify the method to use (post/get/put/patch/delete).
                method: 'PATCH',
                // Specify the URL to send to.
                url: 'Inventory/Discontinue',
                // Specify the query parameters (the stuff we used in postman).
                params: {
                    productID: id                  
                }
            }
            // .then() means that once the response is received, do something with it.
        ).then((res) => {
            // In this case, set our state to reflect what we got.
            setWaiting(false);
            setResponse(res.data);
            setStatusCode(res.status);
            setLoading(true);
        }
            // .catch() runs rather than .then() if the initial method throws an exception, in which case we can pull the error code and message from the API error response.
        ).catch((err) => {
            setWaiting(false);
            setResponse(err.response.data);
            setStatusCode(err.response.status);
        });
    }
    // Build the table based on Products data.
    function renderProductsTable(products) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Product ID</th>
                        <th>Name</th>
                        <th>Quantity</th> 
                        <th>Discontinue</th> 
                    </tr>
                </thead>
                <tbody>
                    {products.map(product =>
                        <tr key={product.id}>
                            <td>{product.id}</td>
                            <td>{product.name}</td>
                            <td>{product.quantity}</td> 
                            <td><button value={product.id} onClick={handleClick}>Discontinue</button></td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    // Grab our data from our API.
    async function populateProductData() {
        // npm install --save axios
        const response = await axios.get('Inventory/GetInventory?showDiscontinuedItems=false');
        setProducts(response.data);
        setLoading(false);
    }

    useEffect(() => {
        populateProductData();
    }, [loading]);

    let contents = loading
        ? <p><em>Loading...</em></p>
        : renderProductsTable(products);

    return (
        <div>
            <h1 id="tabelLabel" >Products</h1>
            <p>This component demonstrates fetching data from the server.</p>
            <p>{waiting ? "Awaiting response..." : `Response recieved ${statusCode}: ${JSON.stringify(response)}`}</p>
            {contents}

            <button className="btn btn-primary" onClick={() => { setLoading(true) }}>Refresh</button>
        </div>
    );
}

export { GetActiveInventory };

