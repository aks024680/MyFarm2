using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoFishing : MonoBehaviour
{
    // ���㹦��
    public bool OpenFishing = false;
    // �����ı�����ʾ
    private bool showDialog = false;
    // �ж��Ƿ��ڵ�����
    private bool isFishing = false;
    // �ж��ϴε����Ƿ����
    private bool beforeFish = false;
    // �ȴ�����ʱ��
    private float fishTime = 0;
    //���ڿ����Ƿ���Կ�������
    public bool canFish = false;
    // �ж��Ƿ����������ʱ�����ı���
    public bool notFish = false;
    // ���Ʒ�ֹһ���԰Ѳ�������������ı������ִ��
    public bool afterNotFish = false;
    // ���Ƶ������ı�����ʾ
    public bool closeFishingAlert = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        fishTime += Time.deltaTime;
        // ���Ƶ���ͼ�����ʾ
        fishMessageControll();

        //���㹦��
        if (OpenFishing == true)
        {
            print("openFish:" + OpenFishing);
            // ���ϴε�������Ž�����һ�ε���
            if (beforeFish == false)
            {
                print("before:" + beforeFish);
                // ��鲢��������״̬
                fishing();
                fishTime = 0;
            }
            else
            {

                if (fishTime >= 3)
                {
                    // ��ȡ��Ʒ
                    GetFish();
                    fishTime = 0;
                }

            }

        }
/*        // �����ı�����ű�����
        if (notFish == true)
        {
            controllDialogJs();
        }*/

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            OpenFishing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            OpenFishing = false;
        }
    }
    public void fishMessageControll()
    {
        // ��ȡ��������
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (OpenFishing == true)
        {
            // �������ܹ����㣬�����������رյ�����ʾ
            if (isFishing == false)
            {
                Player.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            }
            else {
                Player.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
        }
        else
        {
            Player.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
    }

    // ���E����
    public void fishing() {
        // ��Ҫ�ر��ı�����ܹ���ʼ����
        GameObject dialogObject = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (dialogObject.transform.GetChild(0).GetChild(0).gameObject.activeSelf == true) {
            return;
        }
        if (canFish == false) {
            canFish = true;
            return;
        }
        // �ж��Ƿ���E
        if (Input.GetKeyDown(KeyCode.E)) {
            //�ж��Ƿ�װ����ͣ������
            // ��ȡuiϵͳ
            GameObject UiSystem = GameObject.FindGameObjectWithTag("mainUI");
            // �ж��Ƿ�Ϊ��
            if (UiSystem.transform.GetChild(0).GetChild(0).GetChild(19).gameObject.activeSelf == false)
            {
                //**---------------------------------------------------------------------------------------------*/
                // �����ı�����ʾδװ�������
                // ��ȡʵ�����������
                GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
                DialogMessage(dialogGameObject);
                // ����ı�
                dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message =new string[] { "��δװ�������" };
                //**---------------------------------------------------------------------------------------------*/
                notFish = true;
            }
            else {
                // װ������Ʒ�Ƿ�Ϊ�����
                List<itemsCreations> fishRodList = new List<itemsCreations>();
                // ��ȡ�������
                itemsCreations fishRod1 = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/fishRod1");
                itemsCreations fishRod2 = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/fishRod2");
                itemsCreations fishRod3 = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/fishRod3");
                fishRodList.Add(fishRod1);
                fishRodList.Add(fishRod2);
                fishRodList.Add(fishRod3);
                // �Ƚ�װ������ƷͼƬ�͵���͵���ƷͼƬ�����Ƿ�һ��
                Image equipImage = UiSystem.transform.GetChild(0).GetChild(0).GetChild(19).gameObject.GetComponent<Image>();
                int sameEquipImageCount = 0;
                for (int i = 0; i < fishRodList.Count; i++) {
                    print(equipImage.sprite);
                    print(fishRodList[i].name);
                    if (equipImage.sprite == fishRodList[i].itemImage) {
                        sameEquipImageCount++;
                    }
                }
                if (sameEquipImageCount == 0)
                {
                    // �����ı�����ʾδװ�������
                    // ��ȡʵ�����������
                    GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
                    DialogMessage(dialogGameObject);
                    // ����ı�
                    dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "��װ������Ʒ���ǵ����" };
                    notFish = true;
                }
                else {
                    // �Ƿ������
                    bagCreations playerBag = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
                    // ������ұ���
                    if (playerBag.bag.Count == 0)
                    {
                        // ��ȡʵ�����������
                        GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
                        DialogMessage(dialogGameObject);
                        // ����ı�
                        dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "��û�����" };
                        // 
                        notFish = true;
                    }
                    else {
                        int baitCount = 0;
                        for (int i = 0; i < playerBag.bag.Count; i++) {
                            if (playerBag.bag[i].itemName.Contains("���")) {
                                baitCount++;
                            }
                        }
                        if (baitCount == 0)
                        {
                            // ��ȡʵ�����������
                            GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
                            DialogMessage(dialogGameObject);
                            // ����ı�
                            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "��û�����" };
                            notFish = true;
                        }
                        else {
                            // ��ʽ���뵽����״̬------------------------
                            // ���������ʾ
                            // ��ȡ��������
                            GameObject Player = GameObject.FindGameObjectWithTag("Player");
                            // �رյ�����ʾ
                            isFishing = true;
                            Player.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                            // �ر���ҽű�
                            Player.GetComponent<PlayerController>().enabled = false;
                            // �����������
                            beforeFish = true;

                        }
                    }
                }
            }
        }
    }
    // ��ȡ��Ʒ
    public void GetFish()
    {   // ��ȡ��������
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        // ��ȡ��ұ���
        bagCreations playerBag = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
        // ��ȡ��Ҫ��������Ʒ���뵽����Ļ�ȡ��
        List<itemsCreations> addFishList = new List<itemsCreations>();
        // ��ȡ���е���Ʒ
        itemsCreations[] allItemList = Resources.LoadAll<itemsCreations>("Prefabs/bag/itemData/itemsCreation");
        for (int i = 0; i < allItemList.Length; i++)
        {
            if (allItemList[i].isAddFishing == true)
            {
                addFishList.Add(allItemList[i]);
            }
        }
        // ��д�����
        System.Random random = new System.Random();
        int randomIndex = random.Next(0, addFishList.Count - 1);
        //���������Ʒ�ķ���
        getItem addItem = new getItem();
        int isFull = addItem.AddNewFishItem(addFishList[randomIndex], playerBag);
        // ��ʾ�ı�
        // �����Ʒ����
        if (isFull == 1)
        {
            // ��ȡʵ�����������
            GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
            DialogMessage(dialogGameObject);
            // ����ı�
            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "������" + addFishList[randomIndex].itemName + "�ۼ���������" };
        }
        else
        {
            // ��ȡʵ�����������
            GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
            DialogMessage(dialogGameObject);
            // ����ı�
            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message =new string[] { "�������:" + addFishList[randomIndex].itemName };
            // �ڵ���ɹ���ȡ��Ʒ��ʱ��Ž������Ӿ���
            // �������ﾭ��
            PlayerController playerController = Player.GetComponent<PlayerController>();
            playerController.currentPlayerExp += addFishList[randomIndex].Exp;
            // ���ӵ��㾭��
            playerController.currentFishExp += addFishList[randomIndex].Exp;

        }
        // �������
        GridPrefab gridPrefab = new GridPrefab();
        int primaryBaitIndex = -1;
        int middleBaitIndex = -1;
        int highBaitIndex = -1;
        for (int i = 0;i<playerBag.bag.Count;i++) {
            if (playerBag.bag[i].itemName == "�������") {
                primaryBaitIndex = i;
            } else if (playerBag.bag[i].itemName == "�м����") {
                middleBaitIndex = i;
            } else if (playerBag.bag[i].itemName == "�߼����") {
                highBaitIndex = i;
            }
        }
        if (primaryBaitIndex != -1) {
            gridPrefab.ItemDiscardRedurce(playerBag,playerBag.bag[primaryBaitIndex]);
        } else if (middleBaitIndex != -1) {
            gridPrefab.ItemDiscardRedurce(playerBag, playerBag.bag[middleBaitIndex]);
        } else if (highBaitIndex != -1) {
            gridPrefab.ItemDiscardRedurce(playerBag, playerBag.bag[highBaitIndex]);
        }


        //������ҽű�
        Player.GetComponent<PlayerController>().enabled = true;
        // ����������ʾ
        isFishing = false;
        // �رյ��㶯��
        Player.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        // �رյ������
        beforeFish = false;
        // ���ƿ��Կ�������ر�
        canFish = false;
        // ��ʽ���뵽����״̬------------------------
    }

    // �ı���Ϣ��ʾ
    public void DialogMessage(GameObject dialogGameObject) {
        // ��ȡԤ����
        GameObject dialog = Resources.Load<GameObject>("Dialog/DialogPrefab");
        // �ж��Ƿ���ʵ�����ı���
        if (!dialogGameObject)
        {
            Instantiate(dialog);
        }
        // Ϊ��ֹ��ʼ��ʱ���ȡ����ʵ���������壬���¸�ֵ
        dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
        dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }

    // �����ı���ű���ʧ��
    public void controllDialogJs()
    {
        // ��ȡʵ�����������
        GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (dialogGameObject)
        {
            if (showDialog == true)
            {
                // ��������·���ʱ�򣬱����Ѿ�����ʾ���صĹ���,����Ҫ�ı����Դ������ı���Ĺ���
                dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().enabled = false;
            }
            else
            {
                dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().enabled = true;
            }
        }
        // ��ֹһ��ֱ��ִ�е�����Ĵ��룬����һ��
        if (afterNotFish == false)
        {
            afterNotFish = true;
            return;
        }
        if (dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.E))
        {
            dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            notFish = false;
            afterNotFish = false;
        }
    }
}
