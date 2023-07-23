using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public interface ITicketService
{
    Task BuyTicket(TicketPurchaseDto purchaseInfo);
}