using System.Collections.Generic;
using UnityEngine;

namespace Baller
{
    [CreateAssetMenu(fileName = "MatchAudiosConfiguration", menuName = "Match/Audios", order = 0)]
    public class MatchAudiosConfiguration : ScriptableObject
    {
        private Dictionary<MatchAudio, AudioClip> _audios;

        [SerializeField] 
        private MatchAudio[] _matchAudios;
    }

    public enum MatchAudioType
    {
        Success
    }

    [System.Serializable]
    public class MatchAudio
    {
        [SerializeField]
        private MatchAudioType _matchAudio;
        [SerializeField]
        private AudioClip _audioClip;
    }
}