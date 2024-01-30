using Addresables;
using UnityEngine;
using UnityEngine.UI;

namespace Baller
{
    public class RestartMatch : MonoBehaviour
    {
        [SerializeField]
        private Button _restartButton;

        private AddressableSceneLoader _addressablesSceneLoader;

        private void Awake()
        {
            _addressablesSceneLoader = new AddressableSceneLoader();
            _restartButton.onClick.AddListener(OnRestart);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                OnRestart();
            }
        }

        private void OnRestart()
        {
            _addressablesSceneLoader.LoadSceneAsync("Match");
        }
    }
}