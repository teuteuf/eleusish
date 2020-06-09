using System.Collections.Generic;
using Game.CardComponents;
using UnityEngine;

public class GuessingLine : MonoBehaviour
{
    [SerializeField] private float spaceBetweenCards = 0.5f;

    private List<Card> _cards = new List<Card>();

    public void AddCard(Card card)
    {
        card.transform.parent = transform;
        _cards.Add(card);
        OrganizeCards();
    }

    private void OrganizeCards()
    {
        var guessingLineStartPosition = transform.position;
        for (var i = 0; i < _cards.Count; i++)
        {
            _cards[i].transform.position = new Vector3(
                guessingLineStartPosition.x + i * spaceBetweenCards,
                guessingLineStartPosition.y + 0.001f * i,
                guessingLineStartPosition.z
            );
            _cards[i].transform.rotation = transform.rotation;
        }
    }
}
