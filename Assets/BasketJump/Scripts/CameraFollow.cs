using UnityEngine;

namespace BasketJump
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform player;
        public Vector3 offset = new Vector3(0, 2, -10); // Offset between player and camera
        public float verticalThreshold = 0.5f; // Threshold to start following player upwards
        public float smoothSpeed = 5f; // Speed of camera movement

        private Vector3 targetPosition;

        private void Start()
        {
            targetPosition = player.position + offset;
        }

        private void Update()
        {
            if (player.position.y - transform.position.y > verticalThreshold)
            {
                Vector3 targetPosition = new Vector3(transform.position.x, player.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
            }
        }
    }
}
