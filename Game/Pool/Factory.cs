using System.Threading.Tasks;
using UnityEngine;


[RequireComponent(typeof(PoolComponent))]
public class Factory<M, T> : Singleton<M> where M : MonoBehaviour
{
    protected PoolComponent _pool;
    protected override void Awake() {
        base.Awake();
        _pool = GetComponent<PoolComponent>();
    }

    protected async Task<IPoolObject> GetObject(T variant) {
        IPoolObject obj = await _pool.GetObject<T>();
        obj.BindObject(variant);
        return obj;
    }
}