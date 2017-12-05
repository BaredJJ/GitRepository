﻿using System.Windows;


namespace MyMusicBase
{
    public interface IMessageService
    {
        void ShowMessage(string message);
        void ShowExclametion(string exclametion);
        void ShowError(string error);
    }

    class MessageService : IMessageService
    {
        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowExclametion(string exclamation)
        {
            MessageBox.Show(exclamation, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public void ShowError(string error)
        {
            MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
