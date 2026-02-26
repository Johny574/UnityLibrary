public interface ICloudItem<T> {
    public abstract void Load(T save);
    public abstract T Save();
}