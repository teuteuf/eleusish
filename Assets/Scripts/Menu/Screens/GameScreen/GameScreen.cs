using Game;
using UnityEngine;

namespace Menu.Screens.GameScreen
{
    public class GameScreen : MonoBehaviour
    {
        [SerializeField] private GameObject pauseLayer = default; 
        
        public void TogglePause()
        {
            pauseLayer.SetActive(!pauseLayer.activeSelf);
            TimeManager.instance.TogglePause();
        }
    }
}
