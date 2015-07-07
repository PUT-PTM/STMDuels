using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class STMReceiver :IDisposable
{
	public SerialPort Port;
	public float axisX;
	public float axisY;
	public float axisZ;
	public bool is_shooting;

	public STMReceiver()
	{
		Port = new SerialPort("COM6", 115200, Parity.None, 8, StopBits.One);

		axisX = 0;
		axisY = 0;
		axisZ = 0;

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
	
	public float Data;
	public bool Break;
	
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
	
	
	private void InternalStartListening()
	{       

		while (_keepListenieng)
		{
			Port.BaseStream.Flush();

			
			if (Port.ReadByte() == 0xAA) //172
			{
				
				int command = Port.ReadByte();
				byte[] buffer = new byte[4];
				
				if (command == 0xAC)
				{
					
					Debug.Log("Komenda akcelerometra: " + command + "\n");
					
					buffer[0] = (byte)Port.ReadByte();
					buffer[1] = (byte)Port.ReadByte();
					buffer[2] = (byte)Port.ReadByte();
					buffer[3] = (byte)Port.ReadByte();
					axisX = BitConverter.ToSingle(buffer, 0);
					Debug.Log("Wspolrzedna X: " + axisX + "\n");
					
					buffer[0] = (byte)Port.ReadByte();
					buffer[1] = (byte)Port.ReadByte();
					buffer[2] = (byte)Port.ReadByte();
					buffer[3] = (byte)Port.ReadByte();
					axisY = BitConverter.ToSingle(buffer, 0);
					Debug.Log("Wspolrzedna Y: " + axisY + "\n");
					
					buffer[0] = (byte)Port.ReadByte();
					buffer[1] = (byte)Port.ReadByte();
					buffer[2] = (byte)Port.ReadByte();
					buffer[3] = (byte)Port.ReadByte();
					axisZ = BitConverter.ToSingle(buffer, 0);
					Debug.Log("Wspolrzedna Z: " + axisZ + "\n");
					
					byte crc = (byte)Port.ReadByte();
					
				}
				else if (command == 0x38) //56
				{
					Debug.Log("Komenda przycisku: " + command + "\n");
					byte button_state = (byte)Port.ReadByte();
					if(button_state == 255) is_shooting = true;
					else is_shooting = false;
					Debug.Log("Aktualny stan przycisku: " + button_state + "\n");
										
					byte crc = (byte)Port.ReadByte();
					
					if (button_state == 0)
						Break = false;
					else
						Break = true;
				}
			}
		}
		
	}
	
	public void Dispose()
	{
		_keepListenieng = false;
	}
}