using JsonServerOpg;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

Console.WriteLine("TCP server");

TcpListener listener = new TcpListener(IPAddress.Any, 7);

listener.Start();

while (true)
{


    TcpClient socket = listener.AcceptTcpClient();
    IPEndPoint clientEndPoint = socket.Client.RemoteEndPoint as IPEndPoint;
    Console.WriteLine("Client connected: " + clientEndPoint.Address);


    Task.Run(() => HandleClient(socket));


}

void HandleClient(TcpClient socket)
{

    NetworkStream ns = socket.GetStream();
    StreamReader reader = new StreamReader(ns);
    StreamWriter writer = new StreamWriter(ns);

    while (socket.Connected)
    {
        Random random = new Random();
        string? msg = reader.ReadLine();
        Input? jsonMsg = null;
        Console.WriteLine("test: " + msg);

        if (msg.ToLower() == "stop")
        {
            writer.WriteLine("Server has been stopped");
        }
        else
        {
            try
            {
                jsonMsg = JsonSerializer.Deserialize<Input>(msg);
            }
            catch (Exception ex)
            {
                writer.WriteLine("Invalid input");
            }

            if (jsonMsg != null)
            {
                if (jsonMsg.Method.StartsWith("add"))
                {
                    int sum = jsonMsg.Num1 + jsonMsg.Num2;
                    string resultMessage = "Result: " + sum;
                    Console.WriteLine(resultMessage);
                    writer.WriteLine(resultMessage);
                    writer.Flush();

                }
                if (jsonMsg.Method.StartsWith("subtract"))
                {
                    int sum = jsonMsg.Num1 - jsonMsg.Num2;
                    string resultMessage = "Result: " + sum;
                    Console.WriteLine(resultMessage);
                    writer.WriteLine(resultMessage);
                    writer.Flush();
                }
            }
            if (jsonMsg.Method.StartsWith("random"))
            {
                int randomNum = random.Next(jsonMsg.Num1, jsonMsg.Num2);
                string number = "" + randomNum;
                Console.WriteLine(number);
                writer.WriteLine(number);
                writer.Flush();
            }
        }
    }
}