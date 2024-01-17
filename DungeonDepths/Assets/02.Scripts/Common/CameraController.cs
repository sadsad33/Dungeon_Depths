using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Vector3 delta = new Vector3(0f, 2f, 5f);    // ī�޶� offset �� : ��ġ ������
	public GameObject player = null;
	//public Camera mainCamera;
	Vector3 offset;
	float zoomSpeed = 10f;
	float rotationX = 0.0f;         // X�� ȸ����
	float rotationY = 0.0f;         // Y�� ȸ����
	float speed = 100.0f;           // ȸ���ӵ�
	string playerTag = "Player";
	void Awake()
	{
		//mainCamera = GetComponent<Camera>();
		player = GameObject.FindWithTag(playerTag).gameObject;
		
		transform.position = player.transform.position + delta;
		transform.LookAt(player.transform.position + Vector3.up * 1f);
		offset = player.transform.position - transform.position;
	}
	//void Start()
	//{
	//	transform.position = player.transform.position + delta;
	//	transform.LookAt(player.transform.position + Vector3.up * 1f);
	//	offset = transform.position - player.transform.position;
	//}
	void LateUpdate()
	{
        // offset ��ŭ �Ÿ��� �ΰ� �÷��̾ �Ѿư���.
        transform.position = player.transform.position + offset;
		Rotate();
		Zoom();
		//offset = transform.position - player.transform.position;
		offset = player.transform.position - transform.position;


		Vector3 direction = (player.transform.position - transform.position);
		RaycastHit[] hits = Physics.RaycastAll(transform.position, direction.normalized + Vector3.up * 1.2f, direction.magnitude,
							1 << LayerMask.NameToLayer("EnvironmentObject"));

		//Debug.DrawRay(transform.position, direction + Vector3.up * 1.2f, Color.red, 1f);

		for (int i = 0; i < hits.Length; i++)
		{
			TransparentObject[] obj = hits[i].transform.GetComponentsInChildren<TransparentObject>();

			for (int j = 0; j < obj.Length; j++)
			{
				//Debug.Log("Transparent�迭 : "+obj[j].name);
				obj[j]?.BecomeTransparent();
			}
		}

		//RaycastHit hit;
		//if(Physics.Raycast(transform.position, offset, out hit) && !hit.collider.CompareTag(playerTag))
		//{
		//	Debug.Log("ī�޶� ��� ��ü�� �±� : " + hit.collider.name);
		//	transform.position = player.transform.position + new Vector3(0, 1.5f, -0.8f);
		//}
	}
	void Zoom()
	{
		float distance = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        float newDeltaMagnitude = delta.magnitude - distance;
        newDeltaMagnitude = Mathf.Clamp(newDeltaMagnitude, 2f, 10f);

        delta = delta.normalized * newDeltaMagnitude;


    }
	void Rotate()
	{
		// x,y�� ���������� ���콺 ȸ����ȭ���� ���, �� ���� ��ŸŸ�Ӱ� �ӵ��� ���Ѵ�.
		float _rotationX = Input.GetAxis("Mouse X") * Time.deltaTime * speed;
		float _rotationY = Input.GetAxis("Mouse Y") * Time.deltaTime * speed;

		// ���� ī�޶��� x�� ȸ��, y�� ȸ�� ���� ������ ������ ��ȭ���� �����ش�.
		rotationX += _rotationX;
		rotationY -= _rotationY;
		// y�� ���������� ī�޶� ȸ���� ���Ѱ��� ���Ѱ��� �߰��Ѵ�.
		rotationY = Mathf.Clamp(rotationY, 5f, 70f);

		// ī�޶��� ȸ���� �ݿ����ش�. 
		transform.rotation = Quaternion.Euler(rotationY, rotationX, 0f);

		// ī�޶� ��ġ ����
		Vector3 _negDistance = new Vector3(0.0f, 0.0f, -delta.magnitude);
		Vector3 _position = transform.rotation * _negDistance + player.transform.position;
		transform.position = _position + Vector3.up * 1f;
	}
}
