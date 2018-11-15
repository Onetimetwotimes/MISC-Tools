using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateVolumes
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args == null | (args[0].Contains("s") & args[0].Contains("n")))
                    printHelp();
                else if (args[0].Equals("CreateItems"))
                {
                    createItems(args[1]);
                }
                else if (args[0].Contains("n"))
                {
                    Console.WriteLine($"Creating volumes of { args[1] } files each");
                    if (args[0].Contains("f"))
                        CreateVolumesFN(args[1]);
                    else
                    {
                        if (args[0].Contains("m"))
                            CreateVolumesN(args[1], true);
                        else
                            CreateVolumesN(args[1], false);
                    }

                }
                else if (args[0].Contains("s"))
                {
                    Console.WriteLine($"Creating volumes of { args[1] } MB each");
                    CreateVolumesS(args[1]);
                }

            }
            else
                printHelp();

        }

        private static void CreateVolumesS(string n)
        {
            
        }

        private static void CreateVolumesN(string _n, bool m)
        {
            int n = int.Parse(_n);
            string env = Environment.CurrentDirectory;
            string self = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            List<String> files = Directory.GetFiles(env).ToList();
            int volumes = (int) Math.Ceiling((decimal)files.Count / n);

            Console.WriteLine($"There will be a total of {volumes} volumes.");

            files.Remove(self);
            files.Sort();

            List<String> FFiles = new List<String>();
            for (int i = 0; i < volumes; i++)
            {
                if (files.Count < n)
                {
                    Console.WriteLine($"Creating volume {n + 1}");
                    foreach (string file in files)
                    {
                        if (Directory.Exists(env + $"\\Volume {i + 1}")) ;
                        else
                            Directory.CreateDirectory(env + $"\\Volume {i + 1}");
                        if (m)
                        {
                            Console.WriteLine($"Moving file {files.IndexOf(file) + 1} of {files.Count()}");
                            File.Move(file, env + $"\\Volume {i + 1}\\" + file.Split('\\').Last());
                        }
                        else
                        {
                            Console.WriteLine($"Copying file {files.IndexOf(file) + 1} of {files.Count()}");
                            File.Copy(file, env + $"\\Volume {i + 1}\\" + file.Split('\\').Last());
                        }

                    }
                }
                else
                {
                    FFiles.AddRange(files.GetRange(0, n));
                    files.RemoveRange(0, n);
                    Console.WriteLine($"Creating volume {i + 1}");
                    foreach (string file in FFiles)
                    {
                        
                        if (Directory.Exists(env + $"\\Volume {i + 1}")) ;
                        else
                            Directory.CreateDirectory(env + $"\\Volume {i + 1}");
                        if (m)
                        {
                            Console.WriteLine($"Moving file {FFiles.IndexOf(file) + 1} of {n}");
                            File.Move(file, env + $"\\Volume {i + 1}\\" + file.Split('\\').Last());
                        }
                        else
                        {
                            Console.WriteLine($"Copying file {FFiles.IndexOf(file) + 1} of {n}");
                            File.Copy(file, env + $"\\Volume {i + 1}\\" + file.Split('\\').Last());
                        }

                    }
                    Console.WriteLine($"Volume {i + 1} created!");
                    files.Sort();
                    FFiles.Clear();
                }

            }


        }

        private static void CreateVolumesFN(string _n)
        {
            int n = int.Parse(_n);
            string env = Environment.CurrentDirectory;
            string self = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            List<String> files = Directory.GetDirectories(env).ToList();
            int volumes = (int)Math.Ceiling((decimal)files.Count / n);

            Console.WriteLine($"There will be a total of {volumes} volumes.");

            files.Sort();

            List<String> FFiles = new List<String>();
            for (int i = 0; i < volumes; i++)
            {
                if (files.Count() < n)
                {
                    if (Directory.Exists(env + $"\\Volume {i + 1}")) ;
                    else
                        Directory.CreateDirectory(env + $"\\Volume {i + 1}");
                    Console.WriteLine($"Creating volume {n + 1}");
                    foreach (string file in files)
                    {
                        Console.WriteLine($"Moving folder {files.IndexOf(file) + 1} of {files.Count()}");
                        Directory.Move(file, env + $"\\Volume {i + 1}\\" + file.Split('\\').Last());
                    }
                }
                else
                {
                    FFiles.AddRange(files.GetRange(0, n));
                    files.RemoveRange(0, n);
                    Console.WriteLine($"Creating volume {i + 1}");
                    foreach (string file in FFiles)
                    {
                        Console.WriteLine($"Moving folder {FFiles.IndexOf(file) + 1} of {n}");
                        if (Directory.Exists(env + $"\\Volume {i + 1}")) ;
                        else
                            Directory.CreateDirectory(env + $"\\Volume {i + 1}");

                        Directory.Move(file, env + $"\\Volume {i + 1}\\" + file.Split('\\').Last());
                    }
                    Console.WriteLine($"Volume {i + 1} created!");
                    files.Sort();
                    FFiles.Clear();
                }

            }
        }


        static void createItems(string _n)
        {
            int n = int.Parse(_n);
            for (int i = 0; i < n; i++)
            {
                File.Create(Environment.CurrentDirectory + "\\" + i.ToString());
            }
        }
        static void printHelp()
        {
            Console.WriteLine("Creates volumes of specified sizes \n" +
                "Argument | Function \n" +
                "-n | Creates volumes of specified number of files \n" +
                "-s | Creates volumes of specified size in MB --Not Implemented-- \n" +
                "-f | Creates volumes of FOLDERS instead of FILES --Requires that items be MOVED instead of COPIED--");
        }
    }
}
