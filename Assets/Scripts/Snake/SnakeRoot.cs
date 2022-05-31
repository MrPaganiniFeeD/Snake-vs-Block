using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Snake
{
    [RequireComponent(typeof(TailGenerator))]
    public class SnakeRoot : MonoBehaviour
    {
        [SerializeField] private SnakeHead _head;
        [SerializeField] private float _speed;
        [SerializeField] private int _tailGeneratorSize;
        [SerializeField] private float _tailSpringine;

        public int CurrentSize;
        
        public event UnityAction<int> SizeUpdate;
        public event Action Death;

        private SnakeInput _input;
        private TailGenerator _tailGenerator;
        private List<Segment> _tails;

        private void Awake()
        {
            _tailGenerator = GetComponent<TailGenerator>();
            _tails = _tailGenerator.Generator(_tailGeneratorSize);
            _input = GetComponent<SnakeInput>();
            UpdateCurrentSize();
            SizeUpdate?.Invoke(_tails.Count);
    
        }

        private void OnEnable()
        {
            _head.BlockCollided += OnBlockCollided;
            _head.BonusCollected += OnBonusCollected;
        }

        private void OnDisable()
        {
            _head.BlockCollided -= OnBlockCollided;
            _head.BonusCollected -= OnBonusCollected;
        }

        private void FixedUpdate()
        {
            Move(_head.transform.position + _head.transform.up * _speed * Time.deltaTime);

            _head.transform.up = _input.GetDirectionToClick(_head.transform.position);
        }

        private void UpdateCurrentSize() => 
            CurrentSize = _tails.Count;

        private void Move(Vector3 nextPosition)
        {
            Vector3 previousPosition = _head.transform.position;

            foreach (var segment in _tails)
            {
                Vector3 tempPosition = segment.transform.position;
                segment.transform.position = 
                    Vector2.Lerp(segment.transform.position, previousPosition, _tailSpringine * Time.deltaTime);
                previousPosition = tempPosition;

            }
            _head.Move(nextPosition);
        }
        private void OnBlockCollided()
        {
            if (_tails.Count - 1 == 0) 
                Death?.Invoke();

            Segment deletedSegment = _tails[_tails.Count - 1];
            _tails.Remove(deletedSegment);
            Destroy(deletedSegment.gameObject);
            UpdateCurrentSize();
            SizeUpdate?.Invoke(_tails.Count);
        }
        private void OnBonusCollected(int bonusCollected)
        {
            _tails.AddRange(_tailGenerator.Generator(bonusCollected));
            UpdateCurrentSize();
            SizeUpdate?.Invoke(_tails.Count);
        }
    }
}
