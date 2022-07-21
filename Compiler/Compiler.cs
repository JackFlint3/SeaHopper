using MCDatapackCompiler.Compiler.Trees.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDatapackCompiler.Compiler
{
    internal class Compiler
    {


        public delegate void Alert(string message);

        public Alert OnInfo = (x) => { };
        public Alert OnError = (x) => { };
        public Alert OnWarning = (x) => { };
        public Alert OnFatal = (x) => { };

        public string? InputOverride = null;
        readonly string fromProject;
        readonly string destination;

        public string FromProject => fromProject;

        public string Destination => destination;

        public Compiler(string fromProject, string destination)
        {
            this.fromProject = fromProject ?? throw new ArgumentNullException(nameof(fromProject));
            this.destination = destination ?? throw new ArgumentNullException(nameof(destination));
        }

        public void Compile()
        {
            DirectoryInfo directory = new DirectoryInfo(FromProject);
            if (!directory.Exists) throw new Exception();


            Builder.Context.BuildContext buildContext = new(Destination);
            // TODO: Load Datapack name, description and version here


            Parser.StreamParser parser;

            if (InputOverride != null)
            {
                parser = new Parser.StreamParser(InputOverride);
                buildContext.CurrentFile = "InputOverride";
                buildContext.Data["prefix"] = null;
                Expression? ast;
                ast = parser.Parse();

                OnInfo("Parsed " + buildContext.CurrentFile);

                // Prebuilding
                ast.Build(buildContext);

                OnInfo("Prebuilt " + buildContext.CurrentFile);
            }
            else
                foreach (var file in directory.GetFiles())
                {
                    if (file.Extension == ".ch")
                    {
                        string fileText;
                        try
                        {
                            fileText = File.ReadAllText(file.FullName);
                        }
                        catch (Exception ex)
                        {
                            OnError("Failed to read file '" + file.FullName + "'");
                            continue;
                        }

                        buildContext.CurrentFile = file.FullName;
                        buildContext.Data["prefix"] = null;
                        parser = new Parser.StreamParser(fileText);
                        Expression? ast;

                        try
                        {
                            ast = parser.Parse();
                        }
                        catch (Exception ex)
                        {
                            OnFatal("Failed to Parse '" + buildContext.CurrentFile + "' :\n" + ex);
                            throw;
                        }
                        OnInfo("Parsed " + buildContext.CurrentFile);

                        // Prebuilding
                        try
                        {
                            ast.Build(buildContext);
                        }
                        catch (Exception ex)
                        {
                            OnFatal("Failed to Prebuild ast for '" + buildContext.CurrentFile + "' :\n" + ex);
                            throw;
                        }
                        OnInfo("Prebuilt " + buildContext.CurrentFile);
                    }
                }
            // Phase 2 Building
            buildContext.Datapack.Build(buildContext);

            OnInfo("Finished Parsing");
            // Writing completed build to file
            var dir = buildContext.Datapack.WriteToFile(destination);
            OnInfo("Wrote Build to File at " + dir.FullName);
        }
    }
}
