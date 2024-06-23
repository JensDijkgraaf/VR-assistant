using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class FileExtractor : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return EnsureFileExtracted();
    }

    private IEnumerator EnsureFileExtracted()
    {
        string persistentDirectory = Path.Combine(Application.persistentDataPath, "Model");
        string persistentPath = Path.Combine(persistentDirectory, "silero_vad.onnx");

        if (!File.Exists(persistentPath))
        {
            string streamingPath = Path.Combine(Application.streamingAssetsPath, "Model/silero_vad.onnx");            
            using (UnityWebRequest request = UnityWebRequest.Get(streamingPath))
            {
                yield return request.SendWebRequest();
                
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Failed to load file: " + request.error);
                }
                else
                {
                    // Ensure the directory exists
                    Directory.CreateDirectory(persistentDirectory);
                    
                    File.WriteAllBytes(persistentPath, request.downloadHandler.data);
                }
            }
        }
    }
}
