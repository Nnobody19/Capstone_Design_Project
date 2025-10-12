using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerViewInteraction : MonoBehaviour
{
    private Camera _mainCamera;

    public float InteractionDistance = 3f;
    public LayerMask InteractionLayerMask;

    public Transform PlayerTransform;
    public Transform SpawnTransform;

    public Image FadeImage;
    public float FadeDuration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsPlayerStop) return;

        // 메인 카메라 정중앙에서 앞쪽으로 raycast == 플레이어가 바라보는 방향으로 raycast
        Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, InteractionDistance, InteractionLayerMask))
        {
            ItemPickup itemPickup = hit.collider.GetComponent<ItemPickup>();

            if (itemPickup != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (InventoryManager.Instance.Add(itemPickup.item)) Destroy(hit.collider.gameObject);
                }
            }

            else if (hit.collider.tag == "Interaction")
            {
                if (Input.GetKeyDown(KeyCode.E)) StartCoroutine(TeleportPlayer());
            }
        }
    }

    private IEnumerator TeleportPlayer()
    {
        GameManager.IsPlayerStop = true;        // 이동하는 동안 플레이어 정지

        float timer = 0f;

        while (timer < FadeDuration) {
            FadeImage.color = new Color(0, 0, 0, timer / FadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        FadeImage.color = new Color(0, 0, 0, 1);

        CharacterController cc = PlayerTransform.GetComponent<CharacterController>();
        cc.enabled = false;
        PlayerTransform.position = SpawnTransform.position;
        cc.enabled = true;

        timer = 0f;
        while (timer < FadeDuration) {
            FadeImage.color = new Color(0, 0, 0, 1 - (timer / FadeDuration));
            timer += Time.deltaTime;
            yield return null;
        }
        FadeImage.color = new Color(0, 0, 0, 0);

        GameManager.IsPlayerStop = false;
    }
}
