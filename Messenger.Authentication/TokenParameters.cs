namespace Messenger.Authentication
{
    // TODO(@Prostaandr): Данные захардкожены, что не есть хорошо, в идеале потом вынести это в конфигурацию. 
    public class TokenParameters
    {
        public string Issuer = "string";
        public string Audience = "string";
        public string SecretKey = "keykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykey"; // Такая строка, так как для HS256 нужно больше 128 бит
    }
}
