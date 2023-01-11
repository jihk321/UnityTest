using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f; // 앞뒤 움직임의 속도
    public float rotateSpeed = 180f; // 좌우 회전 속도


    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터

    private void Awake() {
        Cursor.lockState = CursorLockMode.Locked;    
    }
    private void Start() {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate() {
        // 물리 갱신 주기마다 움직임, 회전, 애니메이션 처리 실행
        MouseX();
        Move();

        float moving = playerInput.UpDown;
        playerAnimator.SetFloat("Move", moving);
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move() {
        Vector3 moveDistance = ((playerInput.UpDown * transform.forward) + (playerInput.LeRi * transform.right)).normalized  * moveSpeed * Time.deltaTime;
        // Vector3 moveDistance = new Vector3(playerInput.LeRi, 0, playerInput.UpDown) * moveSpeed * Time.deltaTime;
        // Debug.Log("moveDistance : " + moveDistance);
        // Vector3 moveDistance = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
        
    }

    // 입력값에 따라 캐릭터를 좌우로 회전
    private void Rotate()
    {
        // Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Plane plane = new Plane(transform.up, transform.position);

        // float distance;
        // if ( !plane.Raycast(cameraRay, out distance) )
        //     return ;

        // Vector3 hitPoint = cameraRay.GetPoint(distance);
        // transform.LookAt(hitPoint);

        // Debug.DrawRay(
        //     cameraRay.origin, 
        //     (hitPoint - cameraRay.origin) * Vector3.Distance(hitPoint, cameraRay.origin),
        //     Color.red,
        //     Time.deltaTime
        // );
        
    }

    private void MouseX() {
        float mouseX = Input.GetAxis("Mouse X");
        
        transform.rotation = transform.rotation * Quaternion.Euler(0f, mouseX * rotateSpeed * Time.deltaTime, 0f);
        // transform.eulerAngles += transform.up * (mouseX * rotateSpeed * Time.deltaTime);
    }

    private void ToZombie() {
        Transform zombiePos = Zombie.FindObjectOfType<Transform>();
        Vector3 MyPos = transform.position;

        transform.LookAt(zombiePos.position);
    }
}