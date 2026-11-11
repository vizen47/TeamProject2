using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class PortraitPresenter : DialoguePresenterBase
{
    [SerializeField] private Image portraitImage;
    [SerializeField] private CharacterPortraits[] characters;

    private CharacterPortraits currentSpeaker;

    public override YarnTask RunLineAsync(LocalizedLine line, LineCancellationToken token)
    {
        foreach (var character in characters)
        {
            if (character.speakerName != line.CharacterName)
                continue;

            string key = character == currentSpeaker ? "" : character.defaultExpression.key;

            foreach (string tag in line.Metadata)
            {
                if (tag.StartsWith("portrait:"))
                {
                    key = tag.Substring("portrait:".Length);
                    break;
                }
            }

            if (key == "")
                return YarnTask.CompletedTask;

            foreach (var expression in character.expressions)
            {
                if (expression.key != key)
                    continue;

                portraitImage.sprite = expression.sprite;
                portraitImage.enabled = true;
                currentSpeaker = character;
                return YarnTask.CompletedTask;
            }
        }

        HidePortrait();
        return YarnTask.CompletedTask;
    }

    public override YarnTask OnDialogueStartedAsync()
    {
        HidePortrait();
        return YarnTask.CompletedTask;
    }

    public override YarnTask OnDialogueCompleteAsync()
    {
        HidePortrait();
        return YarnTask.CompletedTask;
    }

    private void HidePortrait()
    {
        portraitImage.enabled = false;
        currentSpeaker = null;
    }
}
