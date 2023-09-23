using System;
using Data.Static.Trains;
using Interactive;
using QuickOutline.Scripts;
using UnityEngine;

namespace Train
{
    public class RailwayCarriageManager : MonoBehaviour
    {
        [SerializeField] private RailwayCarriageScriptableObject _railwayCarriageScriptableObject;
        [SerializeField] private TrainConfigurationScriptableObject _trainConfigurationScriptableObject;
        private Outline _outline;
        private InteractiveObject _interactiveObject;

        private void Awake()
        {
            if (_railwayCarriageScriptableObject.IsInteractive == false) return;
            
            _interactiveObject = gameObject.AddComponent<InteractiveObject>();
            _outline = gameObject.AddComponent<Outline>();
            _outline.OutlineWidth = _trainConfigurationScriptableObject.OutlineIntensity;
            _outline.OutlineMode = Outline.Mode.OutlineAll;
            _outline.enabled = false;
        }

        private void Start()
        {
            RegisterCallbacks();
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
        private void OnMouseExit() => _outline.enabled = false;
        private void OnMouseClick()
        {
            _outline.OutlineColor = _trainConfigurationScriptableObject.OutlineColorChosen;
        }
    }
}
