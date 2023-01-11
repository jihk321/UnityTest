using UnityEngine;

// 플레이어 캐릭터를 조작하기 위한 사용자 입력을 감지
// 감지된 입력값을 다른 컴포넌트들이 사용할 수 있도록 제공
public class PlayerInput : MonoBehaviour {
    public string UpDownAxisName = "Vertical"; // 상하
    public string LeRiAxisName = "Horizontal"; // 좌우
    public string fireButtonName = "Fire1"; // 발사를 위한 입력 버튼 이름
    public string reloadButtonName = "Reload"; // 재장전을 위한 입력 버튼 이름
    public string RotateAxis = "Mouse ScrollWheel";

    // 값 할당은 내부에서만 가능
    public float UpDown { get; private set; } // 상하
    public float LeRi { get; private set; } // 좌우
    public float Rotate { get; private set; } // 회전
    public bool fire { get; private set; } // 감지된 발사 입력값
    public bool reload { get; private set; } // 감지된 재장전 입력값

    private Transform[] zombieuitrans;
    private GameObject[] zoms;

    // 매프레임 사용자 입력을 감지
    private void Update() {
        // 게임오버 상태에서는 사용자 입력을 감지하지 않는다
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            UpDown = 0;
            LeRi = 0;
            Rotate = 0;
            fire = false;
            reload = false;
            return;
        }

        // UpDown에 관한 입력 감지
        UpDown = Input.GetAxisRaw(UpDownAxisName);
        // LeRi에 관한 입력 감지
        LeRi = Input.GetAxisRaw(LeRiAxisName);
        
        if (Input.GetMouseButton(2)) // 마우스 휠 클릭하는 동안 마우스 x값 가져옴 
            Debug.Log(" 마우스 포지션 :" + Input.mousePosition);

        else Rotate = 0;
        
        // fire에 관한 입력 감지
        fire = Input.GetButton(fireButtonName);
        // reload에 관한 입력 감지
        reload = Input.GetButtonDown(reloadButtonName);
    }
}