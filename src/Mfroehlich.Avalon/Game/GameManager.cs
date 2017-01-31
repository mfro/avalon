using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mfroehlich.Avalon.Game.Cards;
using Newtonsoft.Json.Linq;

namespace Mfroehlich.Avalon.Game
{
    public class GameManager
    {
        public List<Member> Members { get; } = new List<Member>();
        public List<Card> Required = new List<Card> {
            new Assassin(),
            new Merlin()
        };

        public List<OptionalCard> Optionals { get; } = new List<OptionalCard> {
            new OptionalCard(new Percival()),
            new OptionalCard(new Mordred()),
            new OptionalCard(new Oberon()),
            new OptionalCard(new Morgana())
        };

        public async Task AddSocket(Socket socket, string name)
        {
            var id = (Members.LastOrDefault()?.Id ?? 0) + 1;
            var member = new Member(id, name, socket);
            Members.Add(member);

            socket.Closed += async (s, e) => {
                Members.Remove(member);
                await SendMembers();
            };

            socket.Received += OnReceivedAsync;

            await socket.SendAsync("self", member);
            await SendMembers();
            await SendCards();
        }

        private async void OnReceivedAsync(object sender, JObject e)
        {
            var member = Members.Single(m => m.Socket == sender);

            var type = (string)e["type"];
            switch (type) {
                case "ready":
                    member.Ready = !member.Ready;
                    await SendMembers();
                    break;

                case "toggle-card":
                    var key = (string)e["body"];
                    var card = Optionals.SingleOrDefault(s => s.Card.Key == key);
                    if (card != null) {
                        card.Enabled = !card.Enabled;
                    }
                    await SendCards();
                    break;
            }
        }

        private async Task<bool> Seance()
        {
            var evil = (Members.Count + 2) / 3;
            var good = Members.Count - evil;

            var cards = Optionals.Where(c => c.Enabled).Select(c => c.Card).ToList();
            cards.AddRange(Required);
            evil -= cards.Count(c => c.Team == Team.Evil);
            good -= cards.Count(c => c.Team == Team.Good);

            if (evil < 0 || good < 0) {
                return false;
            }

            for (var i = 0; i < evil; i++) {
                cards.Add(new MinionOfMordred());
            }

            for (var i = 0; i < good; i++) {
                cards.Add(new LoyalServant());
            }

            var outcome = new Dictionary<Member, Card>();
            var random = new Random();

            foreach (var member in Members) {
                var card = cards[random.Next(cards.Count)];
                cards.Remove(card);
                outcome.Add(member, card);
            }

            foreach (var pair in outcome) {
                var known = outcome.Where(other => other.Key != pair.Key && pair.Value.CanSee(other.Value));
                await pair.Key.Socket.SendAsync("info", new
                {
                    card = pair.Value,
                    known = known.Select(other => other.Key.Name)
                });

                Members.Remove(pair.Key);
            }

            return true;
        }

        private async Task SendMembers()
        {
            if (Members.Count >= 5 && Members.All(m => m.Ready)) {
                if (await Seance())
                    return;
            }

            foreach (var member in Members) {
                await member.Socket.SendAsync("members", Members);
            }
        }

        private async Task SendCards()
        {
            foreach (var member in Members) {
                await member.Socket.SendAsync("cards", Optionals);
            }
        }
    }
}