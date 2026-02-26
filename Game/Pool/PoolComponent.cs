using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PoolComponent : MonoBehaviour {
    #region  Reference
    [SerializeField] AssetReference _assetReference;
    AsyncOperationHandle<GameObject>? _loadedReference = null;
    #endregion

    Queue<GameObject> _available =  new();
    public List<GameObject> Objects  { get; private set; } = new();
    int _poolSize = 16;

    public async void Awake() {
        await Addressables.InitializeAsync().Task;
        CreatePool();
    }

    async void CreatePool() {
        if (_poolSize == 0)
            return;

        else if (_assetReference == null)
            throw new System.Exception($"No prefab assigned to pool {this}.");

        for (int i = 0; i < _poolSize; i++) {
            GameObject obj = await Create(_assetReference);
            Objects.Add(obj);
        }
    }

    public async Task<IPoolObject> GetObject<T>() {
        GameObject obj;
        try {
            obj = _available.Dequeue();
        }
        catch {
            obj = await Create(_assetReference, false);
        }

        obj.gameObject.SetActive(true);
        return obj.GetComponent<IPoolObject>();
    }

    async Task<AsyncOperationHandle<T>> LoadAsset<T>(object key) {
        try {
            AsyncOperationHandle<T> loadedasset = Addressables.LoadAssetAsync<T>(key);
            await loadedasset.Task;

            if (loadedasset.Status == AsyncOperationStatus.Succeeded)
                return loadedasset;
            else
                return default;
        }
        catch  {
            return default;
        }
    }

     async Task<GameObject> Create(AssetReference key, bool enqueue = true, SpawnData transform = null) {
        if (!key.RuntimeKeyIsValid())
            throw new System.Exception($"Invalid runtime key on asset {key}: {key.RuntimeKey}");

        if (_loadedReference == null)
            _loadedReference = await LoadAsset<GameObject>(key);

        var gameobject = ((AsyncOperationHandle<GameObject>)_loadedReference).Result;

        if (transform == null)
            transform = new SpawnData(gameobject.transform.position, gameobject.transform.rotation.eulerAngles, gameobject.transform.localScale, gameobject.gameObject.activeSelf, gameObject.transform);

        GameObject newObject = Object.Instantiate(gameobject);

        if (enqueue)
            _available.Enqueue(newObject);

        PoolObject poolObject = newObject.AddComponent<PoolObject>();
        transform.SetTransform(newObject.transform);
        poolObject.Disable += (poolobj) => _available.Enqueue(poolobj);
        return newObject;
    }

    public void OnDisable() {
        foreach (var obj in Objects)
            GameObject.Destroy(obj);
    }
}