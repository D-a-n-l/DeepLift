using NTC.Pool;
using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidbod;

    [SerializeField]
    private Collider2D collider2d;

    [SerializeField]
    private PresetSpriteRenderer sprite;

    [SerializeField] 
    private float speed;

    [SerializeField]
    private float damage;

    [SerializeField]
    private bool isCheckDeathZone = false;

    [ShowIf(nameof(isCheckDeathZone))]
    [SerializeField]
    private string tagDeathZone;

    [ShowIf(nameof(isCheckDeathZone))]
    [SerializeField]
    private float distanceToPlayer;

    private PlayerMovement player;

    private WaitForSeconds waitForSeconds = new WaitForSeconds(4f);

    private bool isReady = true;

    private void Start()
    {
        rigidbod.linearVelocity = transform.right * speed;
    }

    private void OnEnable()
    {
        rigidbod.linearVelocity = transform.right * speed;

        StartCoroutine(ResetPosition());
    }

    private IEnumerator ResetPosition()//was a bag... bullet wrong position shot
    {
        yield return new WaitForEndOfFrame();

        rigidbod.linearVelocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (isReady == true)
        {
            if (col.gameObject.TryGetComponent(out HealthControl entity))
            {
                NightPool.Despawn(gameObject);

                if (col.collider.TryGetComponent(out HitBox hitBox))
                {
                    col.gameObject.GetComponent<ParticleManagement>().SetPositionAndRotation(DetectionContactPoint.Position(col), DetectionContactPoint.Rotation(rigidbod));
                    
                    entity.GetDamage(damage * hitBox.Multiply);
                }
                else if (col.collider.TryGetComponent(out ParticleManagement particle))
                {
                    particle.SetPositionAndRotation(DetectionContactPoint.Position(col), DetectionContactPoint.Rotation(rigidbod));

                    particle.Spawn();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isCheckDeathZone == true)
        {
            if (col.CompareTag(tagDeathZone))
            {
                if (player == null && col.gameObject.GetComponent<PlayerMovement>())
                    player = col.gameObject.GetComponent<PlayerMovement>();

                if (player != null && Vector2.Distance(player.transform.position, transform.position) > distanceToPlayer)
                {
                    isReady = false;

                    collider2d.enabled = isReady;
                }
                else
                {
                    isReady = false;

                    collider2d.enabled = isReady;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isCheckDeathZone == true)
        {
            if (gameObject.activeSelf == true)
                StartCoroutine(DelaySwitch());
        }
    }

    private IEnumerator DelaySwitch()
    {
        yield return waitForSeconds;

        isReady = true;
        collider2d.enabled = isReady;
    }
}