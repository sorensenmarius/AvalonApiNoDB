namespace AvalonApiNoDB.Core.Domain.Rounds
{
    public enum RoundStatus
    {
        SelectingTeam = 0,
        VotingForTeam = 1,
        RevealTeamVote = 2,
        VotingExpedition = 3,
        RevealExpeditionVote = 4,
        RoundEnded = -1
    }
}
