using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string singleArgument = string.Join(" ", args);

            Console.WriteLine($"Single argument =>{singleArgument}");

            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = @"/C mongosh";
            p.Start();
            // Read the output stream first and then wait.
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();


            Console.WriteLine($"{output}  {System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}");



            Console.ReadLine();

        }
    }
}
