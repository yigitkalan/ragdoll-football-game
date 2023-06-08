using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Goal : MonoBehaviour
{
    [SerializeField] GameObject LeftGoal;
    [SerializeField] GameObject RightGoal;
    [SerializeField] Score score;
    public int goalCount = 0;
    public static Goal Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;

        LeftGoal = GameObject.FindGameObjectWithTag("LeftGoal");
        RightGoal = GameObject.FindGameObjectWithTag("RightGoal");
        score = GameObject.FindAnyObjectByType<Score>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(action(other));
    }
    IEnumerator action(Collider other)
    {
        yield return new WaitForSeconds(1.2f);
        if (other.gameObject == LeftGoal)
        {
            goalCount++;
            score.AddScore(score.team2);
            RoomManager.Instance.Spawn(score.team2);

            //timeManager.goalHandler(score.team2);
        }
        else if (other.gameObject == RightGoal)
        {
            goalCount++;
            score.AddScore(score.team1);
            RoomManager.Instance.Spawn(score.team1);
            //timeManager.goalHandler(score.team1);
        }
    }
}