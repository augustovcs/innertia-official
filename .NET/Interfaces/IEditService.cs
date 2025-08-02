/*

Defined the packages and libraries for project usage below,
Keep just the "using" as already injected for better memory economy and usage.
For first time looking the project, keep maintain this arc model for Interfaces.

*/


using Auth.DTO;

namespace Auth.Interfaces;

public interface IEditService
{

    Task<bool> EditUser(AuthLoginDTO credentials);

}
