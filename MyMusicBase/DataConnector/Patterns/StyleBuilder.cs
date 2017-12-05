using System;
using System.Collections.Generic;

namespace DataConnector.Patterns
{
    public class StyleBuilder:Builder
    {
        private List<List<string>> _data = null;

        private static Style BuildStyles(string name ) 
            => new Style(MyMusicBase.DataConnector.GetString1("SELECT * FROM Style WHERE Name = '" + name + "'"));

        private static List<int> BuildStyleList(int styleId )
        {
            List<string> list =
                MyMusicBase.DataConnector.GetList1("SELECT * FROM ArtistStyle WHERE StyleId = '" + styleId + "'");
            List<int> artistId = new List<int>();
            for (int i = 0; i < list.Count; ++i)
            {
                artistId.Add(new ArtistStyle(list[i]).ArtistId);
            }
            return artistId;
        }

        private static List<Artist> BuildArtist(List<int> artistId)
        {
            List<Artist> artist = new List<Artist>();
            for (int i = 0; i < artistId.Count; ++i)
            {
                string temp =
                    MyMusicBase.DataConnector.GetString1("SELECT * FROM Artist WHERE ArtistId = '" + artistId[i] + "'");
                artist.Add(new Artist(temp));
            }
            return artist;
        }

        public override List<List<string>> Create(string option)
        {
            _data = new List<List<string>>( );
            Style style = BuildStyles(option );
            List<Artist> artist = BuildArtist(BuildStyleList(style.StyleId ));

            List<string> artList = new List<string>();
            for (int i = 0; i < artist.Count; ++i)
            {
                string artistString = artist[i].Name + " " + artist[i].Appearance.Year;
                if (artist[i].BreackUp.Year != 0)
                    artistString += " - " + artist[i].BreackUp.Year;
                artList.Add(artistString);
            }
            List<string> styleList = new List<string>();
            styleList.Add(style.Name);
            _data.Add(artList);
            _data.Add(styleList);
            return _data;
        }
    }
}
