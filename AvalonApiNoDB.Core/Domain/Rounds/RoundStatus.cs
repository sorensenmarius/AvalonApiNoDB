namespace AvalonApiNoDB.Core.Domain.Rounds
{
    public enum RoundStatus
    {
        SelectingTeam = 0,
        VotingForTeam = 1,
        VotingExpedition = 2,
        TeamApproved = 3,
        MissionSuccess = 4,
        MissionFailed = 5,
        RoundEnded = -1
    }
}
