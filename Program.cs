namespace ChatApp2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Server.AcceptMsg();
				Console.WriteLine("Exiting the server");
			}
			else if (args.Length == 1)
			{
				Client.SendMsg(args[0]);
				Console.WriteLine("Exiting the client");
			}
		}
	}
}
