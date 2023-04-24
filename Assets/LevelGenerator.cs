using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<CubeInterface> blocks = new List<CubeInterface>();
    public CubeInterface startingRoom ;
    public int roomAmount;
    
    private CubeInterface.ConnectionPoint initialEdge;
    private CubeInterface latestRoom;
    
    // Start is called before the first frame update
    private void Start()
    {
        ArrangeLevel();
    }
    public void ArrangeLevel()
    {
        
        //placing first room at starting point
        var firstRoom=Instantiate(startingRoom, this.transform);
        firstRoom.transform.localPosition = Vector3.zero;
        //first room is latest room
        latestRoom = firstRoom;
        //selecting edge of first room
        for (int i = 0; i < blocks[0].connectionPoints.Length; i++)
        {
            var currentEdge = blocks[0].connectionPoints[i];
            if (currentEdge.IsConnectable)
            {
                initialEdge = currentEdge;
                Debug.Log(currentEdge.position);
                break;
            }
        }


        //generate Rooms
        for (int i = 1; i < roomAmount; i++)
        {
            //select random room
            var currentBlock = blocks[Random.Range(0, blocks.Count)];
            //create room
            var roomPos = latestRoom.transform.TransformPoint(initialEdge.position);
            var roomRot = latestRoom.transform.TransformDirection(initialEdge.direction);
            latestRoom = Instantiate(currentBlock, this.transform);
            //find its available edge
            for (int g = 0; g < latestRoom.connectionPoints.Length; g++)
            {
                
                Debug.Log(currentBlock);
                var newRoomEdge = latestRoom.connectionPoints[g];
                
                if (newRoomEdge.IsConnectable)
                {
                    //a is the first room c is the its open edge, b is the second room d is the its open edge
                    //available edge found
                    var availableEdge = newRoomEdge;
                    availableEdge.IsConnectable = false;
                    //find direction of first rooms open edge
                    //this supposed to be c reflected
                    Quaternion desiredRot = Quaternion.LookRotation(Vector3.Reflect(roomRot,Vector3.up),Vector3.up);
                    //relative rotation amount of second room
                    //this supposed to be c * quaternion inverse d
                    Quaternion rotationCorrection = desiredRot * Quaternion.Inverse(Quaternion.LookRotation(newRoomEdge.direction,Vector3.up));
                    //rotate second room accordingly
                    
                    latestRoom.transform.rotation = rotationCorrection;
                    //offset amount calculation
                    Vector3 positionCorrection = (roomPos -latestRoom.transform.TransformPoint(availableEdge.position));
                    
                    //place second room accordingly

                    latestRoom.transform.position = latestRoom.transform.position+positionCorrection;
                    initialEdge = availableEdge;
                    break;
                    

                    

                }

            }

            
        }

        
    }
}
