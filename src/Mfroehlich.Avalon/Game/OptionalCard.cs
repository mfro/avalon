namespace Mfroehlich.Avalon.Game
{
    public class OptionalCard
    {
        public Card Card { get; }
        public bool Enabled { get; set; }

        public OptionalCard(Card card)
        {
            Card = card;
        }
    }
}