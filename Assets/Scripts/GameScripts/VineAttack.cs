using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TDController;
using UnityEngine;

public class VineAttack : MonoBehaviour
{
    private EnemyController[] enemies;
    private EnemyController[] sortedEnemies;
    public GameObject vinePrefab;
    public float stunTime;
    private PlayerInput input;
    private PlayerSkills playerSkills;
    public float attackDistance;
    private bool growthEnabled=true;
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        playerSkills = GetComponent<PlayerSkills>();
        
    }
    void Start()
    {
        GameEvents.current.onDeath += Death;
        GameEvents.current.onGameStop += DisableGrowth;
        GameEvents.current.onGameStart += EnableGrowth;
    }
    private void OnDestroy()
    {
        GameEvents.current.onDeath -= Death;
        GameEvents.current.onGameStop -= DisableGrowth;
        GameEvents.current.onGameStart -= EnableGrowth;
    }
    private void Death()
    {
        DisableGrowth();
        GameEvents.current.onGameStop -= DisableGrowth;
        GameEvents.current.onGameStart -= EnableGrowth;
    }
    private void DisableGrowth()
    {
        growthEnabled = false;
    }
    private void EnableGrowth()
    {
        growthEnabled = true;
    }
    void Update()
    {
        //print(playerSkills.GetSkill(SkillColor.Green) == Skills.GreenSkill1);
        if (input.FrameInput.Growth &&
            playerSkills.GetSkill(SkillColor.Green) == Skills.GreenSkill1&&
            growthEnabled)

        {
            enemies =FindObjectsByType<EnemyController>(FindObjectsSortMode.InstanceID);
          
            sortedEnemies= enemies.OrderBy(enemy=> Vector3.Distance(transform.position, enemy.transform.position)).ToArray();
            
            var enemyToStuck = GetUnstuckedEnemy(sortedEnemies);
            if (!enemyToStuck)
                print("I HAVE NO ENEMIES");
            if (enemyToStuck)
            if (Vector3.Distance(transform.position, enemyToStuck.transform.position)<=attackDistance)
                {
                    
                    enemyToStuck.Stuck(stunTime);
                    StartCoroutine(SpawnVine(enemyToStuck.spotToGrow));
                }
               
            
        }

    }
    private EnemyController GetUnstuckedEnemy(EnemyController[] enemies)
    {
        int i = 0;
        while (enemies.Length>i)
        {
            if (!enemies[i].isStuck)
                return enemies[i];
            i++;
        }
        return null;
    }
    private IEnumerator SpawnVine(Transform parent)
    {
        var vine = Instantiate(vinePrefab,parent.position,Quaternion.identity);
        vine.transform.parent = parent;
        print("Instantiated");
        yield return new WaitForSeconds(stunTime);
        print("Destroyed");
        Destroy(vine);
        yield return null;
    }
}
