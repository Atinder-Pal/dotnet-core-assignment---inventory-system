using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using InventorySystemDay2.Models;
using InventorySystemDay2.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystemDay2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        [HttpPost("Create")]
        public ActionResult<Product> ProductCreate_POST(string name, string quantity, string discontinued)
        {
            ActionResult<Product> result;
            try
            {
                result = new ProductController().CreateProduct(name, quantity, discontinued);
            }
            catch (ValidationException e)
            {
                string error = "Error(s) During Creation: " +
                    e.ValidationExceptions.Select(x => x.Message)
                    .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500, "Unknown error occurred, please try again later.");
            }
            return result;
        }

        [HttpPatch("Discontinue")]
        public ActionResult<Product> ProductDiscontinue_PATCH(string productID)
        {
            ActionResult<Product> result;
            try
            {
                result = new ProductController().DiscontinueProductByID(productID);
            }
            catch (ValidationException e)
            {
                string error = "Error(s) During Execution: " +
                    e.ValidationExceptions.Select(x => x.Message)
                    .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500, "Unknown error occurred, please try again later.");
            }
            return result;
        }
        [HttpPatch("Receive")]
        public ActionResult<Product> ProductReceive_PATCH(string productID, string quantity)
        {
            ActionResult<Product> result;
            try
            {
                result = new ProductController().ReceiveProductByID(productID,quantity);
            }
            catch (ValidationException e)
            {
                string error = "Error(s) During Receiving Product: " +
                    e.ValidationExceptions.Select(x => x.Message)
                    .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500, "Unknown error occurred, please try again later.");
            }
            return result;
        }

        [HttpPatch("Send")]
        public ActionResult<Product> ProductSend_PATCH(string productID, string quantity)
        {
            ActionResult<Product> result;
            try
            {
                result = new ProductController().SendProductByID(productID, quantity);
            }
            catch (ValidationException e)
            {
                string error = "Error(s) During Sending Product: " +
                    e.ValidationExceptions.Select(x => x.Message)
                    .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500, "Unknown error occurred, please try again later.");
            }
            return result;
        }
        [HttpGet("GetInventory")]
        public ActionResult<List<Product>> ProductGetInventory_GET(string showDiscontinuedItems, string orderBy)
        {
            ActionResult<List<Product>> result;
            try
            {
                result = new ProductController().GetInventory(showDiscontinuedItems, orderBy);
            }
            catch (ValidationException e)
            {
                string error = "Error(s) During Sending Product: " +
                    e.ValidationExceptions.Select(x => x.Message)
                    .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500, "Unknown error occurred, please try again later.");
            }
            return result;
        }
        [HttpGet("GetProduct")]
        public ActionResult<Product> ProductGetProduct_GET(string productID)
        {
            ActionResult<Product> result;
            try
            {
                result = new ProductController().GetProductByID(productID);
            }
            catch (ValidationException e)
            {
                string error = "Error(s) During Sending Product: " +
                    e.ValidationExceptions.Select(x => x.Message)
                    .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500, "Unknown error occurred, please try again later.");
            }
            return result;
        }
    }
}
