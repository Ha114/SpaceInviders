using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
    [SerializeField]
    private Text ScoreText;
    private int score = 0;

    Vector3 min;
    Vector3 max;

    
   // private float objectWidth;


    public int currentEnemyDestroyed = 0;

    #region parametres_bullet_change_feature
    //bullet change for feature
    public float speed = 7f;
    public Color color = Color.white;
    //for update bullet
    public Button UpdateBulletButton;
    Image image;
    Text textCount;
    //bool for update shooting
    bool isCanUpdate = false;
    #endregion

    private void Start()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        image = UpdateBulletButton.gameObject.GetComponent<Image>();
        textCount = UpdateBulletButton.transform.GetChild(0).GetComponent<Text>();
        StartCoroutine(WaitUpdateBullet());
    }

    #region bullet_change_feature
    public void Change() { StartCoroutine(ChangeBullet()); }
    IEnumerator ChangeBullet()
    {
        newBullet(7f, Color.red, 1.5f);
        for (int i = 1; i <= 5; i++)
        {
            textCount.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        newBullet(-7f, Color.white, 2f);
    }
    void newBullet(float s, Color c, float i)
    {
        speed += s;
        color = c;
        Shoot.instance.interval = i;
    }
    public void UpdateBullet()
    {
        if (isCanUpdate)
        {
            GameManager.instance.Change();
            isCanUpdate = false;
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(WaitUpdateBullet());
    }
    IEnumerator WaitUpdateBullet()
    {
        for (int i = 1; i <= 20; i++)
        {
            image.color = new Color(214 / 255f, 0f, 4 / 255f, 156 / 255f);
            textCount.color = Color.white;
            textCount.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        textCount.text = "go";
        isCanUpdate = true;
        image.color = new Color(160 / 255f, 248 / 255f, 38 / 255f, 156 / 255f);
        textCount.color = Color.black;
    }
    #endregion

    //Score Controller
    public void UpdateScore(int i)
    {
        score += i;
        ScoreText.text = "points: " + score.ToString();
        if (score < 0)
        {
            SaveLastResult();
            SceneManager.LoadScene(2); //GameOverScene
        }
    }
    public void SaveLastResult()
    {
        SaveSystem.instance.SaveLastOne(score);
    }
    public void UpdateDestroyEnemies()
    {
        if (++currentEnemyDestroyed == Spawner.instance.enemiesEnemy.Length)
        {
            SaveLastResult();
            SceneManager.LoadScene(3); //WinScene
        }
    }


}
