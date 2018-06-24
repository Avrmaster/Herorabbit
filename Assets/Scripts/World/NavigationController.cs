using UnityEngine;
using UnityEngine.SceneManagement;

namespace World
{
    public class NavigationController : MonoBehaviour
    {
        public static void OnPlayPress()
        {
            SceneManager.LoadScene("LevelSelector");
        }

        public static void OnSettingsPress()
        {
        }
    }
}