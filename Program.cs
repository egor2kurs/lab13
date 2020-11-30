using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
namespace Лаб13
{   class NAVDiskInfo
    {
        
        public NAVDiskInfo (string s)
        {
            DriveInfo d = new DriveInfo(s);
            Console.WriteLine("Имя: " + d.Name);
            Console.WriteLine("Свободное место: "+d.TotalFreeSpace);
            Console.WriteLine("Информация о диске: "+d.DriveFormat);
            Console.WriteLine();
        }
        public static void Info()
        {
            DriveInfo[] d =DriveInfo.GetDrives();
            foreach (DriveInfo x in d)
            {
                Console.WriteLine("Имя: " + x.Name);
                Console.WriteLine("Объём: " + x.TotalSize);
                Console.WriteLine("Доступный объём: "+x.AvailableFreeSpace);
                Console.WriteLine("Метка: " +x.VolumeLabel);
                
            }
            
        }
        
    }
    class NAVFileInfo
    {
        FileInfo f;
      public NAVFileInfo(string s)
        {
            f = new FileInfo(s);
            Console.WriteLine("Путь: "+Path.GetFullPath(s));
            Console.WriteLine("Размер: "+f.Length);
            Console.WriteLine("Расширение: " + f.Extension);
            Console.WriteLine("Имя: " + f.Name);
            Console.WriteLine("Время создания: " + f.CreationTime);
        }
    }
    class NAVDirInfo
    {
        DirectoryInfo d;
        public NAVDirInfo(string s)
        {
            d = new DirectoryInfo(s);
            Console.WriteLine("Количество файлов: " + d.GetFiles().Length);
            Console.WriteLine("Время создания: "+d.CreationTime);
            Console.WriteLine("Количество поддерикториев: " + d.GetDirectories().Length);
            do
            {
                Console.WriteLine(d.Parent.Name);
                d = d.Parent;
            } while (d.Parent!=null);

        }
    }
    class NAVFileManager
    {
      public NAVFileManager(string s)
        {
            DirectoryInfo d = new DirectoryInfo(s);
            DirectoryInfo dr = new DirectoryInfo("D:/Downloads");
            dr.CreateSubdirectory("Подкаталог");
            NAVLog.Write("Создан Подкаталог D:/Downloads/Подкаталог");
            
            string path = @"D:\Downloads\Подкаталог\File.txt";
            FileInfo f = new FileInfo(path);
            NAVLog.Write("Создан Файл D:/Downloads/Подкаталог/File.txt");
            FileInfo[] fd;
            using (StreamWriter sr = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                DirectoryInfo[] sd = d.GetDirectories();
                foreach (DirectoryInfo x in sd)
                {

                    sr.WriteLine(x.Name);
                }
                fd = d.GetFiles();
                foreach (FileInfo x in fd)
                {

                    sr.WriteLine(x.Name);
                }

            }
            NAVLog.Write("Записаны данные диска D в Файл D:/Downloads/Подкаталог/File.txt");
            f.CopyTo(@"D:\Downloads\Подкаталог\File1.txt");
            NAVLog.Write("Файл File.txt скопирован в Файл D:/Downloads/Подкаталог/File1.txt");
            f.Delete();
                dr.CreateSubdirectory("Подкаталог1");
            NAVLog.Write("Создан Подкаталог D:/Downloads/Подкаталог1");
            foreach (FileInfo x in fd)
                {
                      if (x.Extension==".txt")
                    {
                        x.CopyTo($"D:/Downloads/Подкаталог1/{x.Name}");
                        
                        
                    }
                      
                }
            NAVLog.Write("Записаны .txt файлы диска D в Папку D:/Downloads/Подкаталог1/File.txt");
            dr = new DirectoryInfo("D:/Downloads/Подкаталог1");
            NAVLog.Write("Файл File.txt удалён");
            NAVLog.Write("Подкаталог1 перемещен в D:/Downloads/Подкаталог");
            dr.MoveTo("D:/Downloads/Подкаталог/Подкаталог1");
            using (FileStream fs = new FileInfo("D:/Lab8.txt").OpenRead())
            {
               using (FileStream fss=File.Create("D:/Lab81.txt"))
                {   
                    using (GZipStream g=new GZipStream(fss,CompressionMode.Compress))
                    {
                        fs.CopyTo(g);

                        
                    }
                }
            }
            using (FileStream fs = new FileInfo("D:/Lab81.txt").OpenRead())
            {
              using (FileStream fss = File.Create("D:/Lab811.txt"))
                {
                    using (GZipStream g = new GZipStream(fss, CompressionMode.Decompress))
                    {
                        g.CopyTo(fss);


                    }
                }
            }
            NAVLog.Read();
            NAVLog.Search(22);
            NAVLog.Delete();
        }
    }
    public static class NAVLog
    {
        static FileInfo f = new FileInfo("D:/navlogfile.txt");
        static string[] str;
        public static void Write(string s)
        {
          using (StreamWriter st=new StreamWriter(@"D:/navlogfile.txt",true, System.Text.Encoding.Default)) 
            {
                st.WriteLine(DateTime.Now+" "+s);
            }
          
        }
        public static void Read ()
        {
            string s;
            using (StreamReader st = new StreamReader(@"D:/navlogfile.txt", true))
            {   s= st.ReadToEnd();

            }
            
            str = s.Split('\n');
            Console.WriteLine($"Количество записей {str.Length}");
        }
        public static void Search (int d)
        {
          for (int i=0;i<str.Length-1;i++)
            {
                if (Int32.Parse(str[i].Substring(0, 2)) == d) Console.WriteLine(str[i]);
            }
        }
        public static void Delete ()
        {
            using (StreamWriter st = new StreamWriter(@"D:/navlogfile.txt", false, System.Text.Encoding.Default))
            {
                string d = DateTime.Now.ToString();
                d = d.Substring(11, 2);
                int di = Int32.Parse(d);
                for (int i = 0; i < str.Length - 1; i++)
                {
                    if (Int32.Parse(str[i].Substring(11, 2)) == di) st.WriteLine(str[i]);
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            NAVDiskInfo d = new NAVDiskInfo("D:");
            NAVDiskInfo.Info();
            Console.WriteLine();
            NAVFileInfo f = new NAVFileInfo("D:/Lab8.txt");
            Console.WriteLine();
            NAVDirInfo dr = new NAVDirInfo("D:/Универ/Прога/Лабы");
            Console.WriteLine();
            NAVFileManager fm = new NAVFileManager("D:/");
        }
    }
}
