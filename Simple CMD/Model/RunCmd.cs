using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_CMD.Model
{
	/// <summary>
	/// Error event handler
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void ErrorDataReceivedHandler(object sender, DataReceivedEventArgs e);

	/// <summary>
	/// Output event handler
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void OutputDataReceivedHandler(object sender, DataReceivedEventArgs e);

	/// <summary>
	/// Event handler when script has exited
	/// </summary>
	public delegate void ScriptExitedHandler();

	/// <summary>
	/// Simple class that can create and run cmd scripts
	/// <para>Implements <see cref="IRunCmd"/></para>
	/// </summary>
	public class RunCmd:IRunCmd
	{
		/*Global variables*/
		private Boolean _delete = false;
		private String _path = "";


		/* Events */

		/// <summary>
		/// Event used to send error data to form or another class
		/// </summary>
		public event ErrorDataReceivedHandler ErrorDataReceived;
		/// <summary>
		/// Event used to send output data to form or another class
		/// </summary>
		public event OutputDataReceivedHandler OutputDataReceived;

		/// <summary>
		/// Event that is triggered when the current script has been closed.
		/// <para>This indicates when it is safe to start another script with the same object</para>
		/// </summary>
		public event ScriptExitedHandler ScriptExited;




		/* Scripts */

		/// <summary>
		/// Executes a simple cmd script
		/// <para>Default values: path = Path.GetTempPath() + "myrun.bat", redirects all input and deletes file after usage</para>
		/// </summary>
		/// <param name="command"></param>
		/// <param name="path"></param>
		/// <param name="redirectInput"></param>
		/// <param name="redirectError"></param>
		/// <param name="redirectOutput"></param>
		/// <param name="deleteAfterExecute"></param>
		public void RunScript(String command, String path,Boolean redirectInput, Boolean redirectError, Boolean redirectOutput, Boolean deleteAfterExecute)
		{
			string testPath = path.Substring(path.Length - 4);
			if (testPath != ".bat")
			{
				throw new Exception("The file has to be a .bat file");
			}

			try
			{
				_delete = deleteAfterExecute;
				_path = path;
				File.WriteAllText(_path, command);

				ProcessStartInfo psi = new ProcessStartInfo("cmd.exe")
				{
					UseShellExecute = false,
					RedirectStandardInput = redirectInput,
					CreateNoWindow = true,
					RedirectStandardError = redirectError,
					RedirectStandardOutput = redirectOutput,
					Arguments = "/c" + _path
				};

				Process proc = new Process() { StartInfo = psi };
				proc.EnableRaisingEvents = true;

				// Set the data received handlers
				if (redirectError)
				{
					proc.ErrorDataReceived += Errors;
				}

				if (redirectOutput)
				{
					proc.OutputDataReceived += Output;
				}


				//Event that is triggered when process is exited
				proc.Exited += new EventHandler(ProcExited);

				//Starts process
				proc.Start();
				if (redirectError)
				{
					proc.BeginErrorReadLine();
				}

				if (redirectOutput)
				{
					proc.BeginOutputReadLine();
				}



			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}

		}

		/// <summary>
		/// Executes a simple cmd script
		/// <para>Default values: path = Path.GetTempPath() + "myrun.bat", redirects all input and deletes file after usage</para>
		/// </summary>
		/// <param name="command"></param>
		public void RunScript(String command)
		{
			RunScript(command, Path.GetTempPath() + "myrun.bat", true, true, true,true);
		}

		/// <summary>
		/// Executes a simple cmd script
		/// <para>Default values: path = Path.GetTempPath() + "myrun.bat", redirects all input and deletes file after usage</para>
		/// </summary>
		/// <param name="command"></param>
		/// <param name="deleteAfterExecute"></param>
		public void RunScript(String command, Boolean deleteAfterExecute)
		{
			RunScript(command, Path.GetTempPath() + "myrun.bat", true, true, true, deleteAfterExecute);
		}

		/// <summary>
		/// Executes a simple cmd script
		/// <para>Default values: path = Path.GetTempPath() + "myrun.bat", redirects all input and deletes file after usage</para>
		/// </summary>
		/// <param name="command"></param>
		/// <param name="redirectInput"></param>
		/// <param name="redirectError"></param>
		/// <param name="redirectOutput"></param>
		public void RunScript(String command, Boolean redirectInput, Boolean redirectError, Boolean redirectOutput)
		{
			RunScript(command, Path.GetTempPath() + "myrun.bat", redirectInput, redirectError, redirectOutput,true);
		}

		/// <summary>
		/// Executes a simple cmd script
		/// <para>Default values: path = Path.GetTempPath() + "myrun.bat", redirects all input and deletes file after usage</para>
		/// </summary>
		/// <param name="command"></param>
		/// <param name="redirectInput"></param>
		/// <param name="redirectError"></param>
		/// <param name="redirectOutput"></param>
		/// <param name="deleteAfterExecute"></param>
		public void RunScript(String command, Boolean redirectInput, Boolean redirectError, Boolean redirectOutput,
			Boolean deleteAfterExecute)
		{
			RunScript(command, Path.GetTempPath() + "myrun.bat", redirectInput, redirectError, redirectOutput, deleteAfterExecute);
		}




		/* Admin */


		/// <summary>
		/// Executes a simple cmd script with admin rights
		/// </summary>
		/// <param name="command"></param>
		/// <param name="path"></param>
		/// <param name="deleteAfterExecute"></param>
		public void RunScriptAdmin(String command, String path, Boolean deleteAfterExecute)
		{
			string testPath = path.Substring(path.Length - 4);
			if (testPath != ".bat")
			{
				throw new Exception("The file has to be a .bat file");
			}

			try
			{
				_delete = deleteAfterExecute;
				_path = path;
				File.WriteAllText(_path, command);

				ProcessStartInfo psi = new ProcessStartInfo("cmd.exe")
				{
					UseShellExecute = true,
					RedirectStandardInput = false,
					CreateNoWindow = false,
					RedirectStandardError = false,
					RedirectStandardOutput = false,
					Arguments = "/c" + _path
				};

				Process proc = new Process() { StartInfo = psi };
				proc.EnableRaisingEvents = true;

				//Event that is triggered when process is exited
				proc.Exited += new EventHandler(ProcExited);

				//Starts process
				proc.Start();


			}
			catch (Exception ex)
			{

				throw new Exception(ex.ToString());
			}
		}

		/// <summary>
		/// Executes a simple cmd script with admin rights
		/// <para>The path of this script is saved at %TEMP% and the .bat file is named myrun.bat</para>
		/// </summary>
		/// <param name="command"></param>
		public void RunScriptAdmin(string command)
		{
			RunScriptAdmin(command, Path.GetTempPath() + "myrun.bat", true);
		}

		/// <summary>
		/// Executes a simple cmd script with admin rights
		/// <para>The path of this script is saved at %TEMP% and the .bat file is named myrun.bat</para>
		/// </summary>
		/// <param name="command"></param>
		/// <param name="deleteAfterExecute"></param>
		public void RunScriptAdmin(string command, Boolean deleteAfterExecute)
		{
			RunScriptAdmin(command,Path.GetTempPath() + "myrun.bat",deleteAfterExecute);
		}



		/* Events that are triggered*/
		private void ProcExited(object sender, EventArgs e)
		{
			Process proc = (Process) sender;
			proc.Close();

			//Try and delete file after running process
			try
			{
				if (_delete)
				{
					if (File.Exists(_path))
					{
						File.Delete(_path);
					}
				}
			}
			catch (Exception ex)
			{
				
			}

			ScriptExited?.Invoke();
		}

		private void Errors(object sender, DataReceivedEventArgs e)
		{
			ErrorDataReceived?.Invoke(sender, e);
		}

		private void Output(object sender, DataReceivedEventArgs e)
		{
			OutputDataReceived?.Invoke(sender, e);
		}


	}
}
