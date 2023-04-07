using UnityEngine;
using System.Collections;
using LitJson;
using UnityEngine.UI;
using System.IO.Ports;


public class Soal : MonoBehaviour
{

    public string filePath;
    public string jsonString;
    public JsonData soalData;
    public int numberSoal = 0;
    public GameObject jawabanPrefab;
    public bool nextSoal;
    public bool clickJawaban;
    public SerialPort serial = new SerialPort("COM3", 9600);

    public int score;


    public void SoalBegin(string jsonName)
    {
        score = 0;
        nextSoal = true;
        filePath = System.IO.Path.Combine(Application.streamingAssetsPath, jsonName + ".json");
        StartCoroutine("Json");
        soalData = JsonMapper.ToObject(jsonString);
        OnClick();

    }

    IEnumerator Json()
    {
        if (filePath.Contains("://"))
        {
            WWW www = new WWW(filePath);
            yield return www;
            jsonString = www.text;
        }
        else {
            jsonString = System.IO.File.ReadAllText(filePath);
        }


    }

    public void OnClick()
    {
        if (numberSoal >= soalData["data"].Count)
        {
            Debug.Log("Soal Sudah habis pindah ke Menu Result");

            if (score == soalData["data"].Count)
            {
                
                GameObject.Find("Rank").GetComponent<Text>().text = "Sangat Baik";//Bintang 3
                serial.Open();
                serial.Write("A");

            }
            else
             if (score >= soalData["data"].Count * 1 / 2)
            {
                GameObject.Find("Rank").GetComponent<Text>().text = "Baik";// Bintang 2
            }
            else
            if (score <= soalData["data"].Count * 1 / 2)
            {
                GameObject.Find("Rank").GetComponent<Text>().text = "Buruk"; //Bintang 1
                
            }



            MenuManagerScripts menuResult = GameObject.Find("Canvas").GetComponent<MenuManagerScripts>();
            menuResult.ShowMenu(GameObject.Find("Result").GetComponent<MenuScripts>());
            GameObject.Find("Score").GetComponent<Text>().text = score.ToString() + "/" + soalData["data"].Count;
        }


        if (nextSoal)
        {
            GameObject[] jawabanDestroy = GameObject.FindGameObjectsWithTag("Jawaban");
            if (jawabanDestroy != null)
            {
                for (int x = 0; x < jawabanDestroy.Length; x++)
                {
                    DestroyImmediate(jawabanDestroy[x]);
                }
            }

            GameObject.Find("Soal/Panel/SoalC/Soal").GetComponentInChildren<Text>().text = soalData["data"][numberSoal]["soal"].ToString();

            for (int i = 0; i < soalData["data"][numberSoal]["jawaban"].Count; i++)
            {

                GameObject jawaban = Instantiate(jawabanPrefab);
                jawaban.GetComponentInChildren<Text>().text = soalData["data"][numberSoal]["jawaban"][i].ToString();
                Transform jawabanC = GameObject.Find("JawabanC").GetComponent<Transform>();
                jawaban.transform.SetParent(jawabanC);

                string x = i.ToString();

                if (i == 0)
                {
                    jawaban.name = "JawabanBenar";
                    jawaban.GetComponent<Button>().onClick.AddListener(() => Jawaban("0"));
                }
                else {
                    jawaban.name = "JawabanSalah" + x;
                    jawaban.GetComponent<Button>().onClick.AddListener(() => Jawaban(x));
                }
                jawaban.transform.SetSiblingIndex(Random.Range(0, 3));
            }

            numberSoal++;
            nextSoal = false;
            clickJawaban = true;
            StartCoroutine("Timer");
        }
    }
    public void Jawaban(string x)
    {
        if (clickJawaban)
        {
            if (x == "0")
            {
                score++;
                GameObject.Find("JawabanBenar").GetComponent<Button>().image.color = Color.green;
                GameObject.Find("Image (" + numberSoal + ")").GetComponent<Image>().color = Color.green;
                Debug.Log("Jawaban Benar");
            }
            else {
                GameObject.Find("JawabanSalah" + x).GetComponent<Button>().image.color = Color.red;
                 GameObject.Find("JawabanBenar").GetComponent<Button>().image.color = Color.green;
                //GameObject.Find("Image (" + numberSoal + ")").GetComponent<Image>().color = Color.red;
                //clickJawaban = false;
                //nextSoal = true;
                GameObject.Find("Image (" + numberSoal + ")").GetComponent<Image>().color = Color.red;
                Debug.Log("Jawaban Salahh");
            }
        }
        nextSoal = true;
        clickJawaban = false;

    }
    IEnumerator Timer ()

    {
        Image time = GameObject.Find("Timer").GetComponent<Image>();
        time.fillAmount = 1;
        float timeToWait = 4.75f;
        float incrementToRemove =0.015f;
        float x = time.fillAmount / timeToWait * incrementToRemove;

        while (timeToWait>0)
        {
            yield return new WaitForSeconds(incrementToRemove);

            if (!nextSoal)

            {
                time.fillAmount -= x;
                timeToWait -= incrementToRemove;
            }
            else
            {
                timeToWait = 0;
            }
            
        }

        if (time.fillAmount<= 0.1f)
        {
            for (int i = 1; i < 4; i++)
            {
                GameObject.Find("JawabanSalah" + i).GetComponent<Button>().image.color = Color.red;

            }

            GameObject.Find("JawabanBenar").GetComponent<Button>().image.color = Color.green;
            GameObject.Find("Image (" + numberSoal + ")").GetComponent<Image>().color = Color.red;
            clickJawaban = false;
            nextSoal = true;
        }
    }



}
