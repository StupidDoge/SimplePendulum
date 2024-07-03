using Assets.Scripts.Configs;
using System;
using System.Collections;
using UnityEngine;
using static Assets.Scripts.Configs.CircleConfig;

namespace Assets.Scripts.Logic
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Circle : MonoBehaviour
    {
        public static event Action<Circle> OnCircleDropped;
        public static event Action<Circle, Circle, Circle> OnLineDestroyed;
        public static event Action<int> OnLineDestroyedWithScore;

        private const float CastDistance = 0.01f;
        private const string FloorTag = "Floor";
        private const string CircleTag = "Circle";

        [SerializeField] private CircleVisualEffect _visualEffect;

        [Header("Neighboring circles checks")]
        [SerializeField] private Transform _upCheck;
        [SerializeField] private Transform _upLeftCheck;
        [SerializeField] private Transform _upRightCheck;
        [SerializeField] private Transform _downCheck;
        [SerializeField] private Transform _downLeftCheck;
        [SerializeField] private Transform _downRightCheck;
        [SerializeField] private Transform _leftCheck;
        [SerializeField] private Transform _rightCheck;

        private CircleConfig _circleConfig;
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _sprite;
        private bool _isDropped;

        public int Score => _circleConfig.Score;
        public CircleType Type => _circleConfig.Type;
        public CircleVisualEffect VisualEffect => _visualEffect;

        public bool InVerticalLine
            => HasSameCircleAt(_upCheck) && HasSameCircleAt(_downCheck);

        public bool InHorizontalLine
            => HasSameCircleAt(_leftCheck) && HasSameCircleAt(_rightCheck);

        public bool InDiagonalLine
            => InIncreasingDiagonalLine || InDecreasingDiagonalLine;

        private bool InIncreasingDiagonalLine
            => HasSameCircleAt(_upRightCheck) && HasSameCircleAt(_downLeftCheck);

        private bool InDecreasingDiagonalLine
            => HasSameCircleAt(_upLeftCheck) && HasSameCircleAt(_downRightCheck);

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _sprite = GetComponent<SpriteRenderer>();
        }

        public void Init(CircleConfig config)
        {
            _circleConfig = config;
            _sprite.color = config.Color;
        }

        public bool HasSameCircleAt(Transform check)
        {
            Vector2 checkPosition = new(check.position.x, check.position.y);
            RaycastHit2D hit = Physics2D.Raycast(checkPosition, Vector2.zero, CastDistance, _circleConfig.CircleLayer);

            return HasSameCircleInDirection(hit);
        }

        public bool HasSameCircleInDirection(RaycastHit2D hit)
        {
            if (hit.collider == null)
            {
                return false;
            }

            if (hit.collider.TryGetComponent(out Circle circle))
            {
                if (Type == circle.Type)
                {
                    return true;
                }
            }

            return false;
        }

        public void DeleteHorizontalNeighbours()
            => ShowDestroyVisualEffectsForLine(_rightCheck, _leftCheck);

        public void DeleteVerticalNeighbours()
            => ShowDestroyVisualEffectsForLine(_upCheck, _downCheck);

        public void DeleteDiagonalNeigbours()
        {
            if (InIncreasingDiagonalLine)
            {
                ShowDestroyVisualEffectsForLine(_upRightCheck, _downLeftCheck);
            }
            else if (InDecreasingDiagonalLine)
            {
                ShowDestroyVisualEffectsForLine(_upLeftCheck, _downRightCheck);
            }
        }

        public Circle GetCircleAt(Transform check)
        {
            Vector2 checkPosition = new(check.position.x, check.position.y);
            RaycastHit2D hit = Physics2D.Raycast(checkPosition, Vector2.zero, CastDistance, _circleConfig.CircleLayer);

            if (hit.collider != null && hit.collider.TryGetComponent(out Circle circle))
            {
                if (circle != null)
                {
                    return circle;
                }
            }

            throw new NullReferenceException("Circle is null!");
        }

        public void DropWithForce(float force, float pendulumMovementDirection, float pendulumRotationAngle)
        {
            transform.parent = null;
            _rigidbody.isKinematic = false;

            int directionFromMovement = pendulumMovementDirection > 0 ? 1 : -1;
            float angle = transform.rotation.eulerAngles.z;
            if (angle > 180)
            {
                angle -= 360;
                directionFromMovement *= -1;
            }

            float dropDirection = angle / pendulumRotationAngle * directionFromMovement;

            _rigidbody.AddForce(force * dropDirection * transform.right, ForceMode2D.Impulse);
        }

        public IEnumerator TriggerDestroyAfterAnimation(Circle first, Circle second)
        {
            yield return new WaitForSeconds(VisualEffect.WaitingTime);

            OnLineDestroyed?.Invoke(this, first, second);
            OnLineDestroyedWithScore?.Invoke(_circleConfig.Score);
        }

        private void ShowDestroyVisualEffectsForLine(Transform first, Transform second)
        {
            Circle firstCircle = GetCircleAt(first);
            Circle secondCircle = GetCircleAt(second);
            _visualEffect.Play();
            firstCircle.VisualEffect.Play();
            secondCircle.VisualEffect.Play();

            StartCoroutine(TriggerDestroyAfterAnimation(firstCircle, secondCircle));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if ((collision.collider.CompareTag(FloorTag) || collision.collider.CompareTag(CircleTag)) && !_isDropped)
            {
                _isDropped = true;
                transform.rotation = Quaternion.identity;
                _rigidbody.freezeRotation = true;
                OnCircleDropped?.Invoke(this);
            }
        }
    }
}