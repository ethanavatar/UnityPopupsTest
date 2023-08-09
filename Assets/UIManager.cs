using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject StartupMenu;
    public GameObject RandomMenu;

    public int RandomIntervalMax = 10;
    public int RandomIntervalMin = 2;
    private float RandomTimer = 0.0f;
    void Awake() {
        RandomTimer = (float) GetRandom(RandomIntervalMin, RandomIntervalMax);
    }

    // Start is called before the first frame update
    void Start() {
        var instance = Instantiate(StartupMenu, Vector3.zero, Quaternion.identity);
        instance.transform.SetParent(this.transform, false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    // LateUpdate is called once per frame after Update
    void LateUpdate() {

        if (RandomTimer <= 0.0f) {
            SpawnUI();

            RandomTimer = (float) GetRandom(RandomIntervalMin, RandomIntervalMax);
        }

        RandomTimer -= Time.deltaTime;
    }

    void SpawnUI() {
        if (GameObject.Find("Popup(Clone)") != null) {
            return;
        }

        var instance = Instantiate(RandomMenu, Vector3.zero, Quaternion.identity);
        instance.transform.SetParent(this.transform, false);

        var rectTransform = instance.transform
            .GetChild(0).transform
            .GetChild(1)
            .GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new Vector2(GetRandom(-200, 200), GetRandom(-100, 100));
    }

    private static readonly System.Random random = new System.Random();
    int GetRandom(int min, int max) {
        int result = 0;
        lock (random) {
            result = random.Next(min, max);
        }
        return result;
    }
}
