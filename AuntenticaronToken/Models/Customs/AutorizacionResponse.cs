namespace AutenticarConToken.Models.Customs
{
    public class AutorizacionResponse
    {

        public string Token { get; set; } //Devuelve el token


        public bool Resultado { get; set; }

        public string Mensaje { get; set; }

    }
}
