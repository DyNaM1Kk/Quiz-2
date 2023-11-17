using System;
using System.IO;
using System.Runtime;

namespace Quiz2
{
    class Topic4
    {
        public static void Main()
        {
            string dir1 = "Dir1/";
            string dir2 = @"C:\Users\bogve\source\repos\Quiz 2\Quiz 2\Dir2\";
            string dir3 = "C:\\Users\\bogve\\source\\repos\\Quiz 2\\Quiz 2\\Dir3\\";
            string dir4 = "Dir4/";

            try
            {
                Directory.SetCurrentDirectory("C:\\Users\\bogve\\source\\repos\\Quiz 2\\Quiz 2");

                var files1 = Directory.GetFiles(dir1);
                foreach (string fileName in files1)
                {
                    Console.WriteLine(fileName);
                    var fileInfo = new FileInfo(fileName);
                    using (StreamReader sr = fileInfo.OpenText())
                    {
                        string? line = "";
                        while ((line = sr.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                        }
                    }
                    Console.WriteLine("Attributes: Creation time - {0}, Extension - {1}, Read only - {2}", fileInfo.CreationTime, fileInfo.Extension, fileInfo.IsReadOnly);
                }

                var files2 = Directory.EnumerateFiles(dir2, "*.txt");
                foreach (string fileName in files2)
                {
                    Console.WriteLine(fileName);
                }

                //Directory.Move(dir4, dir2 + "Dir4");
                //CopyDirectory(dir3, dir2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static void CopyDirectory(string source, string dest)
        {
            try
            {
                var srcDir = new DirectoryInfo(source);
                var dirList = srcDir.GetDirectories();
                Directory.CreateDirectory(dest);

                foreach (FileInfo fl in srcDir.GetFiles())
                {
                    string destPath = Path.Combine(dest, fl.Name);
                    Console.WriteLine($"Copying file {fl.Name} to {destPath}");
                    fl.CopyTo(destPath);
                }

                foreach (DirectoryInfo dirInfo in dirList)
                {
                    string destPath = Path.Combine(dest, dirInfo.Name);
                    Console.WriteLine($"Copying directory {dirInfo.Name} to {destPath}");
                    CopyDirectory(dirInfo.FullName, destPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}