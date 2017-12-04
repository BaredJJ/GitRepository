using System;

namespace DataConnector
{
    class Albums
    {
        private int _artistId;
        private string _name;
        private DateTime _dateRelease;
        private static int _count;

        public int ArtistId => _artistId;
        public string Name => _name;
        public DateTime DateRelease => _dateRelease;
        public int Count => _count;

        public Albums()
        { }

        public Albums(int artistId, string name, DateTime dateRelease)
        {
            _artistId = artistId;
            _name = name;
            _dateRelease = dateRelease;
            ++_count;
        }
    }
}
