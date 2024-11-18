using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main()
    {
        // Configuración del servidor
        int port = 5000;
        TcpListener server = null;

        try
        {
            // Crear un TcpListener en el puerto especificado
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Console.WriteLine("Servidor en espera de conexiones...");

            while (true)
            {
                // Aceptar conexión del cliente
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Cliente conectado.");

                // Leer datos del cliente
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Mensaje recibido del cliente: {message}");

                // Enviar respuesta al cliente
                string response = "Mensaje recibido por el servidor.";
                byte[] responseData = Encoding.UTF8.GetBytes(response);
                stream.Write(responseData, 0, responseData.Length);

                // Cerrar conexión
                client.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            server?.Stop();
        }
    }
}

