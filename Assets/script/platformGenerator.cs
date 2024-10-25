using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject[] platformPrefabs; // Array of platform prefabs
    public GameObject[] obstaclePrefabs;
    private float spawnPX = 0;
    private float spawnPZ = 0;
    private float spawnOX = 0;
    private float spawnOZ = 0;
    private float angle;
    private bool isspawnNorth = true;
    private bool isspawnSouth = true;
    private bool isspawnWest = true;
    private bool isspawnEast = true;

    void Update()
    {
        angle = gameObject.transform.rotation.eulerAngles.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "platform") // Assuming the player triggers the spawn
        {
            if (60 < angle && 120 > angle && isspawnNorth == true)
            {
                spawnPX += 160;
                SpawnNorth();
            }
            else if (150 < angle && 210 > angle && isspawnEast == true)
            {
                spawnPZ -= 160;
                spawnEast();
            }

            else if (240 < angle && 300 > angle && isspawnSouth == true)
            {
                spawnPX -= 160;
                spawnSouth();
            }

            else if (-30 < angle && 30 > angle && isspawnWest == true || 330 < angle && 390 > angle && isspawnWest == true)
            {
                spawnPZ += 160;
                spawnWest();
            }
        }
    }

    private void SpawnNorth()
    {
        int randomplatform = Random.Range(0, 4); // Choose a random platform prefab
        isspawnEast = true;
        isspawnNorth = true;
        isspawnSouth = true;
        isspawnWest = true;
        if (randomplatform == 2)
        {
            Vector3 spawnPosition = new Vector3(spawnPX,0, spawnPZ-22);
            Instantiate(platformPrefabs[randomplatform], spawnPosition, Quaternion.identity);
            spawnPZ -= 22;
            spawnOZ -= 22;
            spawnOX = spawnPX;
            isspawnNorth = false;
        }
        else if (randomplatform == 3)
        {
            Vector3 spawnPosition = new Vector3(spawnPX, 0, spawnPZ - 22);
            Instantiate(platformPrefabs[randomplatform], spawnPosition, Quaternion.identity);
            spawnPZ -= 22;
            spawnPX += 44;
            spawnOZ -= 22;
            spawnOX = spawnPX;
            isspawnNorth = false;
        }
        else
        {
            Vector3 spawnPosition = new Vector3(spawnPX, 0, spawnPZ - 22);
            Instantiate(platformPrefabs[randomplatform], spawnPosition, Quaternion.identity);

            //obstacle generator ##############################################################
            spawnOX = spawnPX;
            for (int i = 0; i < 5; i++)
            {
                int randomobstacle = Random.Range(0, 8);
                Vector3 spawnOPosition = new Vector3(spawnOX, 0, spawnOZ - 22);
                Instantiate(obstaclePrefabs[randomobstacle], spawnOPosition, Quaternion.Euler(0, 0, 0));
                spawnOX += 32;
            }
        }
    }

    private void spawnWest()
    {
        int randomplatform = Random.Range(0, 4); // Choose a random platform prefab
        isspawnEast = true;
        isspawnNorth = true;
        isspawnSouth = true;
        isspawnWest = true;
        if (randomplatform == 2)
        {
            Vector3 spawnPosition = new Vector3(spawnPX+22,0, spawnPZ);
            Instantiate(platformPrefabs[randomplatform], spawnPosition, Quaternion.Euler(0, -90, 0));
            spawnPX += 22;
            spawnOX += 22;
            spawnOZ = spawnPZ;
            isspawnWest = false;
        }
        else if (randomplatform == 3)
        {
            Vector3 spawnPosition = new Vector3(spawnPX + 22, 0, spawnPZ);
            Instantiate(platformPrefabs[randomplatform], spawnPosition, Quaternion.Euler(0, -90, 0));
            spawnPX += 22;
            spawnPZ += 44;
            spawnOX += 22;
            spawnOZ = spawnPZ;
            isspawnWest = false;
        }
        else
        {
            Vector3 spawnPosition = new Vector3(spawnPX + 22, 0, spawnPZ);
            Instantiate(platformPrefabs[randomplatform], spawnPosition, Quaternion.Euler(0, -90, 0));

            //obstacle generator ##############################################################
            spawnOZ = spawnPZ;
            for (int i = 0; i < 5; i++)
            {
                int randomobstacle = Random.Range(0, 8);
                Vector3 spawnOPosition = new Vector3(spawnOX + 22, 0, spawnOZ);
                Instantiate(obstaclePrefabs[randomobstacle], spawnOPosition, Quaternion.Euler(0, -90, 0));
                spawnOZ += 32;
            }
        }
    }

    private void spawnSouth()
    {
        int randomplatform = Random.Range(0, 4);
        isspawnEast = true;
        isspawnNorth = true;
        isspawnSouth = true;
        isspawnWest = true;

        if (randomplatform == 2)
        {
            Vector3 spawnPosition = new Vector3(spawnPX,0, spawnPZ+22);
            Instantiate(platformPrefabs[randomplatform], spawnPosition, Quaternion.Euler(0, 180, 0));
            spawnPZ += 22;
            spawnOX = spawnPX;
            isspawnSouth = false;
        }
        else if (randomplatform == 3)
        {
            Vector3 spawnPosition = new Vector3(spawnPX, 0, spawnPZ + 22);
            Instantiate(platformPrefabs[randomplatform], spawnPosition, Quaternion.Euler(0, 180, 0));
            spawnPZ += 22;
            spawnPX -= 44;
            spawnOX = spawnPX;
            isspawnSouth = false;
        }
        else
        {
            Vector3 spawnPosition = new Vector3(spawnPX, 0, spawnPZ + 22);
            Instantiate(platformPrefabs[randomplatform], spawnPosition, Quaternion.Euler(0, 180, 0));

            //obstacle generator ##############################################################
            spawnOX = spawnPX;
            for (int i = 0; i < 5; i++)
            {
                int randomobstacle = Random.Range(0, 8);
                Vector3 spawnOPosition = new Vector3(spawnOX, 0, spawnOZ + 22);
                Instantiate(obstaclePrefabs[randomobstacle], spawnOPosition, Quaternion.Euler(0, 180, 0));
                spawnOX -= 32;
            }
        }
    }

    private void spawnEast()
    {
        int randomplatform = Random.Range(0, 4); // Choose a random platform prefab
        isspawnEast = true;
        isspawnNorth = true;
        isspawnSouth = true;
        isspawnWest = true;
        if (randomplatform == 2)
        {
            Vector3 spawnPosition = new Vector3(spawnPX-22,0, spawnPZ);
            Instantiate(platformPrefabs[randomplatform], spawnPosition, Quaternion.Euler(0, -270, 0));
            spawnPX -= 22;
            spawnOZ = spawnPZ;
            isspawnEast = false;
        }
        else if (randomplatform == 3)
        {
            Vector3 spawnPosition = new Vector3(spawnPX - 22, 0, spawnPZ);
            Instantiate(platformPrefabs[randomplatform], spawnPosition, Quaternion.Euler(0, -270, 0));
            spawnPX -= 22;
            spawnPZ -= 44;
            spawnOZ = spawnPZ;
            isspawnEast = false;
        }
        else
        {
            Vector3 spawnPosition = new Vector3(spawnPX - 22, 0, spawnPZ);
            Instantiate(platformPrefabs[randomplatform], spawnPosition, Quaternion.Euler(0, -270, 0));

            //obstacle generator ##############################################################
            spawnOZ = spawnPZ;
            for (int i = 0; i < 5; i++)
            {
                int randomobstacle = Random.Range(0, 8);
                Vector3 spawnOPosition = new Vector3(spawnOX - 22, 0, spawnOZ);
                Instantiate(obstaclePrefabs[randomobstacle], spawnOPosition, Quaternion.Euler(0, -270, 0));
                spawnOZ -= 32;
            }
        }
        
    }

}
