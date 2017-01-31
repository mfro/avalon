namespace Mfroehlich.Avalon.Game
{
    public class Member
    {
        public int Id { get; }
        public string Name { get; }
        internal Socket Socket { get; }

        public bool Ready { get; set; }
        
        public Member(int id, string name, Socket socket) {
            Id = id;
            Name = name;
            Socket = socket;
        }
    }
}