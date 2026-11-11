using UnityEngine;
using Yarn.Unity;

[AddComponentMenu("Dialogue/Dialogue Frame Presenter")]
public sealed class DialogueFramePresenter : DialoguePresenterBase
{
    [SerializeField] private GameObject frame;
    [SerializeField] private GameObject continueArea;
    [SerializeField] private CanvasGroup lastLineText;
    [SerializeField, Min(0f)] private float lastLineFadeDuration = 0.25f;

    private void Awake()
    {
        HideFrame();
    }

    public override YarnTask OnDialogueStartedAsync()
    {
        frame.SetActive(true);
        return YarnTask.CompletedTask;
    }

    public override YarnTask RunLineAsync(LocalizedLine line, LineCancellationToken token)
    {
        lastLineText.alpha = 0f;
        continueArea.SetActive(true);
        return YarnTask.CompletedTask;
    }

    public override async YarnTask<DialogueOption> RunOptionsAsync(
        DialogueOption[] dialogueOptions, LineCancellationToken token)
    {
        continueArea.SetActive(false);
        await Effects.FadeAlphaAsync(lastLineText, 0f, 1f, lastLineFadeDuration, token.HurryUpToken);
        return null;
    }

    public override YarnTask OnDialogueCompleteAsync()
    {
        HideFrame();
        return YarnTask.CompletedTask;
    }

    private void HideFrame()
    {
        frame.SetActive(false);
        continueArea.SetActive(false);
        lastLineText.alpha = 0f;
    }
}
