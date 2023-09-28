using Data.Static.Trains;
using UI;
using Units.Interactive;
using Units.QuickOutline.Scripts;
using Units.Workers;
using UnityEngine;
using UnityEngine.Events;

namespace Units.Railway
{
    public class RailwayCarriageManager : MonoBehaviour
    {
        [SerializeField] private TrainConfigurationScriptableObject trainConfiguration;
        [SerializeField] private RailwayCarriageScriptableObject railwayCarriage;
        [SerializeField] private Transform workerPosition;
        private InteractiveObject _interactiveObject;
        private UnityAction _onCompletedSuccessful;
        private UnityAction _onCompletedNotSuccessful;
        private GameObject _screen;
        private Outline _outline;
        private bool _isTrainCompleted;
        private bool _isProgress;

        public void OnTrainArrived()
        {
            if (railwayCarriage.IsInteractive)
            {
                SettingInteractive();
                SettingOutline();
                CreatedScreen();
            }
            else
            {
                _onCompletedSuccessful?.Invoke();
            }
        }

        public void RegisterOnComplete(UnityAction onComplete) => _onCompletedSuccessful += onComplete;
        public void UnregisterOnComplete(UnityAction onComplete) => _onCompletedSuccessful -= onComplete;
        public void RegisterOnCompletedNotSuccessful(UnityAction onCompletedNotSuccessful) => _onCompletedNotSuccessful += onCompletedNotSuccessful;
        public void UnregisterOnCompletedNotSuccessful(UnityAction onCompletedNotSuccessful) => _onCompletedNotSuccessful -= onCompletedNotSuccessful;

        private void SettingOutline()
        {
            _outline = gameObject.AddComponent<Outline>();
            _outline.OutlineWidth = trainConfiguration.OutlineIntensity;
            _outline.OutlineMode = Outline.Mode.OutlineAll;
            _outline.enabled = false;
        }
        private void CreatedScreen()
        {
            _screen = Instantiate(RailsTracksManager.Instance.vagonButtonsScreen, transform);
            _screen.transform.localPosition = trainConfiguration.ScreenPosition;
            _screen.transform.localRotation = trainConfiguration.ScreenRotation;

            var vagonMenuButtons = _screen.GetComponentInChildren<VagonMenuButtons>();
            vagonMenuButtons.RegisterOnCallWorkers(() =>
            {
                _screen.SetActive(false);
                RailsTracksManager.Instance.CallWorkers(workerPosition,
                    () =>
                    {
                        _isTrainCompleted = true;
                        _onCompletedSuccessful?.Invoke();
                    },
                    () => _isProgress = true);
            });

            vagonMenuButtons.RegisterOnDoMyself(() =>
            {
                _screen.SetActive(false);
                _isProgress = true;
                RailsTracksManager.Instance.DoMyself(railwayCarriage.RailwayCarriageType,
                    () =>
                    {
                        _isTrainCompleted = true;
                        _onCompletedSuccessful?.Invoke();
                    },
                    () =>
                    {
                        _isProgress = false;
                        _onCompletedNotSuccessful?.Invoke();
                    });
            });

            _screen.SetActive(false);
        }

        private void SettingInteractive()
        {
            _interactiveObject = gameObject.AddComponent<InteractiveObject>();
            _interactiveObject.RegisterMouseEnter(OnMouseEnter);
            _interactiveObject.RegisterMouseExit(OnMouseExit);
            _interactiveObject.RegisterMouseUpAsButton(OnMouseClick);
        }

        private void OnMouseEnter()
        {
            if (_outline == null) return;
            _outline.OutlineColor = trainConfiguration.OutlineColorDefault;
            _outline.enabled = true;
        }

        private void OnMouseExit()
        {
            if (_outline == null) return;
            _outline.enabled = false;
            _screen.SetActive(false);
        }

        private void OnMouseClick()
        {
            if (_isTrainCompleted || _isProgress) return;
            _outline.OutlineColor = trainConfiguration.OutlineColorChosen;
            _screen.SetActive(true);
        }
    }
}
