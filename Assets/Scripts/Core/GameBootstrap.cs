using System.Collections.Generic;
using UnityEngine;
using CityPresident.Rendering;

namespace CityPresident.Core
{
    public sealed class GameBootstrap : MonoBehaviour
    {
        private SimulationClock clock;
        private EconomySimulation economy;
        private readonly List<string> cityNames = new List<string>
        {
            "Novaris", "Montclair", "Valdor", "Port-Royal", "Belrive"
        };

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void CreateGame()
        {
            if (FindFirstObjectByType<GameBootstrap>() != null) return;
            new GameObject("CITY PRESIDENT - Game").AddComponent<GameBootstrap>();
        }

        private void Awake()
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
            BuildWorld();
            clock = gameObject.AddComponent<SimulationClock>();
            economy = gameObject.AddComponent<EconomySimulation>();
            clock.DayPassed += OnDayPassed;
        }

        private void BuildWorld()
        {
            Camera camera = Camera.main;
            if (camera == null)
            {
                GameObject cameraObject = new GameObject("Main Camera");
                cameraObject.tag = "MainCamera";
                camera = cameraObject.AddComponent<Camera>();
            }

            camera.transform.position = new Vector3(0f, 24f, -23f);
            camera.transform.rotation = Quaternion.Euler(48f, 0f, 0f);
            camera.fieldOfView = 48f;
            camera.backgroundColor = new Color(0.55f, 0.68f, 0.76f);
            camera.gameObject.AddComponent<MapCameraController>();

            GameObject lightObject = new GameObject("Sun");
            Light sun = lightObject.AddComponent<Light>();
            sun.type = LightType.Directional;
            sun.intensity = 1.2f;
            lightObject.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

            CreatePrimitive(PrimitiveType.Plane, "Country terrain", new Vector3(0f, 0f, 0f),
                new Vector3(4f, 1f, 4f), new Color(0.34f, 0.48f, 0.34f));

            CreatePrimitive(PrimitiveType.Cube, "Main highway", new Vector3(0f, 0.04f, 0f),
                new Vector3(1.2f, 0.04f, 31f), new Color(0.18f, 0.19f, 0.2f)).transform.rotation =
                Quaternion.Euler(0f, 24f, 0f);

            Vector3[] positions =
            {
                new Vector3(0f, 0.2f, 0f), new Vector3(-7f, 0.2f, 6f),
                new Vector3(7f, 0.2f, 5f), new Vector3(-6f, 0.2f, -8f),
                new Vector3(8f, 0.2f, -7f)
            };

            for (int i = 0; i < positions.Length; i++)
            {
                Color color = i == 0 ? new Color(0.82f, 0.67f, 0.25f) : new Color(0.35f, 0.62f, 0.78f);
                GameObject marker = CreatePrimitive(PrimitiveType.Sphere, cityNames[i], positions[i],
                    Vector3.one * (i == 0 ? 1.4f : 0.9f), color);
                marker.name = cityNames[i] + " - City";
            }

            for (int i = 0; i < 18; i++)
            {
                float x = Mathf.Sin(i * 1.7f) * 13f;
                float z = Mathf.Cos(i * 1.23f) * 12f;
                float height = Random.Range(0.5f, 2.8f);
                CreatePrimitive(PrimitiveType.Cube, "Building " + i,
                    new Vector3(x, height * 0.5f, z),
                    new Vector3(Random.Range(0.5f, 1.2f), height, Random.Range(0.5f, 1.2f)),
                    new Color(0.55f, 0.58f, 0.6f));
            }
        }

        private GameObject CreatePrimitive(PrimitiveType type, string objectName, Vector3 position,
            Vector3 scale, Color color)
        {
            GameObject item = GameObject.CreatePrimitive(type);
            item.name = objectName;
            item.transform.position = position;
            item.transform.localScale = scale;
            Renderer renderer = item.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = new Material(Shader.Find("Universal Render Pipeline/Lit") ??
                                                 Shader.Find("Standard"));
                renderer.material.color = color;
            }
            return item;
        }

        private void OnDayPassed()
        {
            if (economy != null) economy.AdvanceDay();
        }

        private void OnGUI()
        {
            GUIStyle title = new GUIStyle(GUI.skin.label) { fontSize = 22, fontStyle = FontStyle.Bold };
            GUIStyle line = new GUIStyle(GUI.skin.label) { fontSize = 14 };
            GUI.Box(new Rect(12, 12, 310, 188), GUIContent.none);
            GUI.Label(new Rect(26, 20, 280, 32), "CITY PRESIDENT", title);
            GUI.Label(new Rect(26, 55, 280, 22), "République de Novaria", line);
            GUI.Label(new Rect(26, 79, 280, 22), $"Budget : {economy.NationalBudget / 1000000000d:0.0} Md €", line);
            GUI.Label(new Rect(26, 101, 280, 22), $"Dette : {economy.PublicDebt / 1000000000d:0.0} Md €", line);
            GUI.Label(new Rect(26, 123, 280, 22), $"Chômage : {economy.UnemploymentRate:0.0} %", line);
            GUI.Label(new Rect(26, 145, 280, 22), $"Inflation : {economy.InflationRate:0.0} %", line);

            float buttonY = Screen.height - 58;
            GUI.Box(new Rect(12, buttonY - 8, 280, 50), GUIContent.none);
            if (GUI.Button(new Rect(20, buttonY, 62, 32), "Pause")) clock.SetSpeed(GameSpeed.Paused);
            if (GUI.Button(new Rect(86, buttonY, 62, 32), "x1")) clock.SetSpeed(GameSpeed.Normal);
            if (GUI.Button(new Rect(152, buttonY, 62, 32), "x5")) clock.SetSpeed(GameSpeed.Fast);
            if (GUI.Button(new Rect(218, buttonY, 62, 32), "x10")) clock.SetSpeed(GameSpeed.UltraFast);
        }

        private void OnDestroy()
        {
            if (clock != null) clock.DayPassed -= OnDayPassed;
        }
    }
}
