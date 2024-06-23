using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using SimpleFileBrowser;


public class FileBrowse : MonoBehaviour
{
    public static string prompt;
    // Start is called before the first frame update
    void Start()
    {
        prompt = "";
        // FileBrowser.SetDefaultFilter( ".txt" );

    }
    public void BrowseFiles()
    {
        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((string path) =>
        {
            if (path == null)
            {
                Debug.Log("Operation cancelled");
                return;
            }
            else
            {
                // If the file is a txt file, read the contents
                if (path.EndsWith(".txt"))
                {
                    prompt = System.IO.File.ReadAllText(path);
                }
                else
                {
                    Debug.Log("File is not a txt file");
                }
            }
        });
    }
    // public void BrowseFiles()
    // {
    //     FileBrowser.ShowLoadDialog( ( paths ) => { if(paths[0].EndsWith(".txt")) prompt = System.IO.File.ReadAllText(paths[0]); },
	// 							   () => { Debug.Log( "Canceled" ); },
	// 							   FileBrowser.PickMode.Files, false, null, null, "Select Folder", "Select" );

    //     Debug.Log("Prompt: " + prompt);
    // }
}
