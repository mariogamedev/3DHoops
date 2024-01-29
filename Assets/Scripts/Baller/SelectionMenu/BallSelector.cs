using System;
using UnityEngine;
using UnityEngine.UI;

namespace Baller
{
    [Serializable]
    public class BallSelector : MonoBehaviour
    {
        [SerializeField]
        private Image _profile;
        [SerializeField]
        private Button _selectionButton;

        public event Action<int> SelectedAction = delegate { };

        public int ID { get; set; }
        public Image Profile => _profile;

        private void Awake()
        {
            _selectionButton.onClick.AddListener(OnSelectionButtonClick);
        }

        private void OnSelectionButtonClick()
        {
            SelectedAction.Invoke(ID);
        }
    }
}
