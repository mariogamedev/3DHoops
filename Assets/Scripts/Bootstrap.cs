using Addresables;
using UnityEngine;

namespace Baller
{
    public class Bootstrap : MonoBehaviour
    {
        AddressableSceneLoader _addressableSceneLoader;

        void Awake()
        {
            _addressableSceneLoader = new AddressableSceneLoader();
        }

        void Start()
        {
            _addressableSceneLoader.LoadSceneAsync("MainMenu");
        }
    }
}