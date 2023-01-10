using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f; // 앞뒤 움직임의 속도
    public float rotateSpeed = 180f; // 좌우 회전 속도


    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터

    private void Start() {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate() {
        // 물리 갱신 주기마다 움직임, 회전, 애니메이션 처리 실행
        Rotate();
        Move();

        float moving = (playerInput.UpDown + playerInput.LeRi)/2 ;
        playerAnimator.SetFloat("Move", moving);
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move() {
        Vector3 moveDistance = new Vector3(playerInput.LeRi, 0, playerInput.UpDown) * moveSpeed * Time.deltaTime;
        // Debug.Log("moveDistance : " + moveDistance);
        // Vector3 moveDistance = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
    }

    // 입력값에 따라 캐릭터를 좌우로 회전
    private void Rotate() {
        Vector3 mousePos = Input.mousePosition;

        Vector3 forward = mousePos - transform.position;

        // transform.rotation = Quaternion.LookRotation(forward, Vector3.up);

        float mouseX = Input.GetAxis("Mouse X");// * rotateSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y");// * rotateSpeed * Time.deltaTime;

        // playerRigidbody.rotation = Quaternion.LookRotation(Input.mousePosition);
        
        // if (playerInput.Rotate == 0 ) turn = 0;
        // else if (playerInput.Rotate > 0) turn = 1.0f * rotateSpeed * Time.deltaTime;
        // else turn = -1.0f * rotateSpeed * Time.deltaTime;

        // float turn = playerInput.Rotate * rotateSpeed * Time.deltaTime;
        var direct = Input.mousePosition - transform.position;
        Quaternion mouseR = Quaternion.LookRotation(direct.normalized);
        Ray CameraPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, CameraPoint.direction * 100.0f, Color.red, Time.deltaTime);
        // Debug.Log("mouseR :" + mouseR);
        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0f, mouseX, 0f);
        // playerRigidbody.rotation = Quaternion.Euler(0f, mouseX, 0f);
        // playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0f, turn, 0f);
        // playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0, turn, 0); 

        // playerRigidbody.rotation = Quaternion.Euler(0f, mouseR.y, 0f);
        
    }

    private void MouseX() {
        Vector3 mouseP = Input.mousePosition;
        
        Debug.Log("mouseP :" + mouseP);
        float turn;

        Quaternion mouseAngle = Quaternion.LookRotation((mouseP - playerRigidbody.position).normalized);
        transform.LookAt(new Vector3(mouseAngle.x,mouseAngle.y,0));
        // playerRigidbody.rotation =  mouseAngle;
        
    }

    private void ToZombie() {
        Transform zombiePos = Zombie.FindObjectOfType<Transform>();
        Vector3 MyPos = transform.position;

        transform.LookAt(zombiePos.position);
    }
}