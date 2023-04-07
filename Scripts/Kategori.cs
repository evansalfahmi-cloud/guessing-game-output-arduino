using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class Kategori : MonoBehaviour {

    public GameObject katPrefab;
    public Soal soal;
    public MenuManagerScripts menu;

    // Use this for initialization
    void Start ()
    {
        string filePath = Application.streamingAssetsPath;
        DirectoryInfo dir = new DirectoryInfo(filePath);
        FileInfo[] files = dir.GetFiles("*.json");
        foreach(FileInfo file in files )
        {
            GameObject kat = Instantiate(katPrefab) as GameObject;
            kat.name = Path.GetFileNameWithoutExtension(file.Name).ToString();
            kat.transform.SetParent(GameObject.Find("Kategori/Panel/KategoriC").GetComponent<Transform>());
            kat.GetComponentInChildren<Text>().text = kat.name;
            string katName = kat.name;
            kat.GetComponent<Button>().onClick.AddListener(()=>OnClick(katName));
            kat.GetComponent<Button>().onClick.AddListener(() => menu.ShowMenu(GameObject.Find("Canvas/Soal").GetComponent<MenuScripts>()));
        }

       
	}

    public void OnClick (string kategori)
    {
        soal.SoalBegin(kategori);
        Debug.Log(kategori);
    }
	
}
