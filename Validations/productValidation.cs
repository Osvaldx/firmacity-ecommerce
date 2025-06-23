using firmacityBackend.Models;

namespace firmacityBackend.Validations
{
    public static class productValidation
    {
        public static Boolean isValidProduct(Product product, out string? error) {

            if (product.Id < 0) {
                error = "[!] The ID of the product can´t be less than 0";
                return false;
            }

            if (string.IsNullOrEmpty(product.Name)) {
                error = "[!] The product name can´t be null or ''";
                return false;
            }

            if (product.Quantity < 0) {
                error = "[!] The quantity of the product can´t be less than 0";
                return false;
            }

            if (product.Price < 0) {
                error = "[!] The price of the product can´t be less than 0";
                return false;
            }
            error = null;
            return true;
        }
    }
}
