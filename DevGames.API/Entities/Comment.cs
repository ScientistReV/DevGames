namespace DevGames.API.Entities
{
    public class Comment
    {
        public Comment(string title, string description, string user)
        {
            Title = title;
            Description = description;
            User = user;

            dateTime = DateTime.Now;
        }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public string User { get; private set; }

        public DateTime dateTime { get; private set; }

    }
}
