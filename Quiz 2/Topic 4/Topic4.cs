﻿using System.Text;
using System.Runtime.InteropServices;

namespace Quiz2
{
    public class Topic4
    {
        public static void Showcase()
        {
            Directory.SetCurrentDirectory("C:\\Users\\bogve\\source\\repos\\Quiz 2\\Quiz 2\\Topic 4");

            string dir1 = "Dir1/";
            string dir2 = @"C:\Users\bogve\source\repos\Quiz 2\Quiz 2\Topic 4\Dir2\";
            string dir3 = "C:\\Users\\bogve\\source\\repos\\Quiz 2\\Quiz 2\\Topic 4\\Dir3\\";
            string dir4 = "Dir4/";
            
            string file3 = Path.Join(Directory.GetCurrentDirectory(), "File1.txt");
            string file4 = Path.Join(Directory.GetCurrentDirectory(), "File2.txt");

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
            Console.ReadKey(true);

            var files2 = Directory.EnumerateFiles(dir2, "*File2.txt");
            foreach (string fileName in files2)
            {
                Console.WriteLine(fileName);
            }
            Console.ReadKey(true);

            Directory.Move(dir4, dir2 + "Dir4");
            Console.ReadKey(true);

            CopyDirectory(dir3, dir2);
            Console.ReadKey(true);

            if (!File.Exists(file3))
            {
                using (StreamWriter sw = File.CreateText(file3))
                {
                    sw.WriteLine("Placeholder line 1");
                    sw.WriteLine("Placeholder line 2");
                    sw.Write(true);
                    sw.Write("\n");
                    sw.Write(54);
                    sw.Write("\n");
                }
                using (StreamWriter sw = File.AppendText(file3))
                {
                    sw.WriteLine("Placeholder line 3");
                    sw.WriteLine("Placeholder line 4");
                    sw.Write(4.0);
                    sw.Write('\n');
                    sw.Write(sw);
                }
            }
            Console.ReadKey(true);

            if (!File.Exists(file4))
            {
                using (FileStream fs = File.Create(file4))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8, true))
                    {
                        bw.Write(false);
                        bw.Write(1234567890);
                    }
                }
                using (FileStream fs = File.Open(file4, FileMode.Open))
                {
                    using (BinaryReader br = new BinaryReader(fs, Encoding.UTF8, false))
                    {
                        Console.WriteLine(br.ReadBoolean());
                        Console.WriteLine(br.ReadInt32());
                    }
                }
            }
            Console.ReadKey(true);

            MemoryShowcase();
        }

        static void MemoryShowcase()
        {
            var en = new UnicodeEncoding();

            using (MemoryStream ms = new MemoryStream())
            {

                string? input = Console.ReadLine();
                byte[] inputBytes = input != null ? en.GetBytes(input) : [];

                ms.Write(inputBytes);
                ms.Seek(0, SeekOrigin.Begin);

                byte[] bytes = new byte[ms.Length];
                int i = ms.Read(bytes);
                for (; i < ms.Length; ++i)
                {
                    bytes[i] = (byte)ms.ReadByte();
                }

                int charCnt = en.GetCharCount(bytes, 0, i);
                char[] chars = new char[charCnt];
                en.GetDecoder().GetChars(bytes, 0, i, chars, 0);
                Console.WriteLine(chars);
            }
            Console.ReadKey(true);

            unsafe
            {
                byte[] data = en.GetBytes("Some data to be stored in memory");
                byte* dataPtr = (byte*)(Marshal.AllocHGlobal(data.Length)).ToPointer();

                var writeStream = new UnmanagedMemoryStream(dataPtr, data.Length, data.Length, FileAccess.Write);
                writeStream.Write(data, 0, data.Length);
                writeStream.Close();

                byte[] output = new byte[data.Length];

                var readStream = new UnmanagedMemoryStream(dataPtr, data.Length, data.Length, FileAccess.Read);
                readStream.Position = sizeof(char) * 26;
                readStream.Read(output, 0, sizeof(char) * 6);
                readStream.Close();

                Marshal.FreeHGlobal((IntPtr)dataPtr);

                Console.WriteLine(en.GetString(output));
            }
        }

        static void CopyDirectory(string source, string dest)
        {
            DirectoryInfo srcDir = new DirectoryInfo(source);
            DirectoryInfo[] dirList = srcDir.GetDirectories();
            string target = Path.Join(dest, srcDir.Name);
            Directory.CreateDirectory(target);

            foreach (FileInfo fl in srcDir.GetFiles())
            {
                string destPath = Path.Combine(target, fl.Name);
                Console.WriteLine($"Copying file {fl.Name} to {destPath}");
                fl.CopyTo(destPath);
            }

            foreach (DirectoryInfo dirInfo in dirList)
            {
                Console.WriteLine($"Copying directory {dirInfo.Name} to {target}");
                CopyDirectory(dirInfo.FullName, target);
            }
        }
    }
}