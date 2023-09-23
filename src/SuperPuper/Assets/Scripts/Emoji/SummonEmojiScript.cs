using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEmojiScript : MonoBehaviour
{
    [SerializeField] CharactersEmoji ch;
    public void MakeEmoji(CharactersEmoji.EmojiType emojiType, GameObject emotingCharacter, int secondsForEmotion)
    {
        ch.Emoji(emojiType, emotingCharacter, secondsForEmotion);
        StartCoroutine(EmojiVanishTimer(secondsForEmotion));
    }
    IEnumerator EmojiVanishTimer(int time)
    {
        yield return new WaitForSeconds(time);
        ch.DestroyEmotion();
        yield break;
    }
}
