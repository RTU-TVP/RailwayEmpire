#region

using UnityEngine;

#endregion

namespace Units.Train
{
    public class RailTrack : MonoBehaviour
    {
        [field: SerializeField] public Transform StartPoint { get; private set; }
        [field: SerializeField] public Transform StopPoint { get; private set; }
        [field: SerializeField] public Transform EndPoint { get; private set; }
        [field: SerializeField] public bool IsRailTrackAvailable { get; private set; }

        public void SetIsRailTrackAvailable(bool railTrackAvailable)
        {
            IsRailTrackAvailable = railTrackAvailable;
        }
    }
}
