namespace Mfroehlich.Avalon.Game.Cards
{
    public class Percival : Card
    {
        public override string Key => "percival";
        public override string Name => "Percival";

        public override Team Team => Team.Good;

        public override bool CanSee(Card other)
        {
            return other is Merlin || other is Morgana;
        }
    }
}