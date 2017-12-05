﻿using System.Collections.Generic;

namespace DataConnector.Patterns
{
    public class StyleBuilder:Builder
    {
        private List<List<string>> _data;

        private static Style BuildStyles(string name )
        {
            string style = MyMusicBase.DataConnector.GetString("SELECT * FROM Style WHERE Name = '" + name + "'");
            if (style != "")
             return new Style();
            return new Style();
        }

        private static List<int> BuildStyleList(int styleId )
        {
            List<string> list =
                MyMusicBase.DataConnector.GetList("SELECT * FROM ArtistStyle WHERE StyleId = '" + styleId + "'");
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
                    MyMusicBase.DataConnector.GetString("SELECT * FROM Artist WHERE ArtistId = '" + artistId[i] + "'");
                artist.Add(new Artist(temp));
            }
            return artist;
        }

        public override List<List<string>> Create(string option)
        {
            _data = new List<List<string>>( );
            Style style = BuildStyles(option );
            if (style.Name != null)
            {
                List<Artist> artist = BuildArtist(BuildStyleList(style.StyleId));

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
            }
            return _data;
        }
    }
}
