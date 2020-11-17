using UnityEngine;

namespace Menu.Screens
{
    public class GameScreen : MonoBehaviour
    {
        [SerializeField] private GameObject pauseLayer = default; 
        
        public void TogglePause()
        {
            pauseLayer.SetActive(!pauseLayer.activeSelf);
        }
    }
}
