using UnityEngine.UI;
using UnityEngine;
using System;
using Bolt;
using Bolt.Matchmaking;
using UdpKit;
using System.Collections.Generic;

public class Menu : GlobalEventListener
{
    public Button joinGameButtonPrefab;
    public GameObject serverListPanel;
    public GameObject setUsernamePanel;
    private List<Button> joinServerButtons = new List<Button>();
    public float buttonSpacing;

    private void Start()
    {
        setUsernamePanel.SetActive(PlayerPrefs.GetString("username") == null);
    }

    public void OnSetUsernameValueChanged(string input)
    {
        Debug.Log(input);

        PlayerPrefs.SetString("username", input);
    }

    // Host button
    public void StartServer()
    {
        BoltLauncher.StartServer();
    }

    //Join button
    public void StartClient()
    {
        BoltLauncher.StartClient();
    }

    public override void BoltStartDone()
    {
        BoltMatchmaking.CreateSession(sessionID: "test", sceneToLoad: "Game");
    }

    //Called when a room is created or destroyed, as well as every few seconds
    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        ClearSessions();

        
        foreach (var session in sessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;

            int sesssioNumber = UnityEngine.Random.Range(0, 999);

            Button joinGameButtonClone = Instantiate(joinGameButtonPrefab);
            joinGameButtonClone.transform.SetParent(serverListPanel.transform);
            joinGameButtonClone.transform.localPosition = new Vector3(-100, -120 + buttonSpacing * joinServerButtons.Count, 0);
            joinGameButtonClone.GetComponentInChildren<Text>().text = "Game " + sesssioNumber;
            joinGameButtonClone.gameObject.SetActive(true);
            

            joinGameButtonClone.onClick.AddListener(() => JoinGame(photonSession));

            joinServerButtons.Add(joinGameButtonClone);
        }
    }

    private void JoinGame(UdpSession photonSession)
    {
        BoltMatchmaking.JoinSession(photonSession);

    }

    private void ClearSessions()
    {
        foreach (Button button in joinServerButtons)
        {
            Destroy(button.gameObject);
        }
        joinServerButtons.Clear();
    }
}
