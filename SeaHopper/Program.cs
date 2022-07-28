// See https://aka.ms/new-console-template for more information

var dir = Directory.GetCurrentDirectory();
SeaHopper.Compiler.Compiler compiler = new SeaHopper.Compiler.Compiler(dir, dir);
SeaHopper.Compiler.Compiler.Alert alert = (message) =>
{
	Console.WriteLine("\nAlert:\n" + message);
};

compiler.OnInfo = alert;
compiler.OnWarning = alert;
compiler.OnError = alert;
compiler.OnFatal = alert;

compiler.Compile();

Console.WriteLine("Successfully compiled");



