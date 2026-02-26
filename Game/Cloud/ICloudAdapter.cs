public interface ICloudAdapter<S> {
    public abstract S Save();
    public abstract void Load(S save);
    public abstract void Init(CloudConnector<S> connector);
}