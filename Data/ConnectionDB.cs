using MySql.Data.MySqlClient;

namespace firmacityBackend.Data
{
    public class ConnectionDB
    {
        private MySqlConnection connection;
        private string server = "localhost";
        private string database = "pharmastock";
        private string user = "root";
        private string password = "rootnahuel";
        private string connectionChain;

        public ConnectionDB() {
            this.connectionChain = $"Database={this.database}; DataSource={this.server}; User Id={this.user}; Password={this.password}";
        }

        public MySqlConnection getConnection() {
            if (connection == null) {
                try
                {
                    this.connection = new MySqlConnection(this.connectionChain);
                    this.connection.Open();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"[!] Error {ex.Message}");
                }
            }

            return connection;
        }
    }
}
