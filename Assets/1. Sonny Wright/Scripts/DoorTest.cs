using UnityEngine;

namespace Sonny
{
    public class DoorTest : MonoBehaviour
    {
        private Animation doorAnimation;

        void Start()
        {
            doorAnimation = GetComponent<Animation>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                doorAnimation.Play();
            }
        }
    }
}