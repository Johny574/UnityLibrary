


using UnityEngine;
public abstract class CloudAdapter<A, S> : Singleton<A> , ICloudAdapter<S> where A : MonoBehaviour
{
    public CloudConnector<S> Connector;
    public void Init(CloudConnector<S> connector)
    {
        Connector = connector;
    }
    public abstract void Load(S save);
    public abstract S Save();
}