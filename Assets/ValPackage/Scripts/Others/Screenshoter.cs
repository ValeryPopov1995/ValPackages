using NaughtyAttributes;
using System;
using System.IO;
using UnityEngine;
using ValeryPopov.Common.Extantions;

namespace ValeryPopov.Common
{
    public class Screenshoter : MonoBehaviour
    {
        [SerializeField] private KeyCode _keyDown = KeyCode.None;
        [SerializeField, Min(1)] private int _supersize = 1;
        private DirectoryInfo _directory;

        private void Update()
        {
            if (_keyDown != KeyCode.None && Input.GetKeyDown(_keyDown))
                Screenshot();
        }

        private void Awake()
        {
            _directory = new(Application.dataPath.Substring(0, Application.dataPath.Length - 6) + "Screenshots/");
            if (!_directory.Exists) _directory.Create();
        }

        [Button]
        public void Screenshot()
        {
            if (_directory == null) Awake();

            var fileName =
                _directory.FullName +
                "Screenshot " +
                DateTime.Now.ToString("MM-dd-yy HH-mm-ss")
                + ".png";

            ScreenCapture.CaptureScreenshot(fileName, _supersize);
            this.Log(fileName);
        }
    }
}