using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Client.HttpServices;

public interface ITicketService
{
    Task<HttpResponseMessage> BuyTicket(TicketPurchaseDto purchaseInfo);
}