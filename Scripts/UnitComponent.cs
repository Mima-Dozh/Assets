using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitComponent : MonoBehaviour, IEnumerable
{
    [SerializeField] private UnitType type;
    [SerializeField] public int PlayerIndex;
    internal UnitComponent previousComponent;
    internal UnitComponent nextComponent;
    private float acceleration = 3;
    private Rigidbody2D rigidBodyComponent;
    private bool used;
    public Vector2 finishPosition = Vector2.zero;
    private new SpriteRenderer renderer;
    // Start is called before the first frame update
    [SerializeField] public float MaxHealth;
    public float CurrentHealth { get; private set; }
    public float DamageForceThreshold = 1f;
    public float DamageForceScale = 5f;
    private Vector2[] moweDirection = new Vector2[] 
    { 
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
    };

    void OnCollisionEnter2D(Collision2D other) 
    {
        // Collision would usually be on another component, putting it all here for simplicity
        if(other.gameObject.name.IndexOf("PapaPotato") != -1
        && gameObject.name.IndexOf("PapaPotato") == -1
        ||other.gameObject.name.IndexOf("Evil") != -1
        && gameObject.name.IndexOf("Evil") == -1)
        {
            var force = other.relativeVelocity.magnitude;
            rigidBodyComponent.velocity = rigidBodyComponent.velocity - 100 * other.relativeVelocity;
            if (force > DamageForceThreshold) 
            {
                CurrentHealth -= (int)((force - DamageForceThreshold) * DamageForceScale);
                CurrentHealth = Mathf.Max(0, CurrentHealth);
            }
        }
    }

    public void DestroyUnit()
    {
        previousComponent.nextComponent = nextComponent;
        nextComponent.previousComponent = previousComponent;
    }

    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        CurrentHealth = MaxHealth;
        if(PlayerIndex == 0)
            UnitSelect.AddUnit(this);
        else
            EvilBrain.AddUnit(this);
    }

    public IEnumerator<UnitComponent> GetEnumerator()
    {
        yield return this;
        var pathItem = previousComponent;
            while (pathItem != null)
            {
                yield return pathItem;
                pathItem = pathItem.previousComponent;
            }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public void Deselect()
    {
        used = false;
        renderer.material.color = Color.white;
    }

    public void Select()
    {
        used = true;
        renderer.material.color = Color.green;
    }
    // Update is called once per frame
    void Update()
    {
        var startPosition = rigidBodyComponent.position;
        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, MainCamera.leftX + 1, MainCamera.rightX - 1),
            Mathf.Clamp(transform.position.y, MainCamera.leftY + 1, MainCamera.rightY - 1),
            transform.position.z
        );

        var a = finishPosition - startPosition;
        if(a.magnitude < 0.1
        || finishPosition == Vector2.zero)
        {
            rigidBodyComponent.velocity = Vector2.zero;
            return;
        }
        rigidBodyComponent.velocity = a.normalized * acceleration;
    }
}
