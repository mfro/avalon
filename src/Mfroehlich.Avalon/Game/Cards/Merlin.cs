namespace Mfroehlich.Avalon.Game.Cards
{
    public sealed class Merlin : Card
    {
        public override string Key => "merlin";
        public override string Name => "Merlin";

        public override Team Team => Team.Good;

        public override bool CanSee(Card other)
        {
            return other.Team == Team.Evil && !(other is Mordred);
        }
    }
}