using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_CMD.Model
{
	interface IRunCmd
	{

		/* Events */

		/// <summary>
		/// Event used to send error data to form or another class
		/// </summary>
		event ErrorDataReceivedHandler ErrorDataReceived;
		/// <summary>
		/// Event used to send output data to form or another class
		/// </summary>
		event OutputDataReceivedHandler OutputDataReceived;

		/// <summary>
		/// Event that is triggered when the current script has been closed.
		/// <para>This indicates when it is safe to start another script with the same object</para>
		/// </summary>
		event ScriptExitedHandler ScriptExited;




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
		void RunScript(String command, String path, Boolean redirectInput, Boolean redirectError,
			Boolean redirectOutput, Boolean deleteAfterExecute);

		/// <summary>
		/// Executes a simple cmd script
		/// <para>Default values: path = Path.GetTempPath() + "myrun.bat", redirects all input and deletes file after usage</para>
		/// </summary>
		/// <param name="command"></param>
		void RunScript(String command);

		/// <summary>
		/// Executes a simple cmd script
		/// <para>Default values: path = Path.GetTempPath() + "myrun.bat", redirects all input and deletes file after usage</para>
		/// </summary>
		/// <param name="command"></param>
		/// <param name="deleteAfterExecute"></param>
		void RunScript(String command, Boolean deleteAfterExecute);

		/// <summary>
		/// Executes a simple cmd script
		/// <para>Default values: path = Path.GetTempPath() + "myrun.bat", redirects all input and deletes file after usage</para>
		/// </summary>
		/// <param name="command"></param>
		/// <param name="redirectInput"></param>
		/// <param name="redirectError"></param>
		/// <param name="redirectOutput"></param>
		void RunScript(String command, Boolean redirectInput, Boolean redirectError, Boolean redirectOutput);

		/// <summary>
		/// Executes a simple cmd script
		/// <para>Default values: path = Path.GetTempPath() + "myrun.bat", redirects all input and deletes file after usage</para>
		/// </summary>
		/// <param name="command"></param>
		/// <param name="redirectInput"></param>
		/// <param name="redirectError"></param>
		/// <param name="redirectOutput"></param>
		/// <param name="deleteAfterExecute"></param>
		void RunScript(String command, Boolean redirectInput, Boolean redirectError, Boolean redirectOutput,
			Boolean deleteAfterExecute);




		/* Admin */


		/// <summary>
		/// Executes a simple cmd script with admin rights
		/// </summary>
		/// <param name="command"></param>
		/// <param name="path"></param>
		/// <param name="deleteAfterExecute"></param>
		void RunScriptAdmin(String command, String path, Boolean deleteAfterExecute);

		/// <summary>
		/// Executes a simple cmd script with admin rights
		/// <para>The path of this script is saved at %TEMP% and the .bat file is named myrun.bat</para>
		/// </summary>
		/// <param name="command"></param>
		void RunScriptAdmin(String command);

		/// <summary>
		/// Executes a simple cmd script with admin rights
		/// <para>The path of this script is saved at %TEMP% and the .bat file is named myrun.bat</para>
		/// </summary>
		/// <param name="command"></param>
		/// <param name="deleteAfterExecute"></param>
		void RunScriptAdmin(String command, Boolean deleteAfterExecute);

	}
}
