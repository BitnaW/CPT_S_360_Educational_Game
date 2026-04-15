using UnityEngine;

public class MMUMob : MonoBehaviour, IInteractable
{
    public bool isAlive { get; private set; } 
    public string mobID { get; private set; }
    public GameObject itemPrefab; //if mob drops an item 
    public Sprite mobSprite; //sprite for the mob (?)
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mobID ??= GlobalInteractionHelper.GenerateUniqueID(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        if (!CanInteract()) return;
        interactWithMob();
    }

    public bool CanInteract()
    {
        return !isAlive; 
    }

    private void interactWithMob()
    {
        //trigger npc dialogue?
        SetInteraction(true); //if in close proximity, trigger interaction and set alive to true
       
        if (itemPrefab) //for allowing a dropped item, maybe for when mob is killed?
        {
            GameObject droppedItem = Instantiate(itemPrefab, transform.position + Vector3.down, Quaternion.identity);
            //add bounce effect here
        }
    
    }

    public void SetInteraction(bool alive)
    {
        isAlive = alive;
        if (isAlive)
        {
            //allow interaction and npc dialogue
        }


    }
}
