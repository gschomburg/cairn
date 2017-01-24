using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGridGeo : MonoBehaviour {
// Use this for initialization
void Start ()
{
	for (int x = -5; x < 5; x++) {
		for (int z = -5; z < 5; z++) {
			float height = Mathf.Min(Random.value, Random.value, Random.value, Random.value) * 10;

			GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
			cube.transform.position = new Vector3 (x * 2.0f, height * .5f, z * 2.0f);

			Vector3 scale = cube.transform.localScale;
			scale.y *= height;
			cube.transform.localScale = scale;
		}
	}
}

// Update is called once per frame
void Update ()
{
}
}
