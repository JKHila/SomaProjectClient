using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class GCMController : MonoBehaviour {
    public dataController dc;

    private string _text = "(null)";
    // Use this for initialization
    void Start()
    {

        // Create receiver game object
        GCM.Initialize();

        // Set callbacks
        GCM.SetErrorCallback((string errorId) => {
            Debug.Log("Error!!! " + errorId);
        });

        GCM.SetMessageCallback((Dictionary<string, object> table) => {
            Debug.Log("Message!!!");
            GCM.ShowToast(table["key1"]+":"+table["key2"]);
        });
        GCM.SetRegisteredCallback((string registrationId) => {
            Debug.Log("Registered!!! " + registrationId);
            dc.registid(registrationId);
        });

        GCM.SetUnregisteredCallback((string registrationId) => {
            Debug.Log("Unregistered!!! " + registrationId);
        });

        GCM.SetDeleteMessagesCallback((int total) => {
            Debug.Log("DeleteMessages!!! " + total);
        });
        
        
    }

    // Update is called once per frame
    void Update () {
	
	}
}
