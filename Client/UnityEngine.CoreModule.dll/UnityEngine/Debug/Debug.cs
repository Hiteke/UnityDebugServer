using System.Net.Sockets;
using System.Text;

namespace UnityEngine
{
	public class Debug
	{
		static TcpClient tcpClient = new TcpClient();
		static NetworkStream stream;
		
		public static void Log(object message)
		{
			Debug.Send(LogType.Log, message);
		}

		public static void LogError(object message)
		{
			Debug.Send(LogType.Error, message);
		}

		public static void LogException(Exception exception)
		{
			Debug.Send(LogType.Exception, exception);
		}

		public static void LogWarning(object message)
		{
			Debug.Send(LogType.Warning, message);
		}

		public static void LogAssertion(object message)
		{
			Debug.Send(LogType.Assert, message);
		}

		public static void Send(LogType type, object message)
		{
			if (!Debug.tcpClient.Connected)
			{
				Debug.tcpClient.Connect("127.0.0.1", 7777);
				Debug.stream = Debug.tcpClient.GetStream();
				return;
			}
			byte[] bytes = Encoding.UTF8.GetBytes("[" + type.ToString() + "] - " + message.ToString());
			Debug.stream.Write(bytes, 0, bytes.Length);
		}
	}
}
