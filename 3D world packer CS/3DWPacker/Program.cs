using System;
using System.Diagnostics;
using System.IO;

namespace _3DWPacker {
    class Program {
        static void Main(string[] args) {

            //takes in input from thr comand prompt
            Console.WriteLine("What is your mod name?");
            string modName = Console.ReadLine();
            Console.WriteLine("Enter a short 1 line description of your mod...");
            string modDesc = Console.ReadLine();
            Console.WriteLine("Drag and drop the contents folder here!");
            string modDir = Console.ReadLine().Replace("\"", "");

            //makes a contents directory 
            Directory.CreateDirectory(Path.Combine(modName, "contents"));

            //Copys the contents folder the user dragged in earlyer to the output directory
            Console.WriteLine("\nPacking the mod...");
            RecursiveCopy(new DirectoryInfo(modDir), new DirectoryInfo(Path.Combine(modName, "contents")));

            //this makes the rules.txt for cemu to use
            Console.WriteLine("Generating \"rules.txt\"...");
            File.WriteAllText(Path.Combine(modName, "rules.txt"),
                "[Definition]\n" +
                "titleIds = 0005000010145D00,0005000010145C00,0005000010106100\n" +
                "name = " + modName +
                "\npath = Super Mario 3D World/Game Mods/" + modName +
                "\ndescription = " + modDesc +
                " \nversion = 5");

            //simple press enter to exit prompt
            Console.Write("\nSuccess! Press any key to exit...");
            Console.ReadKey();

            //this opens the output directory
            Process exp = new Process();
            exp.StartInfo.FileName = "explorer.exe";
            exp.StartInfo.Arguments = Path.Combine(Directory.GetCurrentDirectory(), modName);
            exp.EnableRaisingEvents = true;

            exp.Start();
        }

        //copy Directorys and sub directorys
        static void RecursiveCopy(DirectoryInfo origin, DirectoryInfo target) {
            foreach(DirectoryInfo dir in origin.GetDirectories())
                RecursiveCopy(dir, target.CreateSubdirectory(dir.Name));
            foreach(FileInfo file in origin.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
        }
    }
}
