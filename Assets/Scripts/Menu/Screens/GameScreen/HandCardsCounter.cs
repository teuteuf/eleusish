using UnityEngine;
using UnityEngine.UI;

namespace Menu.Screens.GameScreen
{
    public class HandCardsCounter : MonoBehaviour
    {
        [SerializeField] private Text uiCounter = default;

        public void UpdateCardCounter(int nbCards)
        {
            uiCounter.text = nbCards.ToString();
        }
    }
}
