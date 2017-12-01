using System.Collections.Generic;
using System.Text;
using System.Windows;


namespace MyMusicBase
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage
    {
        public StartPage( )
        {
            InitializeComponent( );
        }

        private void Search_OnClick(object sender, RoutedEventArgs e)
        {
            if (ChoiseOfSearch.SelectedIndex == 00)
            {
                AlbumText.Text = "";
                ArtistText.Text = "";
                StyleText.Text = "";
                string searchString = "SELECT * FROM Artist WHERE Name = '" + SearchBox.Text + "'";
                int temp = DataConnector.GetId("ArtistId", searchString);

                GetArtistText(temp );
                GetAlbumsText(temp);
                GetStyleText(GetId(temp, "StyleId", "ArtistId"));
            }
            else if (ChoiseOfSearch.SelectedIndex == 1)
            {
                AlbumText.Text = "";
                ArtistText.Text = "";
                StyleText.Text = "";
                string searchString = "SELECT * FROM Style WHERE Name ='" + SearchBox.Text + "'";
                int temp = DataConnector.GetId("StyleId", searchString);

                StyleText.Text = DataConnector
                    .GetScalar(string.Format("SELECT Name FROM Style WHERE Name ='" + SearchBox.Text + "'")).ToString();

                List<int> styleId = GetId(temp, "ArtistId", "StyleId");
                foreach (var i in styleId)
                {
                    GetArtistText(i);
                }
                AlbumText.Text = "";
            }
            else if (ChoiseOfSearch.SelectedIndex == 2)
            {
                AlbumText.Text = "";
                ArtistText.Text = "";
                StyleText.Text = "";
                string searchString = "SELECT * FROM Albums WHERE Name ='" + SearchBox.Text + "'";
                int temp = DataConnector.GetId("ArtistId", searchString);

                searchString = "SELECT Name FROM Albums WHERE Name ='" + SearchBox.Text + "'";
                string str = DataConnector.GetScalar(searchString).ToString();
                AlbumText.Text = str;

                GetArtistText(temp);
                GetStyleText(GetId(temp, "StyleId", "ArtistId"));
            }
 
        }

        private void GetArtistText(int temp)
        {
            StringBuilder str = new StringBuilder( );
            string searchString = "SELECT Name FROM Artist WHERE ArtistId = '" + temp + "'";
            str.AppendFormat(DataConnector.GetString(searchString));
            ArtistText.Text = str.ToString( );
        }

        private void GetAlbumsText( int temp)
        {
            StringBuilder str = new StringBuilder( );
            string searchString = "SELECT Name, DateRelease FROM Albums WHERE ArtistId = '" + temp + "'";
            str.AppendFormat(DataConnector.GetString(searchString));
            AlbumText.Text = str.ToString( );
        }

        private static List<int> GetId(int temp, string chouse, string from)
        {
            StringBuilder str = new StringBuilder( );
            string searchString = "SELECT " + chouse + " FROM ArtistStyle WHERE " + from + " = '" + temp + "'";
            List<int> styleId = DataConnector.GetList(searchString);
            return styleId;
        }

        private void GetStyleText( List<int> styleId)
        {
            StringBuilder str = new StringBuilder();
            foreach (var i in styleId)
            {
                string searchString = "SELECT Name FROM Style WHERE StyleId = '" + i + "'";
                str.AppendFormat(DataConnector.GetScalar(searchString).ToString( ));
            }
            StyleText.Text = str.ToString( );
        }
    }
}
