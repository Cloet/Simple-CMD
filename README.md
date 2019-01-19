# Simple-CMD
Simple library that creates and runs cmd scripts

## TODO
- Add some proper tests.

## Usage
```C#

//Initialize object
RunCmd rcmd = new RunCmd();

//Bind events to methods
rcmd.ErrorDataReceived += new ErrorDataReceivedHandler(Errors);
rcmd.OutputDataReceived += new OutputDataReceivedHandler(Output);
rcmd.ScriptExited += new ScriptExitedHandler(ScriptExited);

//Only needs the command to run. But more options are possible
rcmd.RunScript("@Echo off \n ipconfig");

//These Methods are bound with events
private static void ScriptExited()
{
	
}
private static void Errors(object sender, DataReceivedEventArgs e)
{
	Console.WriteLine(e.Data);
}
private static void Output(object sender, DataReceivedEventArgs e)
{
	Console.WriteLine(e.Data);
}

```

```C#
/* Runs the scripts with normal privileges */

//Default values for booleans are true and the default path is %TEMP% + "myrun.bat"
void RunScript(String command, String path, Boolean redirectInput, Boolean redirectError,Boolean redirectOutput, Boolean deleteAfterExecute);
void RunScript(String command);
void RunScript(String command, Boolean deleteAfterExecute);
void RunScript(String command, Boolean redirectInput, Boolean redirectError, Boolean redirectOutput);
void RunScript(String command, Boolean redirectInput, Boolean redirectError, Boolean redirectOutput,Boolean deleteAfterExecute);

/* Runs the scripts with Admin privilege */

//Default path is %TEMP% + "myrun.bat" and deletes the file after executing on default.
void RunScriptAdmin(String command, String path, Boolean deleteAfterExecute);
void RunScriptAdmin(String command);
void RunScriptAdmin(String command, Boolean deleteAfterExecute);
```
