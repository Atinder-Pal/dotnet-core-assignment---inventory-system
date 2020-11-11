# dotnet-core-assignment---inventory-system-day-2

**Purpose:** This assignment is meant to challenge your mastery of ASP.NET Core and how well you are able to use ASP.NET Core Web API to create a CRUD application. Your goal in this assignment is to create an inventory management system which helps a company keep track of goods and supplies. As goods and supplies are restocked, sold, or used, your application should update the data appropriately to reflect the changes. This is a cumulative activity. Use your code from ASP.NET Core Practice - Inventory System Day 1 as a starting point.

**Author:** Atinder Pal

**Requirements:**
* Admin pages must be in React:
  * An Admin Page must make an http post request to the endpoint calling “CreateProduct”.
  * An Admin Page must make an http patch request to the endpoint calling “DiscontinueProductByID()”.
  * An Admin Page must make an http patch request to the endpoint calling “ReceiveProductByID()”.
  * An Admin Page must make an http patch request to the endpoint calling “SendProductByID()”.
  * An Admin Page must make an http get request that allows the user to view the full active inventory.
  * All pages must display a human-readable error message in the response Content if an error response is received.
  * All pages must display a success message if the action was successful.
* All information must be stored in a database.

**Link to Trello Board:** https://trello.com/b/NoIJw6Bd/nventory-system-day-2
