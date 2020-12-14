namespace Challonge.Api
{
    /// <summary>
    /// Holds the credentials used to access the Challonge API.
    /// </summary>
    public interface IChallongeCredentials
    {
        public string Username { get; set; }
        public string ApiKey { get; set; }
    }
}
