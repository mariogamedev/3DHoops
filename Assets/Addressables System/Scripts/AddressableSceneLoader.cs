using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Addresables
{
    public class AddressableSceneLoader : IAddressableSceneLoader
    {
        private LoadSceneParameters _loadSceneParameters;
        private bool _isLoadOngoing;

        public AddressableSceneLoader()
        {
            InitializeDefaultLoadSceneParameters();
        }

        private void InitializeDefaultLoadSceneParameters()
        {
            _loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Single);
        }

        public void LoadSceneAsync(object key, bool activateOnLoad = true, int priority = 100)
        {
            if (!_isLoadOngoing)
            {
                _isLoadOngoing = true;
                AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(key, _loadSceneParameters, activateOnLoad, priority);
                handle.Completed += OnLoadSceneComplete; 
            }        
            else
            {
                Debug.Log("LoadSceneAsync request denied. Scene loading request already in progress.");
            }
        }

        public void LoadAssetAssync<T>(object key)
        {
            if (!_isLoadOngoing)
            {
                _isLoadOngoing = true;
                AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
                handle.Completed += OnLoadAssetComplete;
            }
            else
            {
                Debug.Log("LoadAssetAssync request denied. Asset loading request already in progress.");
            }
        }

        private void OnLoadAssetComplete<TObject>(AsyncOperationHandle<TObject> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Addressable asset " + handle.Result + " loaded successfully");
            }
            else if (handle.Status == AsyncOperationStatus.Failed)
            {
                Debug.Log("Addressable asset failed on load with the following exception: " + handle.OperationException.Message);
            }
            else if (handle.Status == AsyncOperationStatus.None)
            {
                Debug.Log("Addressable asset failed on load with the following exception: " + handle.OperationException.Message);
            }
        }

        private void OnLoadSceneComplete(AsyncOperationHandle<SceneInstance> sceneInstanceHandle)
        {
            if (sceneInstanceHandle.Status == AsyncOperationStatus.Succeeded)
            {
                OnSucceed(sceneInstanceHandle.Result.Scene.name);
            }
            else if (sceneInstanceHandle.Status == AsyncOperationStatus.Failed)
            {
                OnFailed(sceneInstanceHandle.OperationException.Message);
            }
            else if (sceneInstanceHandle.Status == AsyncOperationStatus.None)
            {
                OnNone(sceneInstanceHandle.OperationException.Message);
            }
            
            _isLoadOngoing = false;
        }

        private void OnSucceed(string sceneName)
        {
            Debug.Log("Addressable scene " + sceneName + " loaded successfully");
        }

        private void OnFailed(string message)
        {
            Debug.Log("Addressable scene failed on load with the following exception: " + message);
        }

        private void OnNone(string message)
        {
            Debug.Log("Addressable scene result on None with the following exception: " + message);
        }
    }
}