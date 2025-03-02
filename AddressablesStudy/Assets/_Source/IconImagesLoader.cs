using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class IconImagesLoader : MonoBehaviour
{
    [SerializeField] private AssetLabelReference icons;

    [Space]
    [SerializeField] private Image iconImagePrefab;
    [SerializeField] private Transform iconImagesRoot;

    private readonly List<Image> _iconImages = new();

    private AsyncOperationHandle<IList<Sprite>> _loadedIcons;

    public void LoadIconImages()
    {
        LoadIcons();
    }

    public void ReleaseIconImages()
    {
        DestroyIconImages();
        ReleaseIcons();
    }

    private void LoadIcons()
    {
        if (_loadedIcons.IsValid()) return;

        _loadedIcons = Addressables.LoadAssetsAsync<Sprite>(icons, InstantiateIconImage);
    }

    private void InstantiateIconImage(Sprite icon)
    {
        Image iconImage = Instantiate(iconImagePrefab, iconImagesRoot);

        iconImage.sprite = icon;

        _iconImages.Add(iconImage);
    }

    private void ReleaseIcons()
    {
        if (!_loadedIcons.IsValid()) return;

        Addressables.Release(_loadedIcons);
        Debug.Log("Icons Released");
    }

    private void DestroyIconImages()
    {
        foreach (Image iconImage in _iconImages)
        {
            Destroy(iconImage.gameObject);
        }

        _iconImages.Clear();
    }
}
