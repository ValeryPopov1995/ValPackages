using UnityEngine;
using UnityEngine.Events;

namespace ValPackage.Common.Ui
{
    public class EscapeFunction : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnEscapePressed;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                OnEscapePressed?.Invoke();
        }
    }
}