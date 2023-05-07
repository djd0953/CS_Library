using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wLib
{
    public class WLIB_DLL
    {
        static WLIB_DLL _instance = new WLIB_DLL();

        static bool isInitialized = false;

        public static WLIB_DLL Instance
        {
            get
            {
                if (isInitialized == false)
                {
                    AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(ResolveAssembly);
                    isInitialized = true;
                }

                return _instance;
            }
        }

        static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            Assembly thisAssembly = Assembly.LoadFrom("wLib.dll");
            Console.WriteLine($"!!파일을 찾을 수 없습니다.({args.Name}) ");

            string resourceName = null;
            string fileName = args.Name.Substring(0, args.Name.IndexOf(',')) + ".dll";
            foreach (string name in thisAssembly.GetManifestResourceNames())
            {
                if (name.EndsWith(fileName))
                {
                    resourceName = name;
                }
            }

            if (resourceName != null)
            {
                using (System.IO.Stream stream = thisAssembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        Console.WriteLine($"!!파일을 로드합니다.({resourceName})");

                        byte[] assembly = new byte[stream.Length];
                        stream.Read(assembly, 0, assembly.Length);
                        return Assembly.Load(assembly);
                    }
                }
            }
            return null;
        }
    }
    
    public static class Spin
    {
        public static void Sleep(int second, ref bool isRunning)
        {
            TimeSpan StopTime = TimeSpan.FromSeconds(second);
            do
            {
                Thread.Sleep(100);
                StopTime -= TimeSpan.FromMilliseconds(100);
            } while (StopTime.TotalMilliseconds > 0 && isRunning == true);
        }
    }


}
