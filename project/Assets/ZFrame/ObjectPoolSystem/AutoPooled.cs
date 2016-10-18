using UnityEngine;
using System.Collections;

public class AutoPooled : MonoBehaviour
{
    [SerializeField]
    private float m_Delay;

    private void Start()
    {
#if !FX_DESIGN
        if (ObjectPoolManager.IsPooled(gameObject)) {
            ObjectPoolManager.DestroyPooled(gameObject, m_Delay);
        }

        if (ObjectPoolManager.IsPooledScenely(gameObject)) {
            ObjectPoolManager.DestroyPooledScenely(gameObject, m_Delay);
        }
#endif
        Destroy(gameObject, m_Delay);
    }
}
