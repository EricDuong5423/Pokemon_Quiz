using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterQuestion : MonoBehaviour
{
    [SerializeField] Text questionText;
    [SerializeField] Image backgroundImage;

    public IEnumerator TypeDialog(string dialog)
    {
        questionText.text = "";
        foreach (var letter in dialog.ToCharArray())
        {
            questionText.text += letter;
            yield return new WaitForSeconds(1f / 30);
        }
    }

    public void EnableMonsterQuestion(bool enable)
    {
        questionText.enabled = enable;
        backgroundImage.enabled = enable;
    }
}
