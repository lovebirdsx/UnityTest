using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
public class Player : MonoBehaviour {
    public int maxHp = 100;
    public int hp = 100;
    public Rigidbody bullet;
    public bool isRobot = false;

    public float bulletRange = 5;
    public float bulletFlyTime = 1;

    NavMeshAgent agent;
    float lastShootTime;
    float lastMoveTime;
    float shootCd = 1;

    RaycastHit m_HitInfo = new RaycastHit();

    public bool IsMoveFinished {
        get {
            // 超过2秒都没有移动,则当成移动完成
            // 主要是为了处理一些莫名其妙的被卡住的情况
            if (Time.time - lastMoveTime > 2.0f)
                return true;

            return !agent.pathPending && (agent.remainingDistance <= agent.stoppingDistance) && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
        }
    }

    public bool CanShoot { get { return Time.time - lastShootTime >= shootCd; } }
    public bool IsDead { get { return hp == 0; } }
    public int Hp {get {return hp;} }

    void Start() {
        agent = GetComponent<NavMeshAgent>();

        if (!isRobot) {
            var tree = GetComponent<BehaviorTree>();
            tree.enabled = false;
        }
    }

    void Update() {
        // 机器人不响应玩家的操作
        if (isRobot)
            return;

        // 避免响应UI的消息
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // 左键移动
        if (Input.GetMouseButtonDown(0)) {
            var ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
                agent.destination = m_HitInfo.point;
        }

        // 右键攻击
        if (Input.GetMouseButtonDown(1)) {
            var ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo)) {
                Vector3 shootPos = m_HitInfo.point;
                shootPos.y = transform.position.y;
                if (CanShoot) {
                    Shoot(shootPos);
                }
            }
        }
    }

    public void Move(Vector3 pos) {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(pos, out hit, 0.2f, 1)) {
            agent.destination = hit.position;
            lastMoveTime = Time.time;
        }
    }

    public void Shoot(Vector3 pos) {
        Quaternion rotation = Quaternion.FromToRotation(transform.position, pos);
        Vector3 direction = Vector3.Normalize(pos - transform.position);
        Vector3 spawnPos = transform.position + direction * 0.5f;

        Rigidbody clonedBullet = Instantiate(bullet, spawnPos, rotation) as Rigidbody;

        float speed = bulletRange / bulletFlyTime;
        clonedBullet.velocity = speed * direction;
        Destroy(clonedBullet.gameObject, bulletFlyTime);

        lastShootTime = Time.time;
    }

    public void TakeDamage(int damage) {
        hp -= damage;
        if (hp < 0)
            hp = 0;
    }
}