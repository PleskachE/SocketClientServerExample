using System.Collections.Generic;

namespace Common
{
    public class SystemMessage
    {
        public List<string> SystemMessages { get; set; }
        public SystemMessage()
        {
            this.SystemMessages = new List<string>()
            {
                "Сервер запущен. Ожидание подключений...",
                "Update",
                "Нет соединения с сервером!",
                "Подключение с сервером установлено!"
            };
        }
    }
}
