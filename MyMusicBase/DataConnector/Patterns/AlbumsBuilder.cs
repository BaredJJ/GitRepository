﻿using System.Collections.Generic;

namespace DataConnector.Patterns
{
    public class AlbumsBuilder:Builder
    {
        private List<List<string>> _data;

        private static Albums BuildAlbumses(string name)
        {
            string album = MyMusicBase.DataConnector.GetString("SELECT * FROM Albums WHERE NAME = '" + name + "'");
            if (album != "")
            {
                return new Albums(album);
            }
            return new Albums();
        }

        private static Artist BuildArtist( int artistId ) 
            => new Artist(MyMusicBase.DataConnector.GetString("SELECT * FROM Artist WHERE ArtistId = '" + artistId + "'"));

        private static List<ArtistStyle> BuildStyleList( int artistId )
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

        public override List<List<string>> Create(string option)
        {
            _data = new List<List<string>>( );
            Albums albums = BuildAlbumses( option);
            if (albums.Name != null)
            {
                Artist artist = BuildArtist(albums.ArtistId);
                List<string> style = BuildStyles(BuildStyleList(albums.ArtistId));

                string artistString = artist.Name + " " + artist.Appearance.Year;
                if (artist.BreackUp.Year != 0)
                    artistString += " - " + artist.BreackUp.Year;
                string albumsString = albums.Name + " " + albums.DateRelease.Year;

                List<string> artList = new List<string>();
                artList.Add(artistString);
                List<string> albList = new List<string>();
                albList.Add(albumsString);
                _data.Add(artList);
                _data.Add(style);
                _data.Add(albList);
            }

            return _data;
        }
    }
}
