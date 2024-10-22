using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    private Stack<GameObject> stack;
    public void Push(GameObject go)
    {
        stack.Push(go);
    }

    public void Pop()
    {
        stack.Pop().SetActive(true);
    }

    public void Peek()
    {
        Debug.Log(stack.Peek());
    }
    private void Awake()
    {
        if (gameManager != null && gameManager != this) 
        {
            Destroy(this.gameObject);
            gameManager = this;
        }
    }

    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
