using System;
using System.IO.Ports;    //Nuget System.IO.Ports

class Emulator
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
        int count = 0;
        string[] requests = { "r100000000", "r200000000", "r300000000", "r400000000",
                "r500000000", "r600000000", "r700000000", "r800000000", "r900000000"};
        string[] responses = { "ABCD1234", "ADSS3444", "QWER4321", "WRRRRERRE",
                    "ASDFGHJKL", "33444", "TYUI4567", "ZXCV9876", "MNBV0000" };
        SerialPort sp = (SerialPort)sender;
        string indata = sp.ReadExisting();
        Console.WriteLine(indata);
        while (count < 8)
        {
            if (indata.Contains(requests[count]))
            {
                sp.WriteLine(responses[count]);
            }
            count++;
        }
        

    }
}
