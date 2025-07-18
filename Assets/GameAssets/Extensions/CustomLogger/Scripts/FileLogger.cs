using UnityEngine;
using System.IO;

namespace CustomLogger
{

	[AddComponentMenu("Monitoring/File Logger")]
	public class FileLogger: MonoBehaviour
	{

		[SerializeField] private string		m_pathDirectory;
		[SerializeField] private string		m_fileName = "default";
		[SerializeField] private bool		m_usePidAsExtension;

		#pragma warning disable 0414
		private string						m_path;
		private static FileStream			m_stream = null;
		private static StreamWriter			m_writer; 
		#pragma warning restore 0414

		private void Awake ()
		{
			m_path = (m_pathDirectory != "" ? m_pathDirectory + '/' : "") + m_fileName + (m_usePidAsExtension ? "." + System.Diagnostics.Process.GetCurrentProcess().Id + ".log" : ".log");
			if (string.IsNullOrEmpty(m_pathDirectory) == false)
			{
				if (Directory.Exists(m_pathDirectory) == false)
					Directory.CreateDirectory(m_pathDirectory);
			}
		}

		private void OnEnable ()
		{
			Application.logMessageReceived += this.HandleLogs;
		}

		private void OnDisable ()
		{
			Application.logMessageReceived -= this.HandleLogs;
		}
	
		private void HandleLogs ( string message, string trace, LogType type )
		{
			WriteInFile(type, message);
		}

		private void WriteInFile ( LogType type, string message )
		{
			#if UNITY_WEBGL || UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
				return ;
			#else

				if (type != LogType.Log)
					message = "[" + type.ToString() + "] " + message;

				// print time
				message = "[" + System.DateTime.Now.ToString() + "] " + message;

				if (File.Exists(m_path))
					m_stream = File.Open(m_path, FileMode.Append, FileAccess.Write);
				else
					m_stream = File.Create(m_path);

				m_writer = new StreamWriter(m_stream);
				m_writer.WriteLine(message);
				m_writer.Close();

			#endif
		}

	}

}