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

            if (WorkersManager.Instance.WorkersCount >= WorkersManager.Instance._workersConfiguration.MaxWorkers)
            {
                vagonMenuButtons.ButtonCallWorkersGameObject.SetActive(false);
            }
            
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

        public static Data.Static.Trains.Train GenerateTrain(RailwayCarriagesDatabaseScriptableObject railwayCarriagesDatabaseScriptableObject)
        {
            int count = Random.Range(4, 7);

            var railwayCarriages = new RailwayCarriageScriptableObject[count];
            railwayCarriages[0] = railwayCarriagesDatabaseScriptableObject.GetRailwayCarriage(RailwayCarriageType.Locomotive);
            for (int i = 1; i < count; i++)
            {
                railwayCarriages[i] = railwayCarriagesDatabaseScriptableObject.GetRandomRailwayCarriage();
                if (railwayCarriages[i].RailwayCarriageType == RailwayCarriageType.Locomotive)
                {
                    i--;
                }
            }

            return new Data.Static.Trains.Train(railwayCarriages);
        }

        public static GameObject CreateRailwayCarriage(GameObject prefab, Transform parent, int count)
        {
            var railwayCarriage = Instantiate(prefab, parent);
            railwayCarriage.transform.position = parent.position - new Vector3(16.5f * count, 0, 0);
            railwayCarriage.transform.rotation = Quaternion.Euler(0, 90, 0);
            return railwayCarriage;
        }

        public void RegisterOnComplete(UnityAction onComplete) => _onComplete = onComplete;

        private void SettingInteractive()
        {
            _interactiveObject = gameObject.AddComponent<InteractiveObject>();
            _interactiveObject.RegisterMouseEnter(OnMouseEnter);
            _interactiveObject.RegisterMouseExit(OnMouseExit);
            _interactiveObject.RegisterMouseUpAsButton(OnMouseClick);
        }

        private void OnMouseEnter()
        {
            if (_outline == null)
            {
                return;
            }
            
            _outline.OutlineColor = trainConfiguration.OutlineColorDefault;
            _outline.enabled = true;
        }

        private void OnMouseExit()
        {
            if (_outline == null)
            {
                return;
            }
            
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
