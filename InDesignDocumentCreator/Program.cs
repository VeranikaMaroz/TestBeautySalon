using System;
using System.Diagnostics;
using System.IO;

namespace InDesignDocumentCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            var templatePath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, @"BeatySalonOrder.indd");
            var userDocumentsPath = Environment.CurrentDirectory;
            var reportFilName = $"BeatySalonOrder_{DateTime.Now:yyyy-MM-dd-HH-mm-ss}";

            InDesign.Application myApp = GetInDesignApplicationInstance();
            if (myApp == null)
                return;

            OrdersReport.CreateOrdersDocument(myApp, templatePath, Path.Combine(userDocumentsPath, $"{reportFilName}.indd"), Path.Combine(userDocumentsPath, $"{reportFilName}.pdf"));
            myApp.Quit();
        }

        static InDesign.Application GetInDesignApplicationInstance()
        {
            InDesign.Application myApp = null;
            try
            {
                Process proc = new Process();
                proc.StartInfo.Verb = "runas";
                proc.StartInfo.FileName = @"C:\Program Files\Adobe\Adobe InDesign 2021\InDesign.exe";
                proc.StartInfo.WorkingDirectory = @"C:\Program Files\Adobe\Adobe InDesign 2021";
                proc.EnableRaisingEvents = true;
                proc.Start();

                Type inDesignAppType = Type.GetTypeFromProgID("InDesign.Application.2021");
                myApp = (InDesign.Application)Activator.CreateInstance(inDesignAppType);
            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                Console.WriteLine("Could not run InDesign");
                Console.WriteLine("Exception:  " + e.ErrorCode.ToString() + "   " + e.ToString());
                myApp = null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:  " + e.Message.ToString() + "   " + e.ToString());
                myApp = null;
            }

            return myApp;
        }

    }
}
