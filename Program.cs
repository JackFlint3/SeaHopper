// See https://aka.ms/new-console-template for more information
using MCDatapackCompiler.Compiler.Lexer;
using MCDatapackCompiler.Compiler.Parser;
using MCDatapackCompiler.Compiler.Parser.Trees.Syntax;
using MCDatapackCompiler.Properties;

Console.WriteLine("Hello, World!");
List<StreamLexer.LexerToken> tokens = new List<StreamLexer.LexerToken>();
string attempt = @"
using minecraft;
using stat.mineBlock.minecraft;


namespace mydatapack {

	[load]
	/*
		Multiline Comment
	*/
	function hello_world_fun{
		// Comment
		as @e[type = player]
			at @s {
				say(" + "\"hello world\"" + @");
				say(" + "\"This is a DirtScript test\"" + @");
			}
	}

	[tick]
	function make_stuff{
		as @e[type = bat]
			say(" + "\"I am a Bat\"" + @");
	}
}
";

StreamParser parser = new StreamParser(attempt);
var expr = parser.Parse();

Console.WriteLine("Successfully parsed");
string str = expr.Build();
Console.WriteLine(str);


