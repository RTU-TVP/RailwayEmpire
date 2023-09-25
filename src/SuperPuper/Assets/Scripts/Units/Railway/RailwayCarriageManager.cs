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
        private UnityAction _onComplete;
        private bool _isTrainCompleted;
        private GameObject _screen;
        private Outline _outline;

        private void Start()
        {
            if (railwayCarriage.IsInteractive)
            {
                SettingInteractive();
                SettingOutline();
                CreatedScreen();
            }
            else
            {
                _onComplete?.Invoke();
            }
        }

        public void RegisterOnComplete(UnityAction onComplete) => _onComplete = onComplete;

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

            /*if (WorkersManager.Instance.WorkersCount >= WorkersManager.Instance._workersConfiguration.MaxWorkers)
            {
                vagonMenuButtons.ButtonCallWorkersGameObject.SetActive(false);
            }*/

            vagonMenuButtons.SetActions(
                () => RailsTracksManager.Instance.DoMyself(railwayCarriage.RailwayCarriageType,
                    () =>
                    {
                        _onComplete?.Invoke();
                        _isTrainCompleted = true;
                        _screen.SetActive(false);
                    }),
                () => RailsTracksManager.Instance.CallWorkers(workerPosition,
                    () =>
                    {
                        _onComplete?.Invoke();
                        _isTrainCompleted = true;
                        _screen.SetActive(false);
                    }));

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
            if (_isTrainCompleted) return;
            _outline.OutlineColor = trainConfiguration.OutlineColorChosen;
            _screen.SetActive(true);
        }
    }
}
