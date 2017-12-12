using System;
using System.Text.RegularExpressions;

namespace DataConnector
{
    public class Albums
    {
        private int _artistId;
        private string _name;
        private DateTime _dateRelease;

        public int ArtistId => _artistId;
        public string Name => _name;
        public DateTime DateRelease => _dateRelease;

        public Albums()
        { }

        public Albums(int artistId, string name, DateTime dateRelease)
        {
            _artistId = artistId;
            _name = name;
            _dateRelease = dateRelease;
        }

        public Albums(string albums)
        {
            string[] temp = Regex.Split(albums, @"@@@");
            if (temp.Length != 0 && temp.Length >= 4)
            {
                _artistId = MyMusicBase.DataConnector.GetInt(temp[1]);
                _name = temp[2];
                _dateRelease = MyMusicBase.DataConnector.GetData(temp[3]);
            }
            else throw new Exception("Can't create instance Albums");
        }
    }
}
