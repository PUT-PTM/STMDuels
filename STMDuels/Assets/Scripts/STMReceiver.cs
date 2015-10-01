using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class STMReceiver :IDisposable
{
	public SerialPort Port;
	public Int16 axisXint;
	public Int16 axisYint;
	public Int16 axisZint;
    public bool is_shooting;

	public STMReceiver()
	{
		Port = new SerialPort("COM15", 115200, Parity.None, 8, StopBits.One);

		axisXint = 0;
		axisYint = 0;
		axisZint = 0;

		if (Port == null)
		{
			Debug.Log("Blad, Port = Null");
		}
		
		try
		{
			Port.Open();
			Debug.Log("Port otwarty poprawnie");
		}
		
		catch
		{
			Debug.Log("Blad, nie mozna otworzyc portu");
		}
	}
	
	private bool _keepListenieng = true;
	private Thread t;
	public void StartListening()
	{
		if (Port.IsOpen == false)
		{
			Debug.Log("Port zamkniety. Nie mozna nasluchiwac");
			return;
		}
		t = new Thread(InternalStartListening);
		_keepListenieng = true;
		t.Start(); 
	}

    public void StopListening()
    {
        Port.Close();
    }
	
	private void InternalStartListening()
	{       

		while (_keepListenieng)
		{
			Port.BaseStream.Flush();



                if (Port.ReadByte() != 0xAA)
                {
                continue;
                }

				
				int command = Port.ReadByte();
				byte[] buffer = new byte[2];
                
                Debug.Log("command: " + command + "\n");
				if (command == 0xAC)
				{
					
					
					buffer[0] = (byte)Port.ReadByte();
					buffer[1] = (byte)Port.ReadByte();
                    Debug.Log("buforX: " + buffer[0] + "\n");
                    axisXint = BitConverter.ToInt16(buffer, 0);
					Debug.Log("wsp. X: " +axisXint);
					
                    
					buffer[0] = (byte)Port.ReadByte();
					buffer[1] = (byte)Port.ReadByte();
                    Debug.Log("buforY: " + buffer[0]+ "\n");
                    axisYint = BitConverter.ToInt16(buffer, 0);
					Debug.Log("wsp. Y: " +axisYint);


					buffer[0] = (byte)Port.ReadByte();
				    buffer[1] = (byte)Port.ReadByte();
                    axisZint = BitConverter.ToInt16(buffer, 0);
					Debug.Log("wsp. Z: " +axisZint);
					


					byte crc = (byte)Port.ReadByte();
					
				}
                 
				else if (command == 0x38) //56
				{
					Debug.Log("Komenda przycisku: " + command + "\n");
					Int16 button_state = Convert.ToInt16(Port.ReadByte());
					if(button_state == 255) is_shooting = true;
					else is_shooting = false;
					Debug.Log("Aktualny stan przycisku: " + button_state + "\n");
										
					byte crc = (byte)Port.ReadByte();
				}

			}
        
	}
	
	public void Dispose()
	{
		_keepListenieng = false;
	}
}