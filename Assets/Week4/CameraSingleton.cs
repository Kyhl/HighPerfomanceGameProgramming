using UnityEngine;

namespace Week4
{
    public class CameraSingleton : MonoBehaviour
    {
        public static Camera instance;

        void Awake()
        {
            instance = GetComponent<Camera>();
        }
    }
}
