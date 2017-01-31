namespace Mfroehlich.Avalon.Game.Cards
{
    public sealed class LoyalServant : Card
    {
        public override string Key => "servant";
        public override string Name => "A Loyal Servant of Arthur";

        public override Team Team => Team.Good;

        public override bool CanSee(Card other) => false;
    }
}