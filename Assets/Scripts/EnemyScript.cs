using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private float phase;
    public GameObject explosion;
    private GameControllerScript gameController;

    // Start is called before the first frame update
    void Start()
    {
        phase = Random.Range(0f, Mathf.PI * 2);
        gameController = GameObject
            .FindWithTag("GameController")
            .GetComponent<GameControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(
            Mathf.Cos(Time.frameCount * 0.05f + phase) * 0.05f,
            -2f * Time.deltaTime, 
            0f
            );
        //Cosの方が横の振れが大きい。Sinの方が縦に軸がある感じがする。
        //Cosはゴーストだと横ゆらゆら。Sinだと縦ゆらゆら。--Youtube
        //数学のトリセツ「三角関数のグラフ【数学ⅡB・三角関数】」で理解！Unityだと(例:ゴースト)これが逆。
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            gameController.AddScore(10);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            gameController.GameOver();
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(explosion, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
