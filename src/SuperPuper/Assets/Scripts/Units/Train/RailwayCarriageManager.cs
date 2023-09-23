using Data.Static.Trains;
using UI;
using Units.Interactive;
using Units.QuickOutline.Scripts;
using UnityEngine;

namespace Units.Train
{
    public class RailwayCarriageManager : MonoBehaviour
    {
        [SerializeField] private RailwayCarriageScriptableObject _railwayCarriageScriptableObject;
        [SerializeField] private TrainConfigurationScriptableObject _trainConfigurationScriptableObject;
        private Outline _outline;
        private InteractiveObject _interactiveObject;
        private GameObject _screen;


        private void Start()
        {
            if (_railwayCarriageScriptableObject.IsInteractive == false) return;

            _interactiveObject = gameObject.AddComponent<InteractiveObject>();
            _outline = gameObject.AddComponent<Outline>();
            _outline.OutlineWidth = _trainConfigurationScriptableObject.OutlineIntensity;
            _outline.OutlineMode = Outline.Mode.OutlineAll;
            _outline.enabled = false;

            RegisterCallbacks();
        }

        public void CreatedScreen(GameObject srenePrefab, Vector3 position, Quaternion rotation, RailTrackManager railTrackManager)
        {
            _screen = Instantiate(srenePrefab, transform);
            _screen.transform.localPosition = position;
            _screen.transform.localRotation = rotation;

            var vagonMenuButtons = _screen.GetComponentInChildren<VagonMenuButtons>();
            vagonMenuButtons.SetActions(
                railTrackManager.DoMyself,
                railTrackManager.CallWorkers);
            _screen.SetActive(false);
        }

        private void RegisterCallbacks()
        {
            _interactiveObject.RegisterMouseEnter(OnMouseEnter);
            _interactiveObject.RegisterMouseExit(OnMouseExit);
            _interactiveObject.RegisterMouseUpAsButton(OnMouseClick);
        }
        private void OnMouseEnter()
        {
            _outline.OutlineColor = _trainConfigurationScriptableObject.OutlineColorDefault;
            _outline.enabled = true;
        }
        private void OnMouseExit()
        {
            _outline.enabled = false;
            _screen.SetActive(false);
        }
        private void OnMouseClick()
        {
            _outline.OutlineColor = _trainConfigurationScriptableObject.OutlineColorChosen;
            _screen.SetActive(true);
        }
    }
}
