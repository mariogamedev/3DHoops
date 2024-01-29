using MVC;
using UnityEngine;
using UnityEngine.UI;

namespace Baller
{
    public class MainMenu : View
    {
        [SerializeField]
        private Button _start;

        private void Awake()
        {
            _start.onClick.AddListener(OnStartClick);
        }

        private void OnStartClick()
        {
            InvokeAction<StartGameAction>();
        }
    }
}
