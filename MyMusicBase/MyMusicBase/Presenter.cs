using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using DataConnector.Patterns;

namespace MyMusicBase
{
    public class Presenter
    {
        private readonly MainWindow _mainWindow;
        private readonly StartPage _startPageWindow;
        private readonly IMessageService _messageService;

        public Presenter(MainWindow window, IMessageService message)
        {
            _mainWindow = window;
            _mainWindow.mainWindowEvent += MainWindow_mainWindowEvent;
            _messageService = message;
        }

        public Presenter(StartPage startPage, IMessageService message)
        {
            _startPageWindow = startPage;
            _startPageWindow.startPageEvent += StartPageWindow_startPageEvent;
            _messageService = message;
        }

        private void MainWindow_mainWindowEvent(object sender, EventArgs e)
        {
            SqlConnection instance = new SqlConnection(DataConnector.SqlStringBuilder(_mainWindow.UserName.Text, _mainWindow.Pasword.Password).ConnectionString);
            try
            {
                instance.Open( );
                Window startPage = new StartPage( );
                instance.Close( );
                startPage.Show( );
                _mainWindow.LoginWindow.Close( );
            }
            catch (Exception)
            {
                _messageService.ShowError("Проверьте имя пользлвателя и пароль. Или возможно отсудствует доступ к базе данных.");
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
                if (_startPageWindow.SearchBox.Text != "")
                {
                    _startPageWindow.ArtistText.Text = "";
                    _startPageWindow.AlbumText.Text = "";
                    _startPageWindow.StyleText.Text = "";
                    List<List<string>> list = new List<List<string>>( );
                    if (_startPageWindow.ChoiseOfSearch.SelectedIndex == 00)
                    {
                        list = new Foreman(new ArtistBuilder( )).Construct(_startPageWindow.SearchBox.Text);
                    }
                    else if (_startPageWindow.ChoiseOfSearch.SelectedIndex == 1)
                    {
                        list = new Foreman(new StyleBuilder( )).Construct(_startPageWindow.SearchBox.Text);
                    }
                    else if (_startPageWindow.ChoiseOfSearch.SelectedIndex == 2)
                    {
                        list = new Foreman(new AlbumsBuilder( )).Construct(_startPageWindow.SearchBox.Text);
                    }
                    if (list.Count != 0)
                        for (int i = 0; i < list.Count; i++)
                        {
                            for (int j = 0; j < list[i].Count; j++)
                            {
                                if (i == 0)
                                    _startPageWindow.ArtistText.Text += list[i][j] + Environment.NewLine;
                                else if (i == 1)
                                    _startPageWindow.StyleText.Text += list[i][j] + Environment.NewLine;
                                else if (i == 2)
                                    _startPageWindow.AlbumText.Text += list[i][j] + Environment.NewLine;
                            }
                        }
                    else
                        _messageService.ShowMessage("Мы не нашли, то что вы ищите в базе");
                }

            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }
    }
}
