using System;

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
            MessageBox.Show(message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowExclametion(string exclamation)
        {
            MessageBox.Show(exclamation, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void ShowError(string error)
        {
            MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
