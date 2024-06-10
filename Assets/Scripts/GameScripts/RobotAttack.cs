using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDController;
public class RobotAttack : MonoBehaviour
{
    private PlayerController playerController;
    private AnimationController animController;
    [SerializeField] private Transform center;
    [SerializeField] float attackRadius;
    [SerializeField] float sphereRadius;
    [SerializeField] private float defaultDamage;
    [SerializeField] private float buffedDamage;
    private float damage;
    private LayerMask damagableLayers;
    private Vector2 spherePos;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animController = GetComponentInChildren<AnimationController>();
        damagableLayers = LayerMask.GetMask("Enemy");
    }
    private void Start()
    {
        playerController.Attacked += Attack;
        GameEvents.current.onDeath += DisableAttack;
    }
    public void SetBuffedDamage()
    {
        damage = buffedDamage;
    }
    public void SetDefaultDamage()
    {
        damage = defaultDamage;
    }
    private void DisableAttack()
    {
        playerController.Attacked -= Attack;
    }
    public void Attack()
    {
        //print("Атаковал");
        AudioController.current.PlayWhipAttackSound();
        spherePos = new Vector2(center.position.x + (attackRadius * animController.GetFacingVector()), center.position.y);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(spherePos, sphereRadius, damagableLayers);
        foreach (var hitCollider in hitColliders)
        {
            //print("Атаковал2: "+hitCollider.name);
            EnemyController tmp;
            if(hitCollider.TryGetComponent(out tmp)&& hitCollider.isTrigger)
            {
                AudioController.current.PlayHitSound();
                hitCollider.gameObject.GetComponent<HealthController>().TakeDamage(damage);
            }
           
        }
    }
    private void OnDestroy()
    {
        playerController.Attacked -= Attack;
        GameEvents.current.onDeath -= DisableAttack;
    }
    private void OnDisable()
    {
        playerController.Attacked -= Attack;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, sphereRadius);
    }
}
