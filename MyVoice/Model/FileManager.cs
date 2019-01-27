using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyVoice.Model
{

    public static class FileManager
    {

        public static string[] ReadWordsFromFile(string filePath)
        {
            if (filePath == null || filePath.Length == 0 || !File.Exists(filePath))
                return new string[0];
            var list = new List<string>();
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
            return list.ToArray();
        }

        public static string[] GetWordsFromFilesList(string folderPath)
        {
            if (folderPath == null || folderPath.Length == 0 || !Directory.Exists(folderPath))
                return new string[0];
            string[] filePaths = Directory.GetFiles(folderPath, "*.mp3", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < filePaths.Length; i++)
                filePaths[i] = Path.GetFileNameWithoutExtension(filePaths[i]);
            return filePaths;
        }


    }

}