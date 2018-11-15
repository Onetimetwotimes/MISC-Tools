using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rmRedSubDir
{
    class Program
    {
        static void Main(string[] args)
        {
            string env = Environment.CurrentDirectory;
            string[] dirs = Directory.GetDirectories(env);
            if (!(args.Length > 0))
            {


                foreach (string dir in dirs)
                {
                    string _dir = $"{dir}{dir.Remove(0, env.Count())}";
                    Console.WriteLine(_dir);

                    string[] files = Directory.GetFiles(_dir, "*.*");
                    foreach (string file in files)
                    {
                        Console.WriteLine($"{dir}\\{file.Split('\\').Last()}");
                        File.Move(file, $"{dir}\\{file.Split('\\').Last()}");
                    }
                    Directory.Delete(_dir);
                }
            }
            else if (args[0].Contains("c"))
            {
                foreach (string dir in dirs)
                {
                    string _dir = $"{dir}{dir.Remove(0, env.Count())}";
                    try
                    {
                        int n = int.Parse(args[1]);
                        if (Directory.GetFiles(dir).Count() > n)
                        {
                            Console.WriteLine($"{dir} has more than {n} files!");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Invalid arguments! {e.Data}");
                        return;
                    }


                }
            }


        }
    }
}
