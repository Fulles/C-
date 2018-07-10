using System;
using System.IO.Ports;    //Nuget System.IO.Ports

class PortDataReceived
{
    public static void Main()
    {
        SerialPort Port = new SerialPort("COM3");
        string start = "Start";

        Port.Open();

        Port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

        Port.WriteLine(start);


        Console.ReadKey();
        Port.Close();
    }

    private static void DataReceivedHandler(
                       object sender,
                       SerialDataReceivedEventArgs e)
    {
        string request = "r800000000";
        string response = "ABCD1234";
        SerialPort sp = (SerialPort)sender;
        string indata = sp.ReadExisting();
        Console.WriteLine(indata);
        if (indata.Contains(request))
        {
            sp.WriteLine(response);
        }

    }
}