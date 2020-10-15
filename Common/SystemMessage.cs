
namespace Common
{
    public static class SystemMessage
    {
        public static string ServerIsRunning()
        { return "The server is running. Waiting for connections..."; }
        public static string Update()
        { return "Update"; }
        public static string NoConnection()
        { return "No connection to the server!"; }
        public static string ConnectionToEstablished()
        { return "Connection to the server is established!"; }
    }
}
