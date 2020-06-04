using System;
using TMPro;
using UnityEngine;

namespace Game.CardComponents
{
    public class CardVisual : MonoBehaviour
    {
        [SerializeField] private Material materialClubs = default;
        [SerializeField] private Material materialDiamonds = default;
        [SerializeField] private Material materialHearts = default;
        [SerializeField] private Material materialSpades = default;

        [SerializeField] private MeshRenderer suiteMeshRenderer = default;
        [SerializeField] private TextMeshPro valueText = default;

        public void Set(CardValue value)
        {
            valueText.text = value.Rank.GetStringValue();
            suiteMeshRenderer.material = GetSuiteMaterial(value.Suite);
        }

        private Material GetSuiteMaterial(CardSuite suite)
        {
            switch (suite)
            {
                case CardSuite.Clubs:
                    return materialClubs;
                case CardSuite.Diamonds:
                    return materialDiamonds;
                case CardSuite.Hearts:
                    return materialHearts;
                case CardSuite.Spades:
                    return materialSpades;
                default:
                    throw new ArgumentOutOfRangeException(nameof(suite), suite, "No material.");
            }
        }
    }
}