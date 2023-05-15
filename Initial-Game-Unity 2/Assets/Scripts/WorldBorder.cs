using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBorder : MonoBehaviour
{
    public Transform playerTransform;
    public float xBorderSize;
    public float yBorderSize;

    private void LateUpdate()
    {
        Vector3 pos = playerTransform.position;
        pos.x = Mathf.Clamp(pos.x, -xBorderSize, xBorderSize);
        pos.y = Mathf.Clamp(pos.y, -yBorderSize, yBorderSize);
        playerTransform.position = pos;

        if (pos.x == -xBorderSize || pos.x == xBorderSize || pos.y == -yBorderSize || pos.y == yBorderSize)
        {
            LevelManager.manager.GameOver();
            Debug.Log("Player died!");
            Destroy(playerTransform.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, new Vector3(xBorderSize * 2, yBorderSize * 2, 0f));
    }
}
