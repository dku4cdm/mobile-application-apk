using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace Close_Contact
{
	[Serializable]
	public class ClassScoreManager
	{
		// класс "счет"
		[Serializable]
		public class ScoreItem
		{
			public int Value ;
		}

		public ScoreItem Score;

		// прочитать счет из файла
		public ClassScoreManager ReadScores()
		{
			try
			{
				var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
				var filePath = System.IO.Path.Combine(sdCardPath, "record.xml");
				FileStream fStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
				var myBinaryFormatter = new BinaryFormatter();
				var mc = (ClassScoreManager) myBinaryFormatter.Deserialize(fStream);
				fStream.Close();
				return mc;
			}
			catch (Exception e)
			{
				Score = new ScoreItem ();
				return this;
			}
		}

		// записать счет в файл
		public void WriteScores()
		{
			var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
			/*if (!Directory.Exists(sdCardPath +"/Application")) Directory.CreateDirectory (sdCardPath +"/Application");
			if (!Directory.Exists(sdCardPath +"/Application/Dageron Studio")) Directory.CreateDirectory (sdCardPath +"/Application/Dageron Studio");*/
			var filePath = System.IO.Path.Combine(sdCardPath, "record.xml");
			FileStream fStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
			var myBinaryFormatter = new BinaryFormatter();
			myBinaryFormatter.Serialize(fStream, this);
			fStream.Close();
		}
	}
}