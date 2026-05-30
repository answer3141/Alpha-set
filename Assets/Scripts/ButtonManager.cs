using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StageSelect()
    {
        SceneManager.LoadScene("StageSelectScene");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void Setting()
    {
        SceneManager.LoadScene("SettingScene");
    }

    public void Title()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void Stage1()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void Stage2()
    {
        SceneManager.LoadScene("Stage2");
    }
    public void Stage3()
    {
        SceneManager.LoadScene("Stage3");
    }
    public void Stage4()
    {
        SceneManager.LoadScene("Stage4");
    }
    public void Stage5()
    {
        SceneManager.LoadScene("Stage5");
    }
    public void Stage6()
    {
        SceneManager.LoadScene("Stage6");
    }
    public void Stage7()
    {
        SceneManager.LoadScene("Stage7");
    }
    public void Stage8()
    {
        SceneManager.LoadScene("Stage8");
    }
}
