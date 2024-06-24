using UnityEngine;
using System.Collections;

public class GroundState
{
	private GameObject player;
	private float width;
	private float height;
	private float length;
	private LayerMask jumpLayer;

	//GroundState constructor.  Sets offsets for raycasting.
	public GroundState(GameObject playerRef, LayerMask layerRef)
	{
		jumpLayer = layerRef;
		player = playerRef;
		width = player.GetComponent<Collider2D>().bounds.extents.x + 0.1f;
		height = player.GetComponent<Collider2D>().bounds.extents.y + 0.1f;
		length = 0.05f;
	}

	//Returns whether or not player is touching wall.
	public bool isWall()
	{
		bool left1 = Physics2D.Raycast(new Vector2(player.transform.position.x - width, player.transform.position.y), -Vector2.right, length, jumpLayer);
		bool left2 = Physics2D.Raycast(new Vector2(player.transform.position.x - width, player.transform.position.y + (height - 0.2f)), -Vector2.right, length, jumpLayer);
		bool left3 = Physics2D.Raycast(new Vector2(player.transform.position.x - width, player.transform.position.y - (height - 0.2f)), -Vector2.right, length, jumpLayer);

		bool right1 = Physics2D.Raycast(new Vector2(player.transform.position.x + width, player.transform.position.y), Vector2.right, length, jumpLayer);
		bool right2 = Physics2D.Raycast(new Vector2(player.transform.position.x + width, player.transform.position.y + (height - 0.2f)), Vector2.right, length, jumpLayer);
		bool right3 = Physics2D.Raycast(new Vector2(player.transform.position.x + width, player.transform.position.y - (height - 0.2f)), Vector2.right, length, jumpLayer);

		Debug.DrawRay(new Vector3(player.transform.position.x - width, player.transform.position.y, 1), -Vector2.right * length, Color.blue, 0.0001f);
		Debug.DrawRay(new Vector3(player.transform.position.x - width, player.transform.position.y + (height - 0.4f), 1), -Vector2.right * length, Color.blue, 0.0001f);
		Debug.DrawRay(new Vector3(player.transform.position.x - width, player.transform.position.y - (height - 0.4f), 1), -Vector2.right * length, Color.blue, 0.0001f);


		Debug.DrawRay(new Vector3(player.transform.position.x + width, player.transform.position.y, 1), Vector2.right * length, Color.red, 0.0001f);
		Debug.DrawRay(new Vector3(player.transform.position.x + width, player.transform.position.y + (height - 0.4f), 1), Vector2.right * length, Color.red, 0.0001f);
		Debug.DrawRay(new Vector3(player.transform.position.x + width, player.transform.position.y - (height - 0.4f), 1), Vector2.right * length, Color.red, 0.0001f);

		if (left1 || right1 || left2 || right2 || left3 || right3)
			return true;
		else
			return false;
	}

	//Returns whether or not player is touching ground.
	public bool isGround()
	{
		bool bottom1 = Physics2D.Raycast(new Vector2(player.transform.position.x, player.transform.position.y - height), -Vector2.up, length, jumpLayer);
		bool bottom2 = Physics2D.Raycast(new Vector2(player.transform.position.x + (width - 0.2f), player.transform.position.y - height), -Vector2.up, length, jumpLayer);
		bool bottom3 = Physics2D.Raycast(new Vector2(player.transform.position.x - (width - 0.2f), player.transform.position.y - height), -Vector2.up, length, jumpLayer);

		Debug.DrawRay(new Vector3(player.transform.position.x, player.transform.position.y - height, 1), -Vector2.up * length, Color.red, 0.0001f);
		Debug.DrawRay(new Vector3(player.transform.position.x + (width - 0.2f), player.transform.position.y - height, 1), -Vector2.up * length, Color.green, 0.0001f);
		Debug.DrawRay(new Vector3(player.transform.position.x - (width - 0.2f), player.transform.position.y - height, 1), -Vector2.up * length, Color.blue, 0.0001f);

		if (bottom1 || bottom2 || bottom3)
			return true;
		else
			return false;
	}

	//Returns whether or not player is touching wall or ground.
	public bool isTouching()
	{
		if (isGround() || isWall())
			return true;
		else
			return false;
	}

	//Returns direction of wall.
	public int wallDirection()
	{
		bool left1 = Physics2D.Raycast(new Vector2(player.transform.position.x - width, player.transform.position.y), -Vector2.right, length, jumpLayer);
		bool left2 = Physics2D.Raycast(new Vector2(player.transform.position.x - width, player.transform.position.y + (height - 0.2f)), -Vector2.right, length, jumpLayer);
		bool left3 = Physics2D.Raycast(new Vector2(player.transform.position.x - width, player.transform.position.y - (height - 0.2f)), -Vector2.right, length, jumpLayer);

		bool right1 = Physics2D.Raycast(new Vector2(player.transform.position.x + width, player.transform.position.y), Vector2.right, length, jumpLayer);
		bool right2 = Physics2D.Raycast(new Vector2(player.transform.position.x + width, player.transform.position.y + (height - 0.2f)), Vector2.right, length, jumpLayer);
		bool right3 = Physics2D.Raycast(new Vector2(player.transform.position.x + width, player.transform.position.y - (height - 0.2f)), Vector2.right, length, jumpLayer);


		if (left1 || left2 || left3)
			return -1;
		else if (right1 || right2 || right3)
			return 1;
		else
			return 0;
	}
}