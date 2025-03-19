using SultanShipping.Abstractions;
using SultanShipping.Contracts.Roles;

namespace SultanShipping.RoleServices.Services;

public interface IRoleService
{
    Task<IEnumerable<RoleResponse>> GetAllAsync(bool includeDisabled = false, CancellationToken cancellationToken = default);
    Task<Result<RoleDetailResponse>> GetAsync(string id);
}