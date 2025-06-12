using System.Data;
using System.Reflection.PortableExecutable;
using firmacityBackend.Data;
using firmacityBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Tree;
using MySql.Data.MySqlClient;
//using Microsoft.AspNetCore.Authorization;

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

        private Boolean isAuthorization() {
            string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;
            return (token == "productsfirmacitytopsecret");
        }

        //[Authorize]
        [HttpGet]
        [Route("getProductsList")]
        public IActionResult getProductsList() {
            if (!isAuthorization()) {
                return BadRequest(new { message = "You can´t access this API"});
            }

            List<Product> productsList = new List<Product>();
            object results;
            string query = "SELECT * FROM products;";
            MySqlDataReader mySqlDataReader = null;

            if (connection.getConnection() != null) {
                mySqlDataReader = DbHelper.queryExecutor(query, this.connection);

                if (mySqlDataReader == null)
                {
                    return BadRequest(new { result = "[!] Database turned off" });
                }

                while (mySqlDataReader.Read()) {
                    productsList.Add(DbHelper.createProduct(mySqlDataReader));
                }

                return Ok(new { Success = true, productList = productsList });
            }
            else {
                return BadRequest(new { message = "Database turned off" });
            }
        }

        [HttpGet]
        [Route("GetProduct")]
        public IActionResult getProduct(int productId) {
            if (!isAuthorization()) {
                return BadRequest(new { result = "You can´t access this API" });
            }

            string query = $"SELECT * FROM products WHERE product_id = @productId";
            MySqlDataReader mySqlDataReader = null;
            Product product = null;

            if (connection.getConnection() != null) {
                var parameters = new Dictionary<string, object>();
                parameters.Add("@productId",productId);
                mySqlDataReader = DbHelper.queryExecutor(query, this.connection, parameters);

                if (mySqlDataReader == null) {
                    return BadRequest(new { result = "[!] Database turned off" });
                }

                while (mySqlDataReader.Read()) {
                    product = DbHelper.createProduct(mySqlDataReader);
                }
            }

            if (product == null) {
                return BadRequest(new { result = "This product does not exist" });
            }

            return Ok(new { Success = true, result = product});
        }
    }
}
