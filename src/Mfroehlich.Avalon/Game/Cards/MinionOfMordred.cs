namespace Mfroehlich.Avalon.Game.Cards
{
    public class MinionOfMordred : Card
    {
        public override string Key => "minion";
        public override string Name => "A Minion of Mordred";

        public override Team Team => Team.Evil;

        public override bool CanSee(Card other)
        {
            return other is MinionOfMordred && !(other is Oberon);
        }
    }
}