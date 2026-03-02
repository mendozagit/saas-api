using SaasApi.Application.Common.Interfaces;

namespace SaasApi.Application.FeatureName.Commands.SaasApiUseCase;

public record SaasApiUseCaseCommand : IRequest<object>
{
}

public class SaasApiUseCaseCommandValidator : AbstractValidator<SaasApiUseCaseCommand>
{
    public SaasApiUseCaseCommandValidator()
    {
    }
}

public class SaasApiUseCaseCommandHandler : IRequestHandler<SaasApiUseCaseCommand, object>
{
    private readonly IApplicationDbContext _context;

    public SaasApiUseCaseCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<object> Handle(SaasApiUseCaseCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
