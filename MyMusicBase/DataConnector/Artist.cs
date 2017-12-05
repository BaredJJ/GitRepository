using System;
using System.Text.RegularExpressions;

namespace DataConnector
{
    class Artist
    {
        private readonly int _id;
        private readonly string _name;
        private readonly DateTime _appearance;
        private readonly DateTime _breackUp;

        public int Id => _id;
        public string Name => _name;
        public DateTime Appearance => _appearance;
        public DateTime BreackUp => _breackUp;

        public Artist()
        { }

        public Artist(string artist)
        {
            string[] result = Regex.Split(artist, @"@@@");
            if (result.Length != 0 && result.Length >= 3)
            {
                _id = MyMusicBase.DataConnector.GetInt(result[0]);
                _name = result[1];
                _appearance = MyMusicBase.DataConnector.GetData(result[2]);
                if (result.Length > 4)
                    _breackUp = MyMusicBase.DataConnector.GetData(result[3]);
                else _appearance = new DateTime(0,0,0);
            }
            else throw new Exception("Can't create instance Artist");
        }

        public Artist(int id, string name, DateTime appearance)
        {
            _id = id;
            _name = name;
            _appearance = appearance;
        }

        public Artist(int id, string name, DateTime appearance, DateTime breackUp):this(id,name,appearance)
        {
            _breackUp = breackUp;
        }
    }
}
