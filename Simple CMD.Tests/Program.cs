using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Simple_CMD.Model;

namespace Simple_CMD.Tests
{
	class Program
	{
		private static Boolean _running = false;
		private static RunCmd rcmd = new RunCmd();
		static void Main(string[] args)
		{
			rcmd.ErrorDataReceived += new ErrorDataReceivedHandler(Errors);
			rcmd.OutputDataReceived += new OutputDataReceivedHandler(Output);
			rcmd.ScriptExited += new ScriptExitedHandler(ScriptExited);

			rcmd.RunScript("@Echo off \n ipconfig", true, true, true, false);
			_running = true;

			while (_running)
			{
				//
			}

			Console.WriteLine("Press enter to continue.");
			Console.ReadLine();
			Console.Clear();

			rcmd.RunScriptAdmin("@Echo off \n ipconfig \n PAUSE");
			_running = true;

			while (_running)
			{
				//
			}

			Console.WriteLine("Press enter to continue.");
			Console.ReadLine();
			Console.Clear();

			rcmd.RunScript("ipconfig", false, false, false, false);

			Console.ReadLine();


		}

		/* These methods are triggered by the corresponding events */
		private static void ScriptExited()
		{
			_running = false;
		}

		private static void Errors(object sender, DataReceivedEventArgs e)
		{
			Console.WriteLine(e.Data);
		}

		private static void Output(object sender, DataReceivedEventArgs e)
		{
			Console.WriteLine(e.Data);
		}

	}
}
