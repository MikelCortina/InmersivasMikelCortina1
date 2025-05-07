using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARSubsystems;
using TMPro; // Asegúrate de tener "TextMeshPro" en el proyecto

namespace UnityEngine.XR.ARFoundation.Samples
{
    [RequireComponent(typeof(ARRaycastManager))]
    public class PlaceOnPlane : PressInputBase
    {
        [Header("Prefabs para instanciar")]
        [SerializeField] GameObject prefabA;
        [SerializeField] GameObject prefabB;

        [Header("UI")]
        [SerializeField] TMP_Dropdown prefabDropdown;

        GameObject currentPrefab;

        bool m_Pressed;
        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
        ARRaycastManager m_RaycastManager;
        List<GameObject> spawnedObjects = new List<GameObject>();

        protected override void Awake()
        {
            base.Awake();
            m_RaycastManager = GetComponent<ARRaycastManager>();
        }

        void Start()
        {
            // Configura el prefab inicial
            SetCurrentPrefab(prefabDropdown.value);
            prefabDropdown.onValueChanged.AddListener(SetCurrentPrefab);
        }

        void SetCurrentPrefab(int index)
        {
            switch (index)
            {
                case 0:
                    currentPrefab = prefabA;
                    break;
                case 1:
                    currentPrefab = prefabB;
                    break;
                default:
                    currentPrefab = prefabA;
                    break;
            }
        }

        void Update()
        {
            if (Pointer.current == null || !m_Pressed)
                return;

            var touchPosition = Pointer.current.position.ReadValue();

            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = s_Hits[0].pose;

                GameObject newObject = Instantiate(currentPrefab, hitPose.position, hitPose.rotation);
                spawnedObjects.Add(newObject);

                m_Pressed = false;
            }
        }

        protected override void OnPress(Vector3 position) => m_Pressed = true;

        protected override void OnPressCancel() => m_Pressed = false;

        public void RemoveLastSpawnedObject()
        {
            if (spawnedObjects.Count > 0)
            {
                GameObject last = spawnedObjects[spawnedObjects.Count - 1];
                Destroy(last);
                spawnedObjects.RemoveAt(spawnedObjects.Count - 1);
            }
        }
    }
}
