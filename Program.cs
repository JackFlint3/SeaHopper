// See https://aka.ms/new-console-template for more information

var dir = Directory.GetCurrentDirectory();
MCDatapackCompiler.Compiler.Compiler compiler = new MCDatapackCompiler.Compiler.Compiler("H:/Documents/VS/repos/MCDatapackCompiler/Properties/Resources", dir);
MCDatapackCompiler.Compiler.Compiler.Alert alert = (message) =>
{
	Console.WriteLine("\nAlert:\n" + message);
};

compiler.OnInfo = alert;
compiler.OnWarning = alert;
compiler.OnError = alert;
compiler.OnFatal = alert;

compiler.Compile();

Console.WriteLine("Successfully compiled");



