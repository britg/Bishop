using UnityEngine;
using System.Collections;

public class AttackController : GameController {

    Player player;

    public float attackDuration = 0.4f;
    public float attackAnimation = 0.1f;
    bool isAnimating = false;
    float currentAttackTime = 0f;

    public Vector3 scaleAmount = new Vector3(1f,1f, 1f);


    bool attackInput {
        get {
            if (Paused) {
                return false;
            }
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) {
                return true;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Space)) {
                return true;
            }

            if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Began)) {
                return true;
            }
            return false;
        }
    }

    // Use this for initialization
    void Start () {
        player = GetPlayer();
    }

    // Update is called once per frame
    void Update () {
        if (attackInput && !isAnimating) {
            Attack();
        }

        if (player.isAttacking) {
            UpdateSwipeTime();
        }
    }

    void Attack () {
        currentAttackTime = 0f;
        player.isAttacking = true;
        isAnimating = true;

        // Do the pulse (change scale)
        iTween.ShakeScale(gameObject, iTween.Hash(
            "time", attackAnimation,
            "amount", scaleAmount, 
            "oncomplete", "OnAttackComplete"));

    }

    void OnAttackComplete () {
        isAnimating = false;
    }

    void UpdateSwipeTime () {
        currentAttackTime += Time.deltaTime;
        if (currentAttackTime >= attackDuration) {
            player.isAttacking = false;
            currentAttackTime = 0f;
        }
    }
}
