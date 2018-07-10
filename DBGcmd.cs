using System;
using System.IO.Ports; //Nuget System.IO.Ports

class DBGcmd
{
    public static void Main()
    {
        string name;
        string[] ports = SerialPort.GetPortNames();

        Console.WriteLine("The following serial ports were found:");

        foreach (string port in ports)
        {
            Console.WriteLine(port);
        }
        Console.WriteLine("Choose a port to connect for the device(COM2)");
        case1:
        name = Console.ReadLine();
        name = name.ToUpper();
        if (Array.IndexOf(ports, name) >= 0)
        {
            SerialPort Port = new SerialPort(name);
            Port.Open();
            Console.WriteLine("The port " + name + " is open for device");
            Port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            Port.WriteLine("halo");
            Console.WriteLine("We are waiting for the device");
            
            Console.WriteLine("If you want to exit, please, press any buttom");
            Console.ReadKey();
            Port.Close();

        }
        else
        {
            Console.WriteLine("Choose correct name (for ex. com1, com2, etc.)");
            goto case1;
        }
        
        
    }

    private static void DataReceivedHandler(
                        object sender,
                        SerialDataReceivedEventArgs e)
    {
        string start = "Start";
        string request = "r800000000";
        SerialPort sp = (SerialPort)sender;
        string indata = sp.ReadExisting();
        Console.WriteLine("Data Received:");
        Console.Write(indata);
        
        
        if (indata.Contains(start))
        {
            Console.WriteLine("We're connected");
            sp.WriteLine(request);
        }
      
    }
   
}