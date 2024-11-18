using System;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main()
    {
        // Configuración del cliente
        string serverIP = "127.0.0.1";
        int port = 5000;

        try
        {
            // Crear una conexión con el servidor
            TcpClient client = new TcpClient(serverIP, port);
            Console.WriteLine("Conectado al servidor.");

            // Enviar un mensaje al servidor
            NetworkStream stream = client.GetStream();
            string message = "Hola desde el cliente.";
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
            Console.WriteLine($"Mensaje enviado: {message}");

            // Leer respuesta del servidor
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Respuesta del servidor: {response}");

            // Cerrar conexión
            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
