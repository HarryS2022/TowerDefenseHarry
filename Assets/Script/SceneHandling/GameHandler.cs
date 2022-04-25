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
        //Debug.Log(Input.mousePosition + " " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if(isPlacingCharacter && Input.GetMouseButtonDown(0))
        {
            Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector3.forward, 11, ValidCharacterPlacementLayer);
            if (hit.collider)
            {
                Vector2 characterPos = new Vector2(origin.x, findY(hit.collider, origin));
                GameObject character = Instantiate(characterToPlace);
                character.transform.position = characterPos;
            }
            isPlacingCharacter = false;
        }
    }

    float findY(Collider2D collider, Vector3 mousePos)
    {
        //find the bottom of collider and return its y 
        return mousePos.y;
    }

    public void PlaceArcher()
    {
        isPlacingCharacter = true;
        characterToPlace = ArcherPrefab.gameObject;
    }
}
