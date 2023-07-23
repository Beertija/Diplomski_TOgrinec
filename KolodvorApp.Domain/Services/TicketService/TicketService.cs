using AutoMapper;
using KolodvorApp.Domain.Entities;
using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public class TicketService : ITicketService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Ticket> _repository;

    public TicketService(IMapper mapper, IRepository<Ticket> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task BuyTicket(TicketPurchaseDto purchaseInfo)
    {
        var model = _mapper.Map<Ticket>(purchaseInfo);
        await _repository.InsertAsync(model);
    }
}