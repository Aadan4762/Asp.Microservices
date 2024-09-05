using User_Service.Data;
using User_Service.Dto;
using User_Service.Models.Entities;

namespace User_Service.Interface;

public interface IAuthService
{
    Task<AuthServiceResponseDto> SeedRolesAsync();
    Task<RegisterAuthResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthServiceResponseDto> LoginAsync(LoginDto loginDto);
    Task<MakeAdminOwnerResponseDto> MakeAdminAsync(UpdatePermissionDto updatePermissionDto);
    Task<MakeAdminOwnerResponseDto> MakeOwnerAsync(UpdatePermissionDto updatePermissionDto);
    Task<RefreshAuthResponseDto> RefreshTokenAsync(TokenRefreshDto tokenRefreshDto);
}