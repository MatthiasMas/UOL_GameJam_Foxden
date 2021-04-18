using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Download : MonoBehaviour
{

    [SerializeField] private string wallpaperUrl = "https://thefoxden.de/spacesquid/";
    
    public void downloadWallpaper()
    {
        Application.OpenURL(this.wallpaperUrl);
    }
    
}
