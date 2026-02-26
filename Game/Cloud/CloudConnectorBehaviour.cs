using Unity.Services.Authentication;
using UnityEngine;

public abstract class CloudConnectorBehaviour<S> : MonoBehaviour { 
    public CloudConnector<S> Connector { get; private set; }
    protected ICloudAdapter<S> _adapter;
    protected void Awake() => _adapter = GetComponent<ICloudAdapter<S>>();

    void Start() {
        Connector = CreateConnector();
        Connector.Create(_adapter);
    }

    void OnDisable() {
        Connector.OnDisable();
    }

    protected async void OnApplicationQuit() {
        Connector.Save();
    }

#if !UNITY_EDITOR
    void OnApplicationPause(bool pauseStatus) {
        if (pauseStatus) {
            Connector.Save(); 
        }        
    }
#endif
    public abstract CloudConnector<S> CreateConnector();
}