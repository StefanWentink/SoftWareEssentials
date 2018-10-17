namespace SWE.Http.Interfaces
{
    public interface ISecurityToken
    {
        string Schema { get; }

        string Parameter { get; }
    }
}