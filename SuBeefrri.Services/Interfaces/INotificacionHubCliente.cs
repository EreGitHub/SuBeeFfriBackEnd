namespace SuBeefrri.Services.Interfaces
{
    public interface INotificacionHubCliente
    {
        Task BroadcastMessage(bool type);
    }
}
