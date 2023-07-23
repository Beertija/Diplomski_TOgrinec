namespace KolodvorApp.Shared.DTOs;

public class TicketPurchaseDto
{
    public MergedRoutesDto Route { get; set; } = new();

    public Guid UserId { get; set; }
}