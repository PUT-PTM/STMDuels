using UnityEngine;

//[RequireComponent(typeof(CarController))]
public class PlayerControlSTM : MonoBehaviour
{
	//private CarController _controller;
	private STMReceiver _receiver;
	void Start()
	{
		_receiver = new STMReceiver();
		_receiver.StartListening();
	}
	
	void Update()
	{

	}
	
	void OnDestroy()
	{
		if (_receiver != null)
		{
			_receiver.Dispose();
		}
	}
}