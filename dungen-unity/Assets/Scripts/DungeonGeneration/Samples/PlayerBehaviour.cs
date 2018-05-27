using UnityEngine;
using DungeonGeneration.Generator;
using DungeonGeneration.Renderer;
using DungeonGeneration.Generator.Samples;
using DungeonGeneration.Logging;

namespace DungeonGeneration.Samples {

    public class PlayerBehaviour : MonoBehaviour {
        private Rigidbody _rigidBody;
        private float _speed;
        private Vector3 _movement;

        void Start() {
            _rigidBody = GetComponent<Rigidbody>();
            _speed = 4f;
        }

        void FixedUpdate() {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            _movement.Set(horizontal, 0f, vertical);
            _movement = _movement.normalized * _speed * Time.deltaTime;
            _rigidBody.MovePosition(transform.position + _movement);
        }
    }
}