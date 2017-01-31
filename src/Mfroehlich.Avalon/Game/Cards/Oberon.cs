namespace Mfroehlich.Avalon.Game.Cards
{
    public class Oberon : MinionOfMordred
    {
        public override string Key => "oberon";
        public override string Name => "Oberon";

        public override bool CanSee(Card other) => false;
    }
}