public class AutoAdvanceCard : TitleCard
{
    // Start is called before the first frame update
    void Start()
    {
        startFade = true;

        FindObjectOfType<DialogueHandler>().ProceedWithText();
    }
}
