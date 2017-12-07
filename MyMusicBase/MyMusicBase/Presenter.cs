using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using DataConnector.Patterns;

namespace MyMusicBase
{
    public class Presenter
    {
        private MainWindow mainWindow;
        private StartPage startPageWindow;

        public Presenter(MainWindow window)
        {
            mainWindow = window;
            mainWindow.mainWindowEvent += MainWindow_mainWindowEvent;
        }

        public Presenter(StartPage startPage)
        {
            startPageWindow = startPage;
            startPageWindow.startPageEvent += StartPageWindow_startPageEvent;
        }

        private void MainWindow_mainWindowEvent(object sender, EventArgs e)
        {
            SqlConnection instance = new SqlConnection(DataConnector.SqlStringBuilder(mainWindow.UserName.Text, mainWindow.Pasword.Password).ConnectionString);
            try
            {
                instance.Open( );
                Window startPage = new StartPage( );
                instance.Close( );
                startPage.Show( );
                mainWindow.LoginWindow.Close( );
            }
            catch (Exception)
            {
                MessageBox.Show("Check you password or login");
            }
            finally
            {
                instance.Close( );
            }
        }

        private void StartPageWindow_startPageEvent(object sender, EventArgs e)
        {
            try
            {
                if (startPageWindow.SearchBox.Text != "")
                {
                    startPageWindow.ArtistText.Text = "";
                    startPageWindow.AlbumText.Text = "";
                    startPageWindow.StyleText.Text = "";
                    List<List<string>> list = new List<List<string>>( );
                    if (startPageWindow.ChoiseOfSearch.SelectedIndex == 00)
                    {
                        list = new Foreman(new ArtistBuilder( )).Construct(startPageWindow.SearchBox.Text);
                    }
                    else if (startPageWindow.ChoiseOfSearch.SelectedIndex == 1)
                    {
                        list = new Foreman(new StyleBuilder( )).Construct(startPageWindow.SearchBox.Text);
                    }
                    else if (startPageWindow.ChoiseOfSearch.SelectedIndex == 2)
                    {
                        list = new Foreman(new AlbumsBuilder( )).Construct(startPageWindow.SearchBox.Text);
                    }
                    if (list.Count != 0)
                        for (int i = 0; i < list.Count; i++)
                        {
                            for (int j = 0; j < list[i].Count; j++)
                            {
                                if (i == 0)
                                    startPageWindow.ArtistText.Text += list[i][j] + Environment.NewLine;
                                else if (i == 1)
                                    startPageWindow.StyleText.Text += list[i][j] + Environment.NewLine;
                                else if (i == 2)
                                    startPageWindow.AlbumText.Text += list[i][j] + Environment.NewLine;
                            }
                        }
                    else
                        MessageBox.Show("Мы не нашли, то что вы ищите в базе", "Сообщение", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
