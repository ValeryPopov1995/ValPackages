using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

namespace ValPackage.Common.Shaders
{
    /// <summary>
    /// Liquid Wobble baheviour for Potion shader
    /// </summary>
    public class Wobble : MonoBehaviour
    {
        private const string _wobbleX = "_WobbleX";
        private const string _wobbleZ = "_WobbleZ";
        private const int _frameUpdate = 3;

        [SerializeField] private float _maxWobble = 0.03f;
        [SerializeField] private float _wobbleSpeed = 1f;
        [SerializeField] private float _recovery = 1f;

        private Renderer _renderer;
        private Vector3 _lastPos;
        private Vector3 _velocity;
        private Vector3 _lastRot;
        private Vector3 _angularVelocity;
        private Vector2 _wobbleAmount;
        private Vector2 _wobbleAmountToAdd;
        private float _pulse;
        private float _time = 0.5f;

        void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void OnEnable()
        {
            _ = UpdateTask();
        }

        private async Task UpdateTask()
        {
            while (gameObject.activeSelf && enabled)
            {
                _wobbleAmountToAdd.x = Mathf.Lerp(_wobbleAmountToAdd.x, 0, Time.deltaTime * _recovery);
                _wobbleAmountToAdd.y = Mathf.Lerp(_wobbleAmountToAdd.y, 0, Time.deltaTime * _recovery);

                _pulse = 2 * Mathf.PI * _wobbleSpeed;
                _wobbleAmount.x = _wobbleAmountToAdd.x * Mathf.Sin(_pulse * _time);
                _wobbleAmount.y = _wobbleAmountToAdd.y * Mathf.Sin(_pulse * _time);

                _renderer.material.SetFloat(_wobbleX, _wobbleAmount.x);
                _renderer.material.SetFloat(_wobbleZ, _wobbleAmount.y);

                _velocity = (_lastPos - transform.position) / Time.deltaTime;
                _angularVelocity = transform.rotation.eulerAngles - _lastRot;

                _wobbleAmountToAdd.x += Mathf.Clamp((_velocity.x + (_angularVelocity.z * 0.2f)) * _maxWobble, -_maxWobble, _maxWobble);
                _wobbleAmountToAdd.y += Mathf.Clamp((_velocity.z + (_angularVelocity.x * 0.2f)) * _maxWobble, -_maxWobble, _maxWobble);

                _lastPos = transform.position;
                _lastRot = transform.rotation.eulerAngles;

                _time += Time.deltaTime * _frameUpdate;
                await UniTask.DelayFrame(_frameUpdate);
            }
        }
    }
}