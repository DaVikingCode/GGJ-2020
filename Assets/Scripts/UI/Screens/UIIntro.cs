using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIntro : BaseUIScreen
{
    public UICard mainCard;
    public UiKeyboardKey leftKey;
    public UiKeyboardKey rightKey;
    public UiKeyboardKey upKey;
    public UiKeyboardKey downKey;

    public void StartAnimation(System.Action onComplete)
    {
        CardData cd = GameManager.instance.deckHandler.getRandomCard();
        mainCard.SetSymbol(cd.front);
        mainCard.HideCard();

        StartCoroutine(IntroAnimation(onComplete));
    }

    IEnumerator IntroAnimation(System.Action onComplete)
    {
        yield return new WaitForSeconds(1f);
        mainCard.PopCard();

        while(mainCard.isAnimating)
        {
            yield return null;
        }

		rightKey.Pop(true);
		yield return new WaitForSeconds(0.2f);
		rightKey.Pop(false);

		mainCard.SwipeRight();

		while(mainCard.isAnimating)
		{
			yield return null;
		}

		CardData cd = GameManager.instance.deckHandler.getRandomCard();
		mainCard.SetSymbol(cd.front);
		mainCard.HideCard();

		yield return new WaitForSeconds(1f);

		mainCard.PopCard();

		while(mainCard.isAnimating)
		{
			yield return null;
		}

		leftKey.Pop(true);
		yield return new WaitForSeconds(0.2f);
		leftKey.Pop(false);

		mainCard.SwipeLeft();

		while(mainCard.isAnimating)
		{
			yield return null;
		}

        yield return new WaitForSeconds(1f);
        onComplete?.Invoke();
    }
}
