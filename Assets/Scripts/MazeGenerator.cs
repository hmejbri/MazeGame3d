using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallsParent;
    public GameObject doorsParent;
    public GameObject floorsParent;

    public GameObject wallPrefab;
    public GameObject doorPrefab;
    public GameObject floorPrefab;

    public int size;
    public int nbWalls;

    public Vector2 Horizontal_wallSize;
    public Vector2 Vertical_wallSize;

    private int getNextMove(int posX, int posY)
    {
        //1: left     2: top     3: right     4: bottom
        int nextMove;

        if (posX == 0)
            if (posY == 0)
                nextMove = Random.Range(2, 4);
            else if (posY == size - 1)
            {
                nextMove = Random.Range(1, 3);
            }
            else
            {
                nextMove = Random.Range(1, 4);
            }
        else if (posX == size - 1)
            if (posY == 0)
                nextMove = Random.Range(3, 5);
            else if (posY == size - 1)
            {
                int[] numbers = new int[] { 1, 4 };
                int rndmIndex = Random.Range(0, 2);
                nextMove = numbers[rndmIndex];
            }
            else
            {
                int[] numbers = new int[] { 1, 3, 4 };
                int rndmIndex = Random.Range(0, 3);
                nextMove = numbers[rndmIndex];
            }
        else if (posY == 0)
            nextMove = Random.Range(2, 5);
        else if (posY == size - 1)
        {
            int[] numbers = new int[] { 1, 2, 4 };
            int rndmIndex = Random.Range(0, 3);
            nextMove = numbers[rndmIndex];
        }
        else
            nextMove = Random.Range(1, 5);

        return nextMove;
    }

    void ReturnToLastCell(Vector2[] stack, bool[,] visits, ref int posX, ref int posY, ref int stackLen)
    {
        bool test;

        do
        {
            if (posX == 0)
                if (posY == 0)
                {
                    if (visits[0, 1] == true && visits[1, 0] == true)
                    {
                        Vector2 newPos = stack[stackLen - 1];
                        posX = (int)newPos.x;
                        posY = (int)newPos.y;
                        stackLen--;
                        test = false;
                    }
                    else
                        test = true;
                }
                else if (posY == size - 1)
                {
                    if (visits[posX, posY - 1] == true && visits[posX + 1, posY] == true)
                    {
                        Vector2 newPos = stack[stackLen - 1];
                        posX = (int)newPos.x;
                        posY = (int)newPos.y;
                        stackLen--;
                        test = false;
                    }
                    else
                        test = true;
                }
                else
                {
                    if (visits[posX, posY + 1] == true && visits[posX + 1, posY] == true && visits[posX, posY - 1] == true)
                    {
                        Vector2 newPos = stack[stackLen - 1];
                        posX = (int)newPos.x;
                        posY = (int)newPos.y;
                        stackLen--;
                        test = false;
                    }
                    else
                        test = true;
                }
            else if (posX == size - 1)
                if (posY == 0)
                {
                    if (visits[posX, 1] == true && visits[posX - 1, 0] == true)
                    {
                        Vector2 newPos = stack[stackLen - 1];
                        posX = (int)newPos.x;
                        posY = (int)newPos.y;
                        stackLen--;
                        test = false;
                    }
                    else
                        test = true;
                }
                else if (posY == size - 1)
                {
                    if (visits[posX, posY - 1] == true && visits[posX - 1, posY] == true)
                    {
                        Vector2 newPos = stack[stackLen - 1];
                        posX = (int)newPos.x;
                        posY = (int)newPos.y;
                        stackLen--;
                        test = false;
                    }
                    else
                        test = true;
                }
                else
                {
                    if (visits[posX, posY + 1] == true && visits[posX - 1, posY] == true && visits[posX, posY - 1] == true)
                    {
                        Vector2 newPos = stack[stackLen - 1];
                        posX = (int)newPos.x;
                        posY = (int)newPos.y;
                        stackLen--;
                        test = false;
                    }
                    else
                        test = true;
                }
            else if (posY == 0)
            {
                if (visits[posX, 1] == true && visits[posX + 1, 0] == true && visits[posX - 1, 0] == true)
                {
                    Vector2 newPos = stack[stackLen - 1];
                    posX = (int)newPos.x;
                    posY = (int)newPos.y;
                    stackLen--;
                    test = false;
                }
                else
                    test = true;
            }
            else if (posY == size - 1)
            {
                if (visits[posX, posY - 1] == true && visits[posX + 1, posY] == true && visits[posX - 1, posY] == true)
                {
                    Vector2 newPos = stack[stackLen - 1];
                    posX = (int)newPos.x;
                    posY = (int)newPos.y;
                    stackLen--;
                    test = false;
                }
                else
                    test = true;
            }
            else
            {
                if (visits[posX, posY + 1] == true && visits[posX, posY - 1] == true && visits[posX + 1, posY] == true && visits[posX - 1, posY] == true)
                {
                    Vector2 newPos = stack[stackLen - 1];
                    posX = (int)newPos.x;
                    posY = (int)newPos.y;
                    stackLen--;
                    test = false;
                }
                else
                    test = true;
            }
        } while (!test);
    }

    Vector2[,] generateMatrix()
    {
        int nbVisits = 1;
        int stackLen = 0;
        Vector2[] stack = new Vector2[size * size];
        bool[,] visits = new bool[size, size];
        Vector2[,] matrix = new Vector2[size, size];
        int posX = Random.Range(0, size);
        int posY = Random.Range(0, size);

        for (var i = 0; i < size; i++)
            for (var j = 0; j < size; j++)
                visits[i, j] = false;

        for (var i = 0; i < size; i++)
            for (var j = 0; j < size; j++)
                matrix[i, j] = new Vector2(-1, -1);

        while (nbVisits < size * size)
        {
            //1: left     2: top     3: right     4: bottom
            visits[posX, posY] = true;
            int nextMove;
            bool test = true;

            ReturnToLastCell(stack, visits, ref posX, ref posY, ref stackLen);

            do
            {
                //Generate next move randomly
                nextMove = getNextMove(posX, posY);

                switch (nextMove)
                {
                    case 1:
                        test = visits[posX, posY - 1] != true;
                        break;
                    case 2:
                        test = visits[posX + 1, posY] != true;
                        break;
                    case 3:
                        test = visits[posX, posY + 1] != true;
                        break;
                    case 4:
                        test = visits[posX - 1, posY] != true;
                        break;
                }
            } while (!test);

            if (matrix[posX, posY].x == -1)
                matrix[posX, posY].x = nextMove;
            else
                matrix[posX, posY].y = nextMove;

            stack[stackLen] = new Vector2(posX, posY);
            stackLen++;

            switch (nextMove)
            {
                case 1:
                    posY -= 1;
                    break;
                case 2:
                    posX += 1;
                    break;
                case 3:
                    posY += 1;
                    break;
                case 4:
                    posX -= 1;
                    break;
            }

            nbVisits++;
        }

        return matrix;
    }

    private void CreateWall(Vector2[,] matrix, int i, int j, GameObject box1)
    {
        bool[,] doors = new bool[size, size];
        for (var k = 0; k < size; k++)
            for (var l = 0; l < size; l++)
                doors[k, l] = false;

        //1: left     2: top     3: right     4: bottom
        if (matrix[i, j].x != 1 && matrix[i, j].y != 1)
        {
            if ((j - 1 >= 0 && matrix[i, j - 1].x != 3 && matrix[i, j - 1].y != 3) || j - 1 < 0)
            {
                //creating a wall with the box sprite and changing its scale
                var wall = Instantiate(wallPrefab, new Vector3(j, 1, i), Quaternion.identity);
                wall.transform.name = "left";
                wall.transform.SetParent(box1.transform);
                wall.transform.localScale = new Vector3(Vertical_wallSize.x, 1, Vertical_wallSize.y + Vertical_wallSize.x);
                //wall.GetComponent<BoxCollider2D>().size = new Vector2(Vertical_wallSize.y, Vertical_wallSize.y);
            }
        }

        if (matrix[i, j].x != 2 && matrix[i, j].y != 2)
        {
            if ((i + 1 < size && matrix[i + 1, j].x != 4 && matrix[i + 1, j].y != 4) || i + 1 >= size)
            {
                //creating a wall with the box sprite and changing its scale
                var wall = Instantiate(wallPrefab, new Vector3(j + Horizontal_wallSize.x / 2, 1, i + Horizontal_wallSize.x / 2), Quaternion.identity);
                wall.transform.name = "top";
                wall.transform.SetParent(box1.transform);
                wall.transform.localScale = new Vector3(Horizontal_wallSize.x, 1, Horizontal_wallSize.y);
                //wall.GetComponent<BoxCollider>().size = new Vector2(Horizontal_wallSize.x - (Horizontal_wallSize.y / 2), Horizontal_wallSize.x - (Horizontal_wallSize.y / 2));
            }
        }

        if (matrix[i, j].x != 3 && matrix[i, j].y != 3)
        {
            if ((j + 1 < size && matrix[i, j + 1].x != 1 && matrix[i, j + 1].y != 1) || j + 1 >= size)
            {
                //creating a wall with the box sprite and changing its scale
                var wall = Instantiate(wallPrefab, new Vector3(j + Vertical_wallSize.y + (Vertical_wallSize.x / 2), 1, i), Quaternion.identity);
                wall.transform.name = "right";
                wall.transform.SetParent(box1.transform);
                wall.transform.localScale = new Vector3(Vertical_wallSize.x, 1, Vertical_wallSize.y + Vertical_wallSize.x);
            }
        }

        if (matrix[i, j].x != 4 && matrix[i, j].y != 4)
        {
            if ((i - 1 >= 0 && matrix[i - 1, j].x != 2 && matrix[i - 1, j].y != 2) || i - 1 < 0) { 
                //creating a wall with the box sprite and changing its scale
                var wall = Instantiate(wallPrefab, new Vector3(j + Horizontal_wallSize.x / 2, 1, i - Horizontal_wallSize.x / 2), Quaternion.identity);
                wall.transform.name = "bottom";
                wall.transform.SetParent(box1.transform);
                wall.transform.localScale = new Vector3(Horizontal_wallSize.x, 1, Horizontal_wallSize.y);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        nbWalls = size;
        Vector2[,] matrix = generateMatrix();

        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                var box1 = new GameObject("box(" + i + ", " + j + ")");
                box1.transform.SetParent(wallsParent.transform);
                CreateWall(matrix, i, j, box1);

                var floor = Instantiate(floorPrefab, new Vector3(i + (Horizontal_wallSize.x / 2), .5f, j), Quaternion.identity);
                floor.transform.SetParent(floorsParent.transform);
                floor.transform.localScale = new Vector3(Horizontal_wallSize.x, .1f, Vertical_wallSize.y);
            }
        }
    }
}
