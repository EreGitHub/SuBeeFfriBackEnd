using Microsoft.AspNetCore.SignalR;
using SuBeefrri.Services.Interfaces;

namespace SuBeefrri.Services.HubNotifications
{
    public class NotificacionHub : Hub<INotificacionHubCliente>
    {
    }
}
