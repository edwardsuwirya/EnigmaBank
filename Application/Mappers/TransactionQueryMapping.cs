using Common.Requests;
using Common.Responses;
using Domain;
using Mapster;

namespace Application.Mappers;

public class TransactionQueryMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Transaction, TransactionResponse>()
            .Map(dest => dest.Balance, src => src.Amount);
    }
}