using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using DataConnector.Patterns;

namespace MyMusicBase
{
    public class Presenter
    {
        private readonly MainWindow _mainWindow;
        private readonly StartPage _startPageWindow;
        private readonly AddArtist _addArtist;
        private readonly AddAlbums _addAlbums;
        private readonly AddStyle _addStyle;
        private readonly IMessageService _messageService = new MessageService();

        private static int _id;

        #region Конструкторы
        public Presenter(MainWindow window)
        {
            _mainWindow = window;
            _mainWindow.MainWindowEvent += MainWindow_mainWindowEvent;
        }

        public Presenter(StartPage startPage)
        {
            _startPageWindow = startPage;
            _startPageWindow.StartPageEvent += StartPageWindow_startPageEvent;
            _startPageWindow.AddEvent += _startPageWindow_AddEvent;
        }

        public Presenter(AddArtist addArtist)
        {
            _addArtist = addArtist;
            _addArtist.NewGroup += _addArtist_NewGroup;
        }

        public Presenter(AddAlbums addAlbums)
        {
            _addAlbums = addAlbums;
            _addAlbums.AddNewAlbum += _addAlbums_AddNewAlbum;
        }

        public Presenter(AddStyle addStyle)
        {
            _addStyle = addStyle;
            _addStyle.AddNewStyle += _addStyle_AddNewStyle;
        }
        #endregion

        #region Добавление в БД
        private void _addStyle_AddNewStyle(object sender, EventArgs e)//Добавление стилей исполнителя
        {
            if (_addStyle.StyleBox.Text != "")
            {
                string[] temp = Regex.Split(_addStyle.StyleBox.Text, @"\b[!,#,$,%,',(,),*,+,\.,/,:,;,<,=,>,?,@,[,\\,\],^,_,{,},|]+\s*|\b\s{2,}");
                AddDataBase.AddStyle(_id, temp);
                _addStyle.Close( );
            }
            else _messageService.ShowMessage("Вы не ввели ни одного стиля");
        }

        private void _addAlbums_AddNewAlbum(object sender, EventArgs e)//Добавление новых альбомов
        {
            if (_addAlbums.AlbumBox.Text != "" && _addAlbums.AlbumDateBox.Text != "")
            {
                try
                {
                    AddDataBase.AddAlbum(_addAlbums.AlbumBox.Text, _addAlbums.AlbumDateBox.Text, _id);
                    MessageBoxResult result = _messageService.ShowExclametion("Вы ввели все альбомы?");
                    _addAlbums.Close( );
                    if (result == MessageBoxResult.Yes)
                    {
                        Window style = new AddStyle( );
                        style.Show( );
                    }
                    else
                    {
                        Window albums = new AddAlbums( );
                        albums.Show( );
                    }
                }
                catch (Exception exception)
                {
                    _messageService.ShowError(exception.Message);
                }
            }
            else _messageService.ShowMessage("Вы не ввели обязательные данные");
        }

        private void _addArtist_NewGroup(object sender, EventArgs e)//Добавление нового артиста
        {
            if (_addArtist.ArtistBox.Text != "" && _addArtist.AppeareanceBox.Text != "")
            {
                try
                {
                    _id = AddDataBase.AddArtist(_addArtist.ArtistBox.Text, _addArtist.AppeareanceBox.Text,
                        _addArtist.BreackUpBox.Text);
                    if ( _id == -1)
                    {
                        _messageService.ShowMessage("Такой исполнитель уже есть в базе");
                        _addArtist.ArtistBox.Clear();
                        _addArtist.AppeareanceBox.Clear();
                        _addArtist.BreackUpBox.Clear();

                    }
                    else
                    {
                        _messageService.ShowMessage("Исолнитель успешно добвален в базу");
                        _addArtist.Close( );
                        Window albums = new AddAlbums( );
                        albums.Show( );
                    }
                }
                catch (Exception exception)
                {
                    _messageService.ShowError(exception.Message);
                }
            }
            else _messageService.ShowMessage("Вы не ввели обязательные данные");
        }
        #endregion

        #region MainWindow
        private void MainWindow_mainWindowEvent(object sender, EventArgs e)
        {
            
            SqlConnection instance = new SqlConnection(DataConnector.SqlStringBuilder(_mainWindow.UserName.Text, _mainWindow.Pasword.Password).ConnectionString);
            try
            {
                instance.Open( );
                Window startPage = new StartPage( );
                _mainWindow.LoginWindow.Close( );
                //instance.Close( );
                startPage.Show( );
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
        #endregion

        #region SatrtPage
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

        private void _startPageWindow_AddEvent(object sender, EventArgs e)
        {
            Window artist = new AddArtist( );
            artist.Show( );
        }
#endregion
    }
}
