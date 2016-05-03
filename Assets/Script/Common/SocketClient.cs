using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

public class SocketClient : MonoBehaviour {
    private string Address = "192.168.1.1";
    private int Port = 8088;
    private IPAddress Ip;
    private EndPoint EP;
    private byte[] Result = new byte[1024];

    void Start() {
        Ip = IPAddress.Parse(Address);
        EP = new IPEndPoint(Ip, Port);
    }
    private void ConnectServer() {
        Socket clientSocket = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(EP);
            ShowMessage("连接成功");
        }
        catch {
            ShowMessage("连接失败");
            return;
        }
       
    }
    //异步接受服务器消息
    private void Receive(Socket clientSocket) {
        //接受服务器消息
        int receive = clientSocket.Receive(Result);
        string message = Encoding.ASCII.GetString(Result, 0, receive);
        ShowMessage(message);
    }
    private void Send(Socket clientSocket) {
        //通过 clientSocket 发送数据  
        for (int i = 0; i < 10; i++)
        {
            try
            {
                Thread.Sleep(1000);    //等待1秒钟  
                string sendMessage = "client send Message Hellp" + System.DateTime.Now;
                clientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
            }
            catch
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                break;
            }
        }
    }
    private void ShowMessage(string message) {
        print(message);
    }

}
