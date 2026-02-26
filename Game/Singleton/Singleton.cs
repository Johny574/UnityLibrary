using UnityEngine;



public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    [SerializeField] bool _persistant = true;
    public static T Instance;
    protected virtual void Awake() {
        if (Instance == null)
            Instance = CreateInstance();
        
    }

    T CreateInstance() {
        T instance = this as T;

        if (_persistant) {
            if (gameObject.transform.parent != null)
                DontDestroyOnLoad(transform.parent);
            else
                DontDestroyOnLoad(instance.gameObject);
        }

        return instance;
    }

    void OnDestroy() {
        if (!_persistant)
            Instance = null;
    }
}