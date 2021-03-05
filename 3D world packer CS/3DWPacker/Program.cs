using System;
using System.Diagnostics;
using System.IO;

namespace _3DWPacker {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("What is your mod name?");
            string modName = Console.ReadLine();
            Console.WriteLine("Enter a short 1 line description of your mod...");
            string modDesc = Console.ReadLine();
            Console.WriteLine("Drag and drop the contents folder here!");
            string modDir = Console.ReadLine().Replace("\"", "");

            Directory.CreateDirectory(Path.Combine(modName, "contents"));

            Console.WriteLine("\nPacking the mod...");
            RecursiveCopy(new DirectoryInfo(modDir), new DirectoryInfo(Path.Combine(modName, "contents")));

            Console.WriteLine("Generating \"rules.txt\"...");
            File.WriteAllText(Path.Combine(modName, "rules.txt"),
                "[Definition]\n" +
                "titleIds = 0005000010145D00,0005000010145C00,0005000010106100\n" +
                "name = " + modName +
                "\npath = Super Mario 3D World/Game Mods/" + modName +
                "\ndescription = " + modDesc +
                " \nversion = 5");

            Console.Write("\nSuccess! Press any key to exit...");
            Console.ReadKey();

            Process exp = new Process();
            exp.StartInfo.FileName = "explorer.exe";
            exp.StartInfo.Arguments = Path.Combine(Directory.GetCurrentDirectory(), modName);
            exp.EnableRaisingEvents = true;

            exp.Start();
        }

        static void RecursiveCopy(DirectoryInfo origin, DirectoryInfo target) {
            foreach(DirectoryInfo dir in origin.GetDirectories())
                RecursiveCopy(dir, target.CreateSubdirectory(dir.Name));
            foreach(FileInfo file in origin.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
        }
    }
}
