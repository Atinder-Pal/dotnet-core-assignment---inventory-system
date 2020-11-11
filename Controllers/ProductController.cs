using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventorySystemDay2.Models;
using InventorySystemDay2.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystemDay2.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public Product CreateProduct(string name, string quantity, string discontinued)
        {
            int parsedQuantity = 0;
            bool parsedDiscontinued = false;
            ValidationException exception = new ValidationException();

            name = (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) ? null : name;
            quantity = (string.IsNullOrEmpty(quantity) || string.IsNullOrWhiteSpace(quantity)) ? null : quantity;
            discontinued = (string.IsNullOrEmpty(discontinued) || string.IsNullOrWhiteSpace(discontinued)) ? null : discontinued;

            using (InventoryContext context = new InventoryContext())
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(name), nameof(name) + " is null."));
                }
                else
                {
                    if (context.Products.Any(x => x.Name.ToLower() == name.ToLower()))
                    {
                        exception.ValidationExceptions.Add(new Exception("Product already exists"));
                    }
                }
                if (string.IsNullOrWhiteSpace(quantity))
                {
                    parsedQuantity = 0;
                }
                else
                {
                    if (!int.TryParse(quantity, out parsedQuantity))
                    {
                        exception.ValidationExceptions.Add(new Exception("Invalid Quantity Value"));
                    }
                    else
                    {
                        if (parsedQuantity < 0)
                        {
                            parsedQuantity = 0;
                        }
                    }
                }
                if (string.IsNullOrWhiteSpace(discontinued))
                {
                    parsedDiscontinued = false;
                }
                else
                {
                    if (!bool.TryParse(discontinued, out parsedDiscontinued))
                    {
                        exception.ValidationExceptions.Add(new Exception("Value of Discontinued should be either true or false"));
                    }
                }
                if (exception.ValidationExceptions.Count > 0)
                {
                    throw exception;
                }
                Product newProduct = new Product()
                {
                    Name = name,
                    Quantity = parsedQuantity,
                    Discontinued = parsedDiscontinued
                };
                context.Products.Add(newProduct);
                context.SaveChanges();

                return newProduct;
            }
        }

        public Product DiscontinueProductByID(string productID)
        {
            Product result;
            int parsedID = 0;
            ValidationException exception = new ValidationException();

            productID = (string.IsNullOrEmpty(productID) || string.IsNullOrWhiteSpace(productID)) ? null : productID;

            using (InventoryContext context = new InventoryContext())
            {
                if (string.IsNullOrWhiteSpace(productID))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(productID), nameof(productID) + " is null."));
                }
                else
                {
                    if (!int.TryParse(productID, out parsedID))
                    {
                        exception.ValidationExceptions.Add(new Exception("ID is not valid"));
                    }
                    else
                    {
                        if (!context.Products.Any(x => x.ID == parsedID))
                        {
                            exception.ValidationExceptions.Add(new Exception("Product doesnot exist"));
                        }
                        else
                        {
                            if (context.Products.Where(x => x.ID == parsedID).SingleOrDefault().Discontinued == true)
                            {
                                exception.ValidationExceptions.Add(new Exception("Product is already discontinued in Inventory"));
                            }
                        }
                    }
                }
                if (exception.ValidationExceptions.Count > 0)
                {
                    throw exception;
                }
                result = context.Products.Where(x => x.ID == parsedID).SingleOrDefault();
                result.Discontinued = true;
                context.SaveChanges();
            }
            return result;
        }

        public Product ReceiveProductByID(string productID, string quantity)
        {
            Product result;
            int parsedID = 0, parsedQuantity = 0;
            ValidationException exception = new ValidationException();

            productID = (string.IsNullOrEmpty(productID) || string.IsNullOrWhiteSpace(productID)) ? null : productID;
            quantity = (string.IsNullOrEmpty(quantity) || string.IsNullOrWhiteSpace(quantity)) ? null : quantity;
            using (InventoryContext context = new InventoryContext())
            {
                if (string.IsNullOrWhiteSpace(productID))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(productID), nameof(productID) + " is null."));
                }
                else
                {
                    if (!int.TryParse(productID, out parsedID))
                    {
                        exception.ValidationExceptions.Add(new Exception("ID is not valid"));
                    }
                    else
                    {
                        if (!context.Products.Any(x => x.ID == parsedID))
                        {
                            exception.ValidationExceptions.Add(new Exception("Product doesnot exist"));
                        }
                    }
                }
                if (string.IsNullOrWhiteSpace(quantity))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(quantity), nameof(quantity) + " is null."));
                }
                else
                {
                    if (!int.TryParse(quantity, out parsedQuantity))
                    {
                        exception.ValidationExceptions.Add(new Exception("Value of Quantity is not valid"));
                    }
                    else if (parsedQuantity <= 0)
                    {
                        exception.ValidationExceptions.Add(new Exception("Quantity received can not be negative or zero"));
                    }
                }
                if (exception.ValidationExceptions.Count > 0)
                {
                    throw exception;
                }
                result = context.Products.Where(x => x.ID == parsedID).Single();
                result.Quantity += parsedQuantity;
                context.SaveChanges();
            }
            return result;
        }
        public Product SendProductByID(string productID, string quantity)
        {
            Product result;
            int parsedID = 0, parsedQuantity = 0;
            ValidationException exception = new ValidationException();

            productID = (string.IsNullOrEmpty(productID) || string.IsNullOrWhiteSpace(productID)) ? null : productID;
            quantity = (string.IsNullOrEmpty(quantity) || string.IsNullOrWhiteSpace(quantity)) ? null : quantity;
            using (InventoryContext context = new InventoryContext())
            {
                if (string.IsNullOrWhiteSpace(productID))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(productID), nameof(productID) + " is null."));
                }
                else
                {
                    if (!int.TryParse(productID, out parsedID))
                    {
                        exception.ValidationExceptions.Add(new Exception("ID is not valid"));
                    }
                    else
                    {
                        if (!context.Products.Any(x => x.ID == parsedID))
                        {
                            exception.ValidationExceptions.Add(new Exception("Product doesnot exist"));
                        }
                    }
                }
                if (string.IsNullOrWhiteSpace(quantity))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(quantity), nameof(quantity) + " is null."));
                }
                else
                {
                    if (!int.TryParse(quantity, out parsedQuantity))
                    {
                        exception.ValidationExceptions.Add(new Exception("Value of Quantity is not valid"));
                    }
                    else if (parsedQuantity <= 0)
                    {
                        exception.ValidationExceptions.Add(new Exception("Quantity to be sent can not be negative or zero"));
                    }
                    else
                    {
                        int currentQty = context.Products.Where(x => x.ID == parsedID).SingleOrDefault().Quantity;
                        if (currentQty < parsedQuantity)
                        {
                            exception.ValidationExceptions.Add(new Exception($"Quantity: {parsedQuantity} requested to be sent is greater than total quantity: {currentQty} in inventory"));
                        }
                    }
                }
                if (exception.ValidationExceptions.Count > 0)
                {
                    throw exception;
                }
                result = context.Products.Where(x => x.ID == parsedID).Single();
                result.Quantity -= parsedQuantity;
                context.SaveChanges();
            }
            return result;
        }

        public List<Product> GetInventory(string showDiscontinuedItems, string orderBy)
        {
            List<Product> result = new List<Product>();
            bool parsedDiscontinued = false;
            ValidationException exception = new ValidationException();

            showDiscontinuedItems = (string.IsNullOrEmpty(showDiscontinuedItems) || string.IsNullOrWhiteSpace(showDiscontinuedItems)) ? null : showDiscontinuedItems;
            orderBy = (string.IsNullOrEmpty(orderBy) || string.IsNullOrWhiteSpace(orderBy)) ? null : orderBy;

            using (InventoryContext context = new InventoryContext())
            {
                if (string.IsNullOrWhiteSpace(showDiscontinuedItems))
                {
                    parsedDiscontinued = true;
                }
                else if (!bool.TryParse(showDiscontinuedItems, out parsedDiscontinued))
                {
                    exception.ValidationExceptions.Add(new Exception("Value of Discontinued should be either true or false"));
                }
                if ((!string.IsNullOrWhiteSpace(orderBy)) && orderBy.ToLower() != "name" && orderBy.ToLower() != "quantity")
                {
                    exception.ValidationExceptions.Add(new Exception("You can either orderby Name or Quantity. input correct value for orderBy"));
                }
                if (exception.ValidationExceptions.Count > 0)
                {
                    throw exception;
                }
                if (!string.IsNullOrWhiteSpace(orderBy) && orderBy.ToLower() == "name")
                    result = parsedDiscontinued ? context.Products.OrderBy(x => x.Name).ToList() : context.Products.Where(x => x.Discontinued == false).OrderBy(x => x.Name).ToList();
                else if (!string.IsNullOrWhiteSpace(orderBy) && orderBy.ToLower() == "quantity")
                    result = parsedDiscontinued ? context.Products.OrderByDescending(x => x.Quantity).ToList() : context.Products.Where(x => x.Discontinued == false).OrderByDescending(x => x.Quantity).ToList();
                else
                    result = parsedDiscontinued ? context.Products.ToList() : context.Products.Where(x => x.Discontinued == false).ToList();
            }
            return result.Any() ? result : null;
        }
        public Product GetProductByID(string productID)
        {
            Product result;
            int parsedID = 0;
            ValidationException exception = new ValidationException();

            productID = (string.IsNullOrEmpty(productID) || string.IsNullOrWhiteSpace(productID)) ? null : productID;

            using (InventoryContext context = new InventoryContext())
            {
                if (string.IsNullOrWhiteSpace(productID))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(productID), nameof(productID) + " is null."));
                }
                else if (!int.TryParse(productID, out parsedID))
                {
                    exception.ValidationExceptions.Add(new Exception("ID is not valid"));
                }

                if (exception.ValidationExceptions.Count > 0)
                {
                    throw exception;
                }
                result = context.Products.Where(x => x.ID == parsedID).SingleOrDefault();
            }
            return result;
        }
    }
}