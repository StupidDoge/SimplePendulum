using System.Collections;
using UnityEngine;

namespace Assets.Scripts.StartMenu
{
    [RequireComponent(typeof(Animator))]
    public class AnimatedCircle : MonoBehaviour
    {
        [SerializeField] private float _delayBeforeAnimation;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _animator.enabled = false;

            StartCoroutine(StartAnimationWithDelay());
        }

        private IEnumerator StartAnimationWithDelay()
        {
            yield return new WaitForSeconds(_delayBeforeAnimation);

            _animator.enabled = true;
        }
    }
}