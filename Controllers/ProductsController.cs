using System.Data;
using firmacityBackend.Data;
using firmacityBackend.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
//using Microsoft.AspNetCore.Authorization;

namespace firmacityBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private ConnectionDB connection;

        public ProductsController()
        {
            this.connection = new ConnectionDB();
        }

        private Boolean isAuthorization()
        {
            string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;
            return (token == "productsfirmacitytopsecret");
        }

        private List<Product> orderProductBy(List<Product> productList, string orderBy)
        {
            switch (orderBy)
            {
                case "price":
                    productList = productList.OrderBy(product => product.Price).ToList();
                    break;
                case "price_desc":
                    productList = productList.OrderByDescending(product => product.Price).ToList();
                    break;
                case "name":
                    productList = productList.OrderBy(product => product.Name).ToList();
                    break;
                case "name_desc":
                    productList = productList.OrderByDescending(product => product.Name).ToList();
                    break;
                default:
                    productList = new List<Product>();
                    break;
            }

            return productList;
        }

        //[Authorize]
        [HttpGet]
        [Route("getProductsList")]
        public IActionResult getProductsList(string orderBy = "")
        {
            if (!isAuthorization())
            {
                return BadRequest(new { message = "You can´t access this API" });
            }

            List<Product> productsList = new List<Product>();
            object results;
            string query = "SELECT * FROM products;";
            MySqlDataReader mySqlDataReader = null;

            if (connection.getConnection() != null)
            {
                mySqlDataReader = DbHelper.queryExecutor(query, this.connection);

                while (mySqlDataReader.Read())
                {
                    productsList.Add(DbHelper.createProduct(mySqlDataReader));
                }

                if (orderBy != "")
                {
                    productsList = orderProductBy(productsList, orderBy);
                    if (productsList.Count == 0)
                    {
                        return BadRequest(new { result = "[!] Invalid sort type" });
                    }
                }

                return Ok(new { Success = true, productList = productsList });
            }
            else
            {
                return BadRequest(new { message = "Database turned off" });
            }
        }

        [HttpGet]
        [Route("GetProduct")]
        public IActionResult getProduct(int productId)
        {
            if (!isAuthorization())
            {
                return BadRequest(new { result = "You can´t access this API" });
            }

            string query = $"SELECT * FROM products WHERE product_id = @productId";
            MySqlDataReader mySqlDataReader = null;
            Product product = null;

            if (connection.getConnection() != null)
            {
                var parameters = new Dictionary<string, object>();
                parameters.Add("@productId", productId);
                mySqlDataReader = DbHelper.queryExecutor(query, this.connection, parameters);

                while (mySqlDataReader.Read())
                {
                    product = DbHelper.createProduct(mySqlDataReader);
                }
            }

            if (product == null)
            {
                return BadRequest(new { result = "This product does not exist" });
            }

            return Ok(new { Success = true, result = product });
        }

        [HttpDelete]
        [Route("deleteProduct")]
        public IActionResult deleteProduct(int productId)
        {
            if (!isAuthorization())
            {
                return BadRequest(new { result = "You can´t access this API" });
            }

            if (connection.getConnection != null)
            {
                string query = "DELETE FROM products WHERE product_id = @productId;";
                var parameters = new Dictionary<string, object>();
                parameters.Add("@productId", productId);

                int rowsAffected = DbHelper.executeNonQuery(query, this.connection, parameters);
                if (rowsAffected == 0)
                {
                    return BadRequest(new { message = $"The productId {productId} don´t exits" });
                }
            }

            return Ok(new { message = $"The productId {productId} has been successfully deleted" });
        }

        [HttpPost]
        [Route("updateProduct")]
        public IActionResult updateProduct(Product product)
        {

            return Ok(new { Success = true });
        }
    }
}