using System;
using System.Text.RegularExpressions;

namespace DataConnector
{
    class ArtistStyle
    {
        private int _artistId;
        private int _styleId;

        public int ArtistId => _artistId;
        public int StyleId => _styleId;

        public ArtistStyle()
        { }

        public ArtistStyle(string artistStyle)
        {
            string[] temp = Regex.Split(artistStyle, @"@@@");
            if (temp.Length != 0 && temp.Length >= 2)
            {
                _artistId = MyMusicBase.DataConnector.GetInt(temp[0]);
                _styleId = MyMusicBase.DataConnector.GetInt(temp[1]);
            }
            else throw new Exception("Can't create a instance ArtistStyle");
        }

        public ArtistStyle(int artistId, int styleId)
        {
            _artistId = artistId;
            _styleId = styleId;
        }
    }
}
