using System.Reflection.PortableExecutable;
using firmacityBackend.Models;
using MySql.Data.MySqlClient;

namespace firmacityBackend.Data
{
    public class DbHelper
    {
        public static MySqlDataReader queryExecutor(string query, ConnectionDB connection, Dictionary<string, object> parameters = null) {
            MySqlDataReader reader = null;
            try {
                MySqlCommand mySqlCommand = new MySqlCommand(query);
                mySqlCommand.Connection = connection.getConnection();

                if (parameters != null) {
                    foreach (var parameter in parameters) {
                        mySqlCommand.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }
                }

                reader = mySqlCommand.ExecuteReader();
            }
            catch (Exception ex) {
                Console.WriteLine($"[!] Error : {ex.Message}");
            }

            return reader;
        }

        public static Product createProduct(MySqlDataReader reader) {
            int productId = reader.GetInt32("product_id");
            string productName = reader.GetString("name");
            int productCant = reader.GetInt32("quantity");
            int productPrice = reader.GetInt32("price");

            Product product = new Product(productId, productName, productCant, productPrice);
            return product;
        }
    }
}
