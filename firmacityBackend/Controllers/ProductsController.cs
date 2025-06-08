using System.Data;
using System.Text.Json.Nodes;
using firmacityBackend.Data;
using firmacityBackend.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace firmacityBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private ConnectionDB connection;

        public ProductsController() {
            this.connection = new ConnectionDB();
        }

        [HttpGet]
        [Route("getProductsList")]
        public IActionResult getProductsList() {

            string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;
            if (token != "productsfirmacitytopsecret") {
                return Ok(new { Success = false, message = "You can´t acccess this API"});
            }

            List<Product> productsList = new List<Product>();
            object results;
            string query = "SELECT * FROM products;";
            MySqlDataReader reader = null;

            if (connection.getConnection() != null)
            {
                MySqlCommand mySqlCommand = new MySqlCommand(query);
                mySqlCommand.Connection = connection.getConnection();
                reader = mySqlCommand.ExecuteReader();

                while (reader.Read()) {
                    int productId = reader.GetInt32("product_id");
                    string productName = reader.GetString("name");
                    int productCant = reader.GetInt32("quantity");
                    int productPrice = reader.GetInt32("price");

                    Product product = new Product(productId, productName, productCant, productPrice);
                    productsList.Add(product);
                }

                results = new { Success = true, productList = productsList };
            }
            else {
                results = new { Success = false, message = "error"};
            }

            return Ok(results);
        }
    }
}
