using UnityEngine;

namespace Kaden
{
    public class DoorAnim : MonoBehaviour
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