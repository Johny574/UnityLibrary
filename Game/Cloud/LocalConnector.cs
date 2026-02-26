public abstract class LocalConnector<T> : ICloudConnector {
    public abstract string LocalSave { get; }
    public abstract void Initilize();
    public T LoadLocal() {
        T save = Serializer.LoadFile<T>(LocalSave);
        return save;
    }

    public void SaveLocal(T save) => Serializer.SaveFile(save, LocalSave);
}