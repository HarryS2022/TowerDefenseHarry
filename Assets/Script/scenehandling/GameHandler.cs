using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public Archer ArcherPrefab;
    public Lancer LancerPrefab;
    public Knight KnightPrefab;
    private bool isPlacingCharacter;
    public LayerMask ValidCharacterPlacementLayer;
    private GameObject characterToPlace;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlacingCharacter && Input.GetMouseButtonDown(0))
        {
            Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //where is the mouse
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector3.forward, float.PositiveInfinity, ValidCharacterPlacementLayer);
            //detect if you hit something 
            if (hit.collider)
            {
                Vector2 characterPos = new Vector2(origin.x, findY(hit.collider, origin));
                //origin is mouse
                GameObject character = Instantiate(characterToPlace);
                character.transform.position = characterPos;
            }
            isPlacingCharacter = false;
        }
    }

    float findY(Collider2D collider, Vector3 mousePos)
    {
        //find the bottom of collider and return its y value
        return mousePos.y;
    }

    public void PlaceArcher()
    {
        isPlacingCharacter = true;
        characterToPlace = ArcherPrefab.gameObject;
    }
}
