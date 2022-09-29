namespace OniCore.Application.Pipelines.Authorization
{
    public interface ISecuredRequest
    {
        public string[] RequiredRoles { get; }
    }
}
