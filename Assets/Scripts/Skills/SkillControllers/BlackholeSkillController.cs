using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackholeSkillController : MonoBehaviour
{
    public float maxSize = 15f;
    public float growSpeed = 3f;
    public int attackCnt = 3;
    public float attackInterval = .3f;
    public bool isAttacking = false;
    public bool isOpendingBlackhole = true;
    public List<Enemy> enemyList = new();

    private Player player;

    private void Start() {
        player = PlayerManager.instance.player;
    }

    private void Update() {
        if (isOpendingBlackhole) {
            transform.localScale = Vector2.Lerp(transform.localScale, new(maxSize, maxSize), growSpeed * Time.deltaTime);
            if (transform.localScale.x >= maxSize - 0.1f && !isAttacking) {
                isAttacking = true;
                player.StartCoroutine(AttackCoroutine());
            }
        } else {
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, growSpeed * Time.deltaTime * 2);
            if (transform.localScale.x <= 0.1f) {
                foreach (Enemy enemy in enemyList) {
                    enemy.FreezeTime(false);
                }
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<Enemy>(out var enemy)) {
            enemy.FreezeTime(true);
            enemyList.Add(enemy);
        }
    }

    private IEnumerator AttackCoroutine() {
        for (int i = 0; i < attackCnt; i++) {
            int enemyCount = enemyList.Count;
            for (int j = 0; j < enemyCount; j++) {
                if (j >= enemyList.Count) {
                    break;
                }
                Enemy enemy = enemyList[j];
                int facingDir = Random.Range(0, 2) == 0 ? -1 : 1;
                Vector3 clonePos = enemy.transform.position + new Vector3(facingDir, 0, 0);
                player.skill.clone.CreateClone(clonePos, enemy.transform.rotation, facingDir);
                yield return new WaitForSeconds(attackInterval);
            }
        }
        isOpendingBlackhole = false;
    }
}
