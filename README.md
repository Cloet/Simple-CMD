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

//Executes a simple command, more options are possible
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
