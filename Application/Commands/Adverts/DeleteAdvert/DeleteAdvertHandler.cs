using Application.Contracts;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System.Data.Common;

namespace Application.Commands.Adverts;

public sealed class DeleteAdvertHandler : IRequestHandler<DeleteAdvertCommand, IResponse>
{
    private readonly IAdvertRepository _advertRepo;
    private readonly IMapper _mapper;

    public DeleteAdvertHandler(IAdvertRepository advertRepository, IMapper mapper)
    {
        _advertRepo = advertRepository;
        _mapper = mapper;
    }
    public async Task<IResponse> Handle(DeleteAdvertCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _advertRepo.RemoveByIdAsync(request.AdvertId);
            var changes = await _advertRepo.SaveChangesAsync();

            if (changes == 0 || !result)
                return ErrorResults.ResourceNotFound();
        }
        catch (DbException ex)
        {
            return ErrorResults.DatabaseError(ex.Message);
        }
        catch (Exception x)
        {
            return ErrorResults.UnexpectedError(x.Message);
        }

        return Success.Instance;
    }
}
