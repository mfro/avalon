using Mfroehlich.Avalon.Game.Cards;

namespace Mfroehlich.Avalon.Game
{
    public enum Team
    {
        Evil,
        Good,
    }

    public abstract class Card
    {
        public abstract string Key { get; }
        public abstract string Name { get; }

        public abstract Team Team { get; }

        public abstract bool CanSee(Card other);

        public static readonly Card
            Assassin = new Assassin(),
            Merlin = new Merlin(),

            Morgana = new Morgana(),
            Mordred = new Mordred(),
            Oberon = new Oberon(),
            Percival = new Percival();
    }
}