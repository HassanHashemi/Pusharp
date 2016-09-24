
namespace Pusharp.Clients
{
    public class TargeTypes
    {
        public const string IdentityModule = "....";
    }
    
    public class ClientMessage
    {
        public string TargetType { get; set; }
        public string JsonMessage { get; set; }
    }
}
