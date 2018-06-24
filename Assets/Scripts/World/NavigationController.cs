using UnityEngine;
using UnityEngine.SceneManagement;

namespace World
{
    public class NavigationController : MonoBehaviour
    {
        public void OnPlayPress()
        {
            SceneManager.LoadScene("LevelSelector");
        }

        public void OnSettingsPress()
        {
        }
    }
}