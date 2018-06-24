using UnityEngine;

namespace World
{
    public class SoundManager
    {
        private bool _isSoundOn;

        public bool IsSoundOn()
        {
            return _isSoundOn;
        }

        public void SetSoundOn(bool val)
        {
            _isSoundOn = val;
            PlayerPrefs.SetInt("sound", this._isSoundOn ? 1 : 0);
            PlayerPrefs.Save();
        }

        private SoundManager()
        {
            _isSoundOn = PlayerPrefs.GetInt("sound", 1) == 1;
        }

        public static SoundManager Instance = new SoundManager();
        
    }
}