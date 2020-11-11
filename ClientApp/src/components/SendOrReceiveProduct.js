import React, { useState, useEffect } from 'react';
import axios from 'axios';

function SendOrReceiveProduct(props) {
    //const [currentCount, setCurrentCount] = useState(0);
    const displayName = props.name;
    const [loading, setLoading] = useState(true);
    // Used to receive the response.
    const [statusCode, setStatusCode] = useState(0);
    const [response, setResponse] = useState([]);
    // Used to store the field values.
    const [quantity, setQuantity] = useState("");
    const [productID, setID] = useState("");
    const [operation, setOperation] = useState("");
    const [products, setProducts] = useState([]);
    // Used to determine whether we are awaiting a response.
    const [waiting, setWaiting] = useState(false);

    async function populateProductData() {
        // npm install --save axios
        const response = await axios.get('Inventory/GetInventory?showDiscontinuedItems=false');
        setProducts(response.data);        
    }

    useEffect(() => {
        populateProductData();
    },[loading]);

    function handleFieldChange(event) {
        switch (event.target.id) {
            case "quantity":
                setQuantity(event.target.value);
                break;
            case "productID":
                setID(event.target.value);
                break;
            case "operation":
                setOperation(event.target.value);
                break;
        }
    }

    function handleSubmit(event) {
        event.preventDefault();
        setWaiting(true);
        
        axios(
            {
                // Specify the method to use (post/get/put/patch/delete).
                method: 'PATCH',
                // Specify the URL to send to.
                url: 'Inventory/' + operation,
                // Specify the query parameters (the stuff we used in postman).
                params: {
                    productID: productID,
                    quantity: quantity
                }
            }
            // .then() means that once the response is received, do something with it.
        ).then((res) => {
            // In this case, set our state to reflect what we got.
            setWaiting(false);
            setResponse(res.data);
            setStatusCode(res.status);
        }
            // .catch() runs rather than .then() if the initial method throws an exception, in which case we can pull the error code and message from the API error response.
        ).catch((err) => {
            setWaiting(false);
            setResponse(err.response.data);
            setStatusCode(err.response.status);
        });
    }

    return (
        <div>
           
            <h1>Send or Receive Product</h1>

            <p>{waiting ? "Awaiting response..." : `Response recieved ${statusCode}: ${JSON.stringify(response)}`}</p>

            <form onSubmit={handleSubmit}>             
                <label for="operation">Choose the desired action:</label>
                <select name="operation" id="operation" onChange={handleFieldChange}>             
                    <option value="Send"> Send product</option>
                    <option value="Receive"> Receive Product</option>
                </select>
                <br />
                <label for="productID">Product Name:</label>
                <select name="productID" id="productID" onChange={handleFieldChange}>
                    {products.map(product =>                        
                        <option key={product.id} value = {product.id} >
                        { product.name }
                        </option>                        
                    )}                      
                </select> 
                <br />
                <label htmlFor="quantity">Quantity</label>
                <input id="quantity" name="quantity" type="number" onChange={handleFieldChange} />
                <br />
                <input type="submit" value="Submit!" />
            </form>
        </div>
    );
}

export { SendOrReceiveProduct };


