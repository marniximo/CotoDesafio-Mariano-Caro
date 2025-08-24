using CotoDesafio.Domain;
using MediatR;

namespace CotoDesafio.Application.Queries
{

    public record GetPercentagesQuery()
        : IRequest<IEnumerable<PercentageByCenterDto>>;

    public record PercentageByCenterDto(string DistributionCenterId, string DistributionCenterName, List<PercentageByModelDto> PercentageByModel);

    public record PercentageByModelDto(string CarModel, decimal SalesTotalPercentage);
}