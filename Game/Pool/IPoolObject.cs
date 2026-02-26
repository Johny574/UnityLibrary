public interface IPoolObject<T> : IPoolObject {
    public abstract void Bind(T variant);
}

public interface IPoolObject
{
    public abstract void BindObject(object variant);
}