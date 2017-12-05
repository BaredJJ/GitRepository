using System.Collections.Generic;

namespace DataConnector.Patterns
{
    public class ArtistBuilder:Builder
    {
        private List<List<string>> _data;

        private static Artist BuildArtist(string name)
        {
            string artist = MyMusicBase.DataConnector.GetString("SELECT * FROM Artist WHERE NAME = '" + name + "'");
            if (artist != "")
            {
                return new Artist(artist );
            }
            return new Artist();
        }

        private static List<Albums> BuildAlbumses( int artistId )
        {
            List<string> albums = MyMusicBase.DataConnector.GetList("SELECT * FROM Albums WHERE ArtistId = '" + artistId + "'");
            List<Albums> albumsList = new List<Albums>(albums.Count);
            for (int i = 0; i < albums.Count; ++i)
            {
                albumsList.Add(new Albums(albums[i]));
            }
            return albumsList;
        }

        private static List<ArtistStyle> BuildStyleList(int artistId)
        {
            List<string> artistStyle =
                MyMusicBase.DataConnector.GetList("SELECT * FROM ArtistStyle WHERE ArtistId = '" + artistId + "'");
            List<ArtistStyle> artistStyleList = new List<ArtistStyle>();
            for (int i = 0; i < artistStyle.Count; ++i)
            {
                artistStyleList.Add(new ArtistStyle(artistStyle[i]));
            }
            return artistStyleList;
        }

        private static List<string> BuildStyles(List<ArtistStyle> artistStyles)
        {
            List<string> styleList = new List<string>();
            for (int i = 0; i < artistStyles.Count; ++i)
            {
               styleList.Add(new Style(MyMusicBase.DataConnector.GetString("SELECT * FROM Style WHERE StyleId = '" + artistStyles[i].StyleId + "'")).Name);
            }
            return styleList;
        }

        public override List<List<string>> Create(string artist)
        {
            _data = new List<List<string>>();
            Artist name = BuildArtist(artist);
            if (name.Name != null)
            {
                List<Albums> albums = BuildAlbumses(name.Id);
                List<string> style = BuildStyles(BuildStyleList(name.Id));

                string artString = name.Name + " " + name.Appearance.Year;
                if (name.BreackUp.Year != 0)
                    artString += " - " + name.BreackUp.Year;
                List<string> artList = new List<string>();
                artList.Add(artString);

                List<string> albList = new List<string>();
                for (int i = 0; i < albums.Count; ++i)
                {
                    string temp = albums[i].Name + " " + albums[i].DateRelease.Year;
                    albList.Add(temp);
                }
                _data.Add(artList);
                _data.Add(style);
                _data.Add(albList);
            }

            return _data;
        }
    }
}
