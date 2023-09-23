using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/Emoji", order = 1)]
public class CharactersEmoji : ScriptableObject
{
    [SerializeField] GameObject _anger;
    [SerializeField] GameObject _happiness;
    [SerializeField] GameObject _sadness;
    [SerializeField] GameObject _sleepy;
    [SerializeField] GameObject _scared;
    GameObject emo;
    Dictionary<EmojiType, GameObject> emojiDictionary = new Dictionary<EmojiType, GameObject>();
    public enum EmojiType
    {
        Anger,
        Happiness,
        Sadness,
        Sleepy,
        Scared
    }
    public void Emoji(EmojiType emojiType,GameObject emotingCharacter, int emotionTime)
    {
        if(emojiDictionary.Count == 0)
        {
            emojiDictionary.Add(EmojiType.Anger, _anger);
            emojiDictionary.Add(EmojiType.Happiness, _happiness);
            emojiDictionary.Add(EmojiType.Sleepy, _sleepy);
            emojiDictionary.Add(EmojiType.Scared, _scared);
            emojiDictionary.Add(EmojiType.Sadness, _sadness);
        }
        emo = Instantiate(emojiDictionary[emojiType], emotingCharacter.transform);
        EmojiTimer(emotionTime);
        
    }
    IEnumerator EmojiTimer(int time)
    {
        yield return new WaitForSeconds(time);
        yield break;
    }
    public void DestroyEmotion()
    {
        Destroy(emo);
    }
}
