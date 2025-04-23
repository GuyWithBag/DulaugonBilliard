using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ArUcoPoseReceiver : MonoBehaviour
{
	[Header("Network Settings")]
	public int port = 5065;

	[Header("Movement Settings")]
	public float positionMultiplier = 1.0f;
	public float rotationMultiplier = 1.0f;
	public float positionResponseSpeed = 15f;
	public float rotationResponseSpeed = 8f;
	[Range(0, 1)] public float positionSmoothing = 0.3f;
	[Range(0, 1)] public float rotationSmoothing = 0.4f;

	private UdpClient client;
	private Thread receiveThread;
	private bool isRunning;
	private Vector3 targetPosition;
	private Vector3 targetRotation;
	private Vector3 currentPosVelocity;
	private Vector3 currentRotVelocity;

	void Start()
	{
		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 0;
		InitializeNetwork();
	}

	void InitializeNetwork()
	{
		try
		{
			client = new UdpClient(port);
			isRunning = true;
			receiveThread = new Thread(ReceiveData);
			receiveThread.IsBackground = true;
			receiveThread.Start();
			Debug.Log($"UDP receiver started on port {port}");
		}
		catch (Exception e)
		{
			Debug.LogError($"Network error: {e.Message}");
		}
	}

	void ReceiveData()
	{
		while (isRunning)
		{
			try
			{
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
				byte[] data = client.Receive(ref anyIP);
				string jsonString = Encoding.UTF8.GetString(data);

				UnityMainThreadDispatcher.Instance().Enqueue(() => 
					{
						ProcessData(jsonString);
					});
			}
			catch (Exception e)
			{
				if (isRunning) Debug.LogWarning($"Receive error: {e.Message}");
			}
		}
	}

	void ProcessData(string jsonString)
	{
		try
		{
			var data = JsonUtility.FromJson<PoseData>(jsonString);

			// Position (convert from ArUco to Unity coordinates)
			targetPosition = new Vector3(
				data.position[0] * positionMultiplier,
				data.position[1] * positionMultiplier,
				data.position[2] * positionMultiplier
			);

			// Rotation (convert from ArUco to Unity coordinates)
			targetRotation = new Vector3(
				data.rotation[0] * rotationMultiplier,  // X
				data.rotation[1] * rotationMultiplier,  // Y
				data.rotation[2] * rotationMultiplier   // Z
			);
		}
		catch (Exception e)
		{
			Debug.LogWarning($"Parse error: {e.Message}");
		}
	}

	void Update()
	{
		// Smooth position movement
		transform.position = Vector3.SmoothDamp(
			transform.position,
			targetPosition,
			ref currentPosVelocity,
			positionSmoothing,
			positionResponseSpeed
		);

		// Smooth rotation movement
		Quaternion targetQuat = Quaternion.Euler(targetRotation);
		transform.rotation = Quaternion.Slerp(
			transform.rotation,
			targetQuat,
			rotationResponseSpeed * Time.deltaTime
		);
	}

	void OnDestroy()
	{
		isRunning = false;
		if (receiveThread != null && receiveThread.IsAlive)
			receiveThread.Abort();
		if (client != null)
			client.Close();
	}

	[System.Serializable]
	private class PoseData
	{
		public int marker_id;
		public float[] position;
		public float[] rotation;
	}
}