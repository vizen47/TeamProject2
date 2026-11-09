using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomViewController : MonoBehaviour
{
    [SerializeField] private List<GameObject> rooms = new();
    [SerializeField] private GameObject arrows;
    
    private int currentIndex = 0;
    private bool isMoveable = true;

    private void Start()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            rooms[i].SetActive(i == currentIndex);
        }
        arrows.SetActive(isMoveable && rooms.Count > 1);
    }

    public void SetMoveable(bool isMoveable)
    {
        this.isMoveable = isMoveable;
        arrows.SetActive(isMoveable && rooms.Count > 1);
    }

    public void MoveLeft() => Move(-1);
    public void MoveRight() => Move(1);
    
    private void Move(int direction)
    {
        if (!isMoveable || rooms.Count < 2) return;
        int nextIndex = (currentIndex + direction + rooms.Count) % rooms.Count;
        rooms[currentIndex].SetActive(false);
        currentIndex = nextIndex;
        rooms[nextIndex].SetActive(true);
    }

    private void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame && isMoveable)
        {
            MoveLeft();
        }

        if (Keyboard.current.dKey.wasPressedThisFrame && isMoveable)
        {
            MoveRight();
        }
    }
}