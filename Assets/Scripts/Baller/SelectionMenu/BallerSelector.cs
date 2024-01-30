using System;
using UnityEngine;
using UnityEngine.UI;

namespace Baller
{
    [Serializable]
    public class BallerSelector : MonoBehaviour
    {      
        [SerializeField]
        private Image _profile;
        [SerializeField]
        private Button _selectionButton;

        public Image Profile => _profile;
        public int ID { get; set; }

        public event Action<int> SelectedAction = delegate { };

        private void Awake()
        {
            _selectionButton.onClick.AddListener(OnSelectionButtonClick);
        }

        private void OnSelectionButtonClick()
        {
            SelectedAction.Invoke(ID);
        }

        public void DisableInteraction()
        {
            _selectionButton.interactable = false;
        }

        public void EnableInteraction()
        {
            _selectionButton.interactable = true;
        }
    }
}
