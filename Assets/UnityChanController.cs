using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  //�i�ǉ�5�j

public class UnityChanController : MonoBehaviour
{
    //�A�j���[�V�������邽�߂̃R���|�[�l���g������
    private Animator myAnimator;

    //Unity�������ړ�������R���|�[�l���g������i�ǉ��j
    private Rigidbody myRigidbody;

    //�O�����̑��x�i�ǉ��j
    private float velocityZ = 16f;

    //�������̑��x�i�ǉ�2�j
    private float velocityX = 10f;

    //������̑��x�i�ǉ�3�j
    private float velocityY = 10f;

    //���E�̈ړ��ł���͈́i�ǉ�2�j
    private float movableRange = 3.4f;

    //����������������W���i�ǉ�4�j
    private float coefficient = 0.99f;

    //�Q�[���I���̔���i�ǉ�4�j
    private bool isEnd = false;

    //�Q�[���I�����ɕ\������e�L�X�g�i�ǉ�5�j
    private GameObject stateText;

    //�X�R�A��\������e�L�X�g�i�ǉ�6�j
    private GameObject scoreText;

    //���_�i�ǉ�6�j
    private int score = 0;

    //���{�^�������̔���i�ǉ�7�j
    private bool isLButtonDown = false;

    //�E�{�^�������̔���i�ǉ�7�j
    private bool isRButtonDown = false;

    //�W�����v�{�^�������̔���i�ǉ�7�j
    private bool isJButtonDown = false;

    // Start is called before the first frame update
    void Start()
    {
        //Animator�R���|�[�l���g���擾
        this.myAnimator = GetComponent<Animator>();

        //����A�j���[�V�������J�n
        this.myAnimator.SetFloat("Speed", 1);

        //Rigidvody�R���|�[�l���g���擾�i�ǉ��j
        this.myRigidbody = GetComponent<Rigidbody>();

        //�V�[������stateText�I�u�W�F�N�g���擾�i�ǉ�5�j
        this.stateText = GameObject.Find("GameResultText");

        //�V�[������scoreText�I�u�W�F�N�g���擾�i�ǉ�6�j
        this.scoreText = GameObject.Find("ScoreText");

    }

    // Update is called once per frame
    void Update()
    {

        //�Q�[���I���Ȃ�Unity�����̓�������������i�ǉ�4�j
        if (this.isEnd)
        {
            this.velocityZ *= this.coefficient;
            this.velocityX *= this.coefficient;
            this.velocityY *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }

        //�������̓��͂ɂ�鑬�x�i�ǉ�2�j
        float inputVelocityX = 0;

        //������̓��͂ɖ鑬�x�i�ǉ�3�j
        float inputVelocityY = 0;

        //Unity��������L�[�܂��̓{�^���ɉ����č��E�Ɉړ�������i�ǉ�2�j(�ǉ�7)
        if ((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x)
        {
            //�������ւ̑��x�����i�ǉ�2�j
            inputVelocityX = -this.velocityX;
        }

        //(�ǉ�7)
        else if ((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange)
        {
            //�E�����ւ̑��x�����i�ǉ�2�j
            inputVelocityX = this.velocityX;
        }

        //�W�����v���Ă��Ȃ��Ƃ��ɃX�y�[�X�������ꂽ��W�����v����i�ǉ�3�j(�ǉ�7)
        if ((Input.GetKeyDown(KeyCode.Space) || this.isJButtonDown) && this.transform.position.y < 0.5f)
        {
            //�W�����v�A�j�����Đ��i�ǉ�3�j
            this.myAnimator.SetBool("Jump", true);

            //������ւ̑��x�����i�ǉ�3�j
            inputVelocityY = this.velocityY;
        }
        else
        {
            //���݂̂x���̑��x�����i�ǉ�3�j
            inputVelocityY = this.myRigidbody.velocity.y;
        }

        //Jump�X�e�[�g�̏ꍇ��Jump��false���Z�b�g����i�ǉ��j
        if(this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        //Unity�����ɑ��x��^����i�ǉ� ���@�ύX2 ���@�ύX3�j
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, velocityZ);

    }

    //�g���K�[���[�h�ő��̃I�u�W�F�N�g�ƐڐG�����ꍇ�̏����i�ǉ�4�j
    void OnTriggerEnter(Collider other)
    {
        //��Q���ɏՓ˂����ꍇ�i�ǉ�4�j
        if(other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;

            //stateText��GAME OVER��\���i�ǉ�5�j
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }

        //�S�[���n�_�ɓ��B�����ꍇ�i�ǉ�4�j
        if (other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;

            //stateText��GAME CLEAR��\���i�ǉ�5�j
            this.stateText.GetComponent<Text>().text = "CLEAR!!";
        }

        //�R�C���ɏՓ˂����ꍇ�i�ǉ�4�j
        if (other.gameObject.tag == "CoinTag")
        {
            //�X�R�A�����Z�i�ǉ�6�j
            this.score += 10;

            //ScoreText�Ɋl�������_����\���i�ǉ�6�j
            this.scoreText.GetComponent<Text>().text = "Score " + this.score + " pt";

            //�p�[�e�B�N�����Đ��i�ǉ�5�j
            GetComponent<ParticleSystem>().Play();


            //�ڐG�����R�C���̃I�u�W�F�N�g��j���i�ǉ��j
            Destroy(other.gameObject);
        }

    }

    //�W�����v�{�^�����������ꍇ�̏����i�ǉ�7�j
    public void GetMyJumpButtonDown()
    {
        this.isJButtonDown = true;
    }

    //�W�����v�{�^���𗣂����ꍇ�̏����i�ǉ�7�j
    public void GetMyJumpButtonUp()
    {
        this.isJButtonDown = false;
    }

    //���{�^���������������ꍇ�̏����i�ǉ�7�j
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }
    //���{�^���𗣂����ꍇ�̏����i�ǉ�7�j
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }

    //�E�{�^���������������ꍇ�̏����i�ǉ�7�j
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }
    //�E�{�^���𗣂����ꍇ�̏����i�ǉ�7�j
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }



}
