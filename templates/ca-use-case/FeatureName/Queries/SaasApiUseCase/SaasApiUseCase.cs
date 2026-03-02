using SaasApi.Application.Common.Interfaces;

namespace SaasApi.Application.FeatureName.Queries.SaasApiUseCase;

public record SaasApiUseCaseQuery : IRequest<object>
{
}

public class SaasApiUseCaseQueryValidator : AbstractValidator<SaasApiUseCaseQuery>
{
    public SaasApiUseCaseQueryValidator()
    {
    }
}

public class SaasApiUseCaseQueryHandler : IRequestHandler<SaasApiUseCaseQuery, object>
{
    private readonly IApplicationDbContext _context;

    public SaasApiUseCaseQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<object> Handle(SaasApiUseCaseQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
