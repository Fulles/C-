using System;
using System.IO.Ports; //Nuget System.IO.Ports


class DBGcmd
{
    public static void Main()
    {
        
        string[] ports = SerialPort.GetPortNames();

        Console.WriteLine("The following serial ports were found:");

        foreach (string port in ports)
        {
            Console.WriteLine(port);
        }
        try
        {
            string name = ChooseName();
            SerialPort Port = new SerialPort(name);
            PortIsOn(Port);
           
        }
        catch (Exception ex)
        {
           
        }
           

    }


    private static string ChooseName()
    {
        string[] ports = SerialPort.GetPortNames();
        Console.WriteLine("Choose a port to connect for the device(COM2)");
        string name = Console.ReadLine();
        name = name.ToUpper();

        while (!Array.Exists(ports, element => element == name))
        {
            Console.WriteLine("Choose correct name (for ex. com1, com2, etc.)");
            name = Console.ReadLine();
            name = name.ToUpper();
        }

        return name;
    }

   private static void PortIsOn (SerialPort port)
    {
        port.Open();
        port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

        Console.WriteLine("We are waiting for the device");
        while (port.IsOpen) { 

        Console.WriteLine("If you want to exit, please, type 'QUIT' or make a request");
        string message = Console.ReadLine();
        if (message == "QUIT")
        {
            port.Close();
        }
        else
            {
                port.WriteLine(message);
            }
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
