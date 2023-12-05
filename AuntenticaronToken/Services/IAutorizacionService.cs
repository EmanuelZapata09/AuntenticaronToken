using AutenticarConToken.Models.Customs;

namespace AutenticarConToken.Services
    
{
    public interface IAutorizacionService
    {
        Task<AutorizacionResponse>DevolverToken(AutorizacionRequest autorizacion);
    }
}
