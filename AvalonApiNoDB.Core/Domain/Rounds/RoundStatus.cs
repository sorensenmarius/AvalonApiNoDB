namespace AvalonApiNoDB.Core.Domain.Rounds
{
    public enum RoundStatus
    {
        SelectingTeam = 0,
        VotingForTeam = 1,
        RevealTeamVote = 2,
        VotingExpedition = 3,
        MissionSuccess = 4,
        MissionFailed = 5,
        RoundEnded = -1
    }
}
