﻿using System;
using System.Windows;

namespace MyMusicBase
{
    /// <summary>
    /// Логика взаимодействия для AddStyle.xaml
    /// </summary>
    public partial class AddStyle : Window
    {
        public AddStyle( )
        {
            InitializeComponent( );
        }

        public event EventHandler AddNewStyle;

        private void StyleButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddNewStyle?.Invoke(sender, e);
        }
    }
}
