using System;

namespace DataConnector
{
    class Artist
    {
        private readonly int _id;
        private readonly string _name;
        private readonly DateTime _appearance;
        private readonly DateTime _breackUp;
        private static int count;

        public int Id => _id;
        public string Name => _name;
        public DateTime Appearance => _appearance;
        public DateTime BreackUp => _breackUp;
        public int Count => count;

        public Artist()
        { }

        public Artist(int id, string name, DateTime appearance)
        {
            _id = id;
            _name = name;
            _appearance = appearance;
            ++count;
        }

        public Artist(int id, string name, DateTime appearance, DateTime breackUp):this(id,name,appearance)
        {
            _breackUp = breackUp;
        }
    }
}
