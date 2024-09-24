using RandomizedWitchNobeta.Archipelago;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RandomizedWitchNobeta.Behaviours
{
    public class ApHandler : MonoBehaviour
    {

        public const string ModDisplayInfo = $"{MyPluginInfo.PLUGIN_NAME} v{MyPluginInfo.PLUGIN_VERSION}";
        private const string APDisplayInfo = $"Archipelago v{ArchipelagoClient.APVersion}";
        public static ArchipelagoClient ArchipelagoClient;

        private static float guiScale = 1f;
        private static int stringCursorPosition = 0;
        private static bool showPort = false;
        private static bool showPassword = false;
        private static string stringToEdit = "";
        private static Dictionary<string, bool> editingFlags = new Dictionary<string, bool>() {
            {"Player", false},
            {"Hostname", false},
            {"Port", false},
            {"Password", false},
        };

        private void Awake()
        {
            // Plugin startup logic
            ArchipelagoClient = new ArchipelagoClient();
            ArchipelagoConsole.Awake();

            ArchipelagoConsole.LogMessage($"{ModDisplayInfo} loaded!");
        }

        private void OnGUI()
        {
            var apWindowRect = new Rect();

            ArchipelagoConsole.OnGUI();

            if (Screen.width <= 1280 && Screen.height <= 800)
            {
                guiScale = 0.75f;
            }
            else
            {
                guiScale = 1f;
            }

            apWindowRect = new Rect(20f, Screen.height * 0.12f, 430f * guiScale, 540f * guiScale);
            GUI.Window(101, apWindowRect, new Action<int>(ArchipelagoConfigEditorWindow), "Archipelago Connection");
        }

        private static void ArchipelagoConfigEditorWindow(int windowID)
        {
            GUI.skin.label.fontSize = (int)(20f * guiScale);
            GUI.skin.button.fontSize = (int)(17f * guiScale);

            string statusMessage;
            // show the Archipelago Version and whether we're connected or not
            if (ArchipelagoClient.Authenticated)
            {
                statusMessage = " Status: Connected";
                GUI.Label(new Rect(10f * guiScale, 20f * guiScale, 400f * guiScale, 30f * guiScale), APDisplayInfo + statusMessage);
            }
            else
            {
                statusMessage = " Status: Disconnected";
                GUI.Label(new Rect(10f * guiScale, 20f * guiScale, 400f * guiScale, 30f * guiScale), APDisplayInfo + statusMessage);

                /*GUI.TextField(new Rect(150, 70, 150, 20),
                    ArchipelagoClient.ServerData.Uri);
                ArchipelagoClient.ServerData.SlotName = GUI.TextField(new Rect(150, 90, 150, 20),
                    ArchipelagoClient.ServerData.SlotName, 16);
                ArchipelagoClient.ServerData.Password = GUI.TextField(new Rect(150, 110, 150, 20),
                    ArchipelagoClient.ServerData.Password);*/

                // requires that the player at least puts *something* in the slot name
                /*if (GUI.Button(new Rect(16, 130, 100, 20), "Connect") &&
                    !ArchipelagoClient.ServerData.SlotName.IsNullOrWhiteSpace())
                {
                    ArchipelagoClient.Connect();
                }*/
            }

            //Player name
            GUI.Label(new Rect(10f * guiScale, 60f * guiScale, 300f * guiScale, 30f * guiScale), $"Player: {TextWithCursor(GetConnectionSetting("Player"), editingFlags["Player"], true)}");

            bool EditPlayer = GUI.Button(new Rect(10f * guiScale, 100f * guiScale, 75f * guiScale, 30f * guiScale), editingFlags["Player"] ? "Save" : "Edit");
            if (EditPlayer) HandleEditButton("Player");

            bool PastePlayer = GUI.Button(new Rect(100f * guiScale, 100f * guiScale, 75f * guiScale, 30f * guiScale), "Paste");
            if (PastePlayer) HandlePasteButton("Player");

            bool ClearPlayer = GUI.Button(new Rect(190f * guiScale, 100f * guiScale, 75f * guiScale, 30f * guiScale), "Clear");
            if (ClearPlayer) HandleClearButton("Player");

            //Hostname
            GUI.Label(new Rect(10f * guiScale, 160f * guiScale, 300f * guiScale, 30f * guiScale), $"Host: {TextWithCursor(GetConnectionSetting("Hostname"), editingFlags["Hostname"], true)}");

            bool setLocalhost = GUI.Toggle(new Rect(160f * guiScale, 200f * guiScale, 90f * guiScale, 30f * guiScale), ArchipelagoClient.ServerData.Hostname == "localhost", "localhost");
            if (setLocalhost && ArchipelagoClient.ServerData.Hostname != "localhost")
            {
                SetConnectionSetting("Hostname", "localhost");
            }

            bool setArchipelagoHost = GUI.Toggle(new Rect(10f * guiScale, 200f * guiScale, 140f * guiScale, 30f * guiScale), ArchipelagoClient.ServerData.Hostname == "archipelago.gg", "archipelago.gg");
            if (setArchipelagoHost && ArchipelagoClient.ServerData.Hostname != "archipelago.gg")
            {
                SetConnectionSetting("Hostname", "archipelago.gg");
            }

            bool EditHostname = GUI.Button(new Rect(10f * guiScale, 240f * guiScale, 75f * guiScale, 30f * guiScale), editingFlags["Hostname"] ? "Save" : "Edit");
            if (EditHostname) HandleEditButton("Hostname");

            bool PasteHostname = GUI.Button(new Rect(100f * guiScale, 240f * guiScale, 75f * guiScale, 30f * guiScale), "Paste");
            if (PasteHostname) HandlePasteButton("Hostname");

            bool ClearHostname = GUI.Button(new Rect(190f * guiScale, 240f * guiScale, 75f * guiScale, 30f * guiScale), "Clear");
            if (ClearHostname) HandleClearButton("Hostname");

            //Port
            GUI.Label(new Rect(10f * guiScale, 300f * guiScale, 300f * guiScale, 30f * guiScale), $"Port: {TextWithCursor(GetConnectionSetting("Port"), editingFlags["Port"], showPort)}");

            showPort = GUI.Toggle(new Rect(270f * guiScale, 305f * guiScale, 75f * guiScale, 30f * guiScale), showPort, "Show");

            bool EditPort = GUI.Button(new Rect(10f * guiScale, 340f * guiScale, 75f * guiScale, 30f * guiScale), editingFlags["Port"] ? "Save" : "Edit");
            if (EditPort) HandleEditButton("Port");

            bool PastePort = GUI.Button(new Rect(100f * guiScale, 340f * guiScale, 75f * guiScale, 30f * guiScale), "Paste");
            if (PastePort) HandlePasteButton("Port");

            bool ClearPort = GUI.Button(new Rect(190f * guiScale, 340f * guiScale, 75f * guiScale, 30f * guiScale), "Clear");
            if (ClearPort) HandleClearButton("Port");

            //Password
            GUI.Label(new Rect(10f * guiScale, 400f * guiScale, 300f * guiScale, 30f * guiScale), $"Password: {TextWithCursor(GetConnectionSetting("Password"), editingFlags["Password"], showPassword)}");

            showPassword = GUI.Toggle(new Rect(270f * guiScale, 405f * guiScale, 75f * guiScale, 30f * guiScale), showPassword, "Show");
            bool EditPassword = GUI.Button(new Rect(10f * guiScale, 440f * guiScale, 75f * guiScale, 30f * guiScale), editingFlags["Password"] ? "Save" : "Edit");
            if (EditPassword) HandleEditButton("Password");

            bool PastePassword = GUI.Button(new Rect(100f * guiScale, 440f * guiScale, 75f * guiScale, 30f * guiScale), "Paste");
            if (PastePassword) HandlePasteButton("Password");

            bool ClearPassword = GUI.Button(new Rect(190f * guiScale, 440f * guiScale, 75f * guiScale, 30f * guiScale), "Clear");
            if (ClearPassword) HandleClearButton("Password");
        }

        //Place a visible cursor in a text label when editing the field
        private static string TextWithCursor(string text, bool isEditing, bool showText)
        {
            string baseText = showText ? text : new string('*', text.Length);
            if (!isEditing) return baseText;
            if (stringCursorPosition > baseText.Length) stringCursorPosition = baseText.Length;
            return baseText.Insert(stringCursorPosition, "<color=#EAA614>|</color>");
        }

        //Get a connection setting value by fieldname
        private static string GetConnectionSetting(string fieldName)
        {
            return fieldName switch
            {
                "Player" => ArchipelagoClient.ServerData.SlotName,
                "Hostname" => ArchipelagoClient.ServerData.Hostname,
                "Port" => ArchipelagoClient.ServerData.Port,
                "Password" => ArchipelagoClient.ServerData.Password,
                _ => "",
            };
        }

        //Set a connection setting value by fieldname
        private static void SetConnectionSetting(string fieldName, string value)
        {
            switch (fieldName)
            {
                case "Player":
                    ArchipelagoClient.ServerData.SlotName = value;
                    return;
                case "Hostname":
                    ArchipelagoClient.ServerData.Hostname = value;
                    return;
                case "Port":
                    ArchipelagoClient.ServerData.Port = value;
                    return;
                case "Password":
                    ArchipelagoClient.ServerData.Password = value;
                    return;
                default:
                    return;
            }
        }

        //Clear all field editing flags (since we do this in a few places)
        private static void ClearAllEditingFlags()
        {

            List<string> fieldKeys = new List<string>(editingFlags.Keys);
            foreach (string fieldKey in fieldKeys)
            {
                editingFlags[fieldKey] = false;
            }
        }

        //Initialize a text field for editing
        private static void BeginEditingTextField(string fieldName)
        {
            if (editingFlags[fieldName]) return; //can't begin if we're already editing this field

            //check and finalize if another field was mid-edit
            List<string> fieldKeys = new List<string>(editingFlags.Keys);
            foreach (string fieldKey in fieldKeys)
            {
                if (editingFlags[fieldKey]) FinishEditingTextField(fieldKey);
            }

            stringToEdit = GetConnectionSetting(fieldName);
            stringCursorPosition = stringToEdit.Length;
            //GUIInput.instance.actionSet.Enabled = false; //prevent keypresses from interacting with the menu while editing
            editingFlags[fieldName] = true;
        }

        //finalize editing a text field and save the changes
        private static void FinishEditingTextField(string fieldName)
        {
            if (!editingFlags[fieldName]) return; //can't finish if we're not editing this field

            stringToEdit = "";
            stringCursorPosition = 0;
            //GUIInput.instance.actionSet.Enabled = true;
            editingFlags[fieldName] = false;
        }

        private static void HandleEditButton(string fieldName)
        {
            if (editingFlags[fieldName])
            {
                FinishEditingTextField(fieldName);
            }
            else
            {
                BeginEditingTextField(fieldName);
            }
        }

        private static void HandlePasteButton(string fieldName)
        {

            SetConnectionSetting(fieldName, GUIUtility.systemCopyBuffer);
            if (editingFlags[fieldName])
            {
                stringToEdit = GUIUtility.systemCopyBuffer;
                FinishEditingTextField(fieldName);
            }
        }

        private static void HandleClearButton(string fieldName)
        {
            SetConnectionSetting(fieldName, "");
            if (editingFlags[fieldName]) stringToEdit = "";
        }
    }
}
