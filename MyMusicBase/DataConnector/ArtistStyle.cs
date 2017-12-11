using System;
using System.Text.RegularExpressions;

namespace DataConnector
{
    class ArtistStyle
    {
        public int ArtistId { get; set; }

        public int StyleId { get; set; }

        public ArtistStyle()
        { }

        public ArtistStyle(int artistId)
        {
            ArtistId = artistId;
        }

        public ArtistStyle(string artistStyle)
        {
            string[] temp = Regex.Split(artistStyle, @"@@@");
            if (temp.Length != 0 && temp.Length >= 2)
            {
                ArtistId = MyMusicBase.DataConnector.GetInt(temp[0]);
                StyleId = MyMusicBase.DataConnector.GetInt(temp[1]);
            }
            else throw new Exception("Can't create a instance ArtistStyle");
        }

        public ArtistStyle(int artistId, int styleId):this(artistId)
        {
            StyleId = styleId;
        }
    }
}
