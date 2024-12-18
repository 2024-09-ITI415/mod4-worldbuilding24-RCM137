using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityStandardAssets.Utility
{
    public class ActivateTrigger : MonoBehaviour
    {
        // A multi-purpose script that causes an action to occur when
        // a trigger collider is entered or when the player looks at an object.
        public enum Mode
        {
            Trigger = 0,    // Just broadcast the action on to the target
            Replace = 1,    // replace target with source
            Activate = 2,   // Activate the target GameObject
            Enable = 3,     // Enable a component
            Animate = 4,    // Start animation on target
            Deactivate = 5  // Deactivate target GameObject
        }

        public Mode action = Mode.Activate;         // The action to accomplish
        public Object target;                       // The game object to affect. If none, the trigger works on this game object
        public GameObject source;
        public int triggerCount = 1;
        public bool repeatTrigger = false;

        public float interactionRange = 500f;         // Interaction range for raycasting
        public KeyCode interactKey = KeyCode.Mouse0; // The key to press for interaction (left mouse button)

        private Camera playerCamera;

        private void Start()
        {
            // Get the player's camera
            playerCamera = Camera.main;
        }

        private void Update()
        {
            // Raycast from the camera's position
            RaycastForInteractable();
        }

        private void RaycastForInteractable()
        {
            // Cast a ray from the camera's position forward
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            // Perform the raycast within the interaction range
            if (Physics.Raycast(ray, out hit, interactionRange))
            {
                GameObject hitObject = hit.collider.gameObject;

                // Check if the ray hits an object with a tag or name indicating it's collectible
                if (hitObject.CompareTag("Collectible"))
                {
                    // Debugging info for testing
                    Debug.Log("Looking at collectible: " + hitObject.name);

                    // Trigger action when the player clicks the left mouse button
                    if (Input.GetMouseButtonDown(0)) // Left mouse button (0)
                    {
                        TriggerAction(hitObject);
                    }
                }
            }
        }

        private void TriggerAction(GameObject targetGameObject)
        {
            triggerCount--;

            if (triggerCount == 0 || repeatTrigger)
            {
                // Perform the action on the target object
                switch (action)
                {
                    case Mode.Trigger:
                        targetGameObject.BroadcastMessage("DoActivateTrigger");
                        break;
                    case Mode.Replace:
                        if (source != null)
                        {
                            Instantiate(source, targetGameObject.transform.position, targetGameObject.transform.rotation);
                            Destroy(targetGameObject); // Remove the original object
                        }
                        break;
                    case Mode.Activate:
                        targetGameObject.SetActive(true);
                        break;
                    case Mode.Enable:
                        var behaviour = targetGameObject.GetComponent<Behaviour>();
                        if (behaviour != null) behaviour.enabled = true;
                        break;
                    case Mode.Animate:
                        var animation = targetGameObject.GetComponent<Animation>();
                        if (animation != null) animation.Play();
                        break;
                    case Mode.Deactivate:
                        targetGameObject.SetActive(false);
                        break;
                }
            }
        }
    }
}