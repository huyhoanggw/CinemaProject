namespace Cinema.IdentityApi.Interfaces
{
    public interface IPermissionServices
    {
        public Task<List<string>> GetUserPermissionsAsync(string userId);
    }
}
