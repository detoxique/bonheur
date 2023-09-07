using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace bonheur
{
    internal class Script
    {
        public bool Active = true;

        public string Path;
        private string code;

        public Script(string path)
        {
            Path = path;
            code = File.ReadAllText(Path);
        }

        public async void Run()
        {
            if (Active)
            {
                var options = ScriptOptions.Default.AddImports("System", "bonheur", "SFML").AddReferences("System", "bonheur", "SFML.Graphics", "SFML.System");
                var script = CSharpScript.Create(code, options);
                await script.RunAsync();
            }
        }
    }
}
