using UnityEngine;
using UnityEngine.Events;

namespace ValeryPopov.Common.Ui
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