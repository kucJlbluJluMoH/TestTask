using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PopupService : IDisposable
{
    private const string PopupKey = "Popup";
    private const string ButtonKey = "PopupButton";

    private readonly Transform _root;

    private AsyncOperationHandle<GameObject> _popupHandle;
    private AsyncOperationHandle<GameObject> _buttonHandle;
    private PopupView _currentPopup;
    private bool _isLoading;

    public PopupService(Transform root)
    {
        _root = root;
    }

    public void Show(PopupData data)
    {
        if (_isLoading)
            return;

        if (_popupHandle.IsValid() && _buttonHandle.IsValid())
        {
            SpawnPopup(data);
            return;
        }

        _isLoading = true;
        LoadAssets(data);
    }

    public void Close()
    {
        _currentPopup?.Close();
        _currentPopup = null;
    }

    public void Dispose()
    {
        Close();
        if (_popupHandle.IsValid()) Addressables.Release(_popupHandle);
        if (_buttonHandle.IsValid()) Addressables.Release(_buttonHandle);
    }

    private void LoadAssets(PopupData data)
    {
        int pending = 2;

        Addressables.LoadAssetAsync<GameObject>(PopupKey).Completed += handle =>
        {
            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"[PopupService] Failed to load '{PopupKey}'");
                _isLoading = false;
                return;
            }
            _popupHandle = handle;
            if (--pending == 0) OnAssetsLoaded(data);
        };

        Addressables.LoadAssetAsync<GameObject>(ButtonKey).Completed += handle =>
        {
            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"[PopupService] Failed to load '{ButtonKey}'");
                _isLoading = false;
                return;
            }
            _buttonHandle = handle;
            if (--pending == 0) OnAssetsLoaded(data);
        };
    }

    private void OnAssetsLoaded(PopupData data)
    {
        _isLoading = false;
        SpawnPopup(data);
    }

    private void SpawnPopup(PopupData data)
    {
        Close();
        GameObject go = GameObject.Instantiate(_popupHandle.Result, _root);
        _currentPopup = go.GetComponent<PopupView>();
        _currentPopup.Setup(data, _buttonHandle.Result);
    }
}
