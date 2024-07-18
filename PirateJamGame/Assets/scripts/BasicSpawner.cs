using UnityEngine;
using UnityEngine.SceneManagement;
using Fusion;
using System.Collections.Generic;
using Fusion.Sockets;

public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    public GameObject playerPrefab;

    private NetworkRunner _runner;

    // private void Start()
    // {
    //     StartGame(GameModeHandler.GameMode);
    // }
    private void Start()
    {
        StartGame(GameModeHandler.GameMode, GameModeHandler.RoomName, GameModeHandler.PlayerName);
    }
    // public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    // {
    //     if (runner.IsServer)
    //     {
    //         // Instantiate player prefab for the new player
    //         Vector3 spawnPosition = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));
    //         NetworkObject networkPlayerObject = runner.Spawn(playerPrefab, spawnPosition, Quaternion.identity, player);
    //     }
    // }
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));
            NetworkObject networkPlayerObject = runner.Spawn(playerPrefab, spawnPosition, Quaternion.identity, player);

            // Set the player name for the newly spawned player
            var playerNameDisplay = networkPlayerObject.GetComponentInChildren<PlayerNameDisplay>();
            if (playerNameDisplay != null)
            {
                playerNameDisplay.SetPlayerName(GameModeHandler.PlayerName);
            }
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        // Find the player's NetworkObject and despawn it
        NetworkObject playerObject = runner.GetPlayerObject(player);
        if (playerObject != null)
        {
            runner.Despawn(playerObject);
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        NetworkInputData data = new NetworkInputData();

        if (Input.GetKey(KeyCode.W)) data.buttons |= NetworkInputData.BUTTON_FORWARD;
        if (Input.GetKey(KeyCode.S)) data.buttons |= NetworkInputData.BUTTON_BACKWARD;
        if (Input.GetKey(KeyCode.A)) data.buttons |= NetworkInputData.BUTTON_LEFT;
        if (Input.GetKey(KeyCode.D)) data.buttons |= NetworkInputData.BUTTON_RIGHT;

        input.Set(data);
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        throw new System.NotImplementedException();
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        throw new System.NotImplementedException();
    }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, System.ArraySegment<byte> data)
    {
        throw new System.NotImplementedException();
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        throw new System.NotImplementedException();
    }

    // async void StartGame(GameMode mode)
    // {
    //     // Create the Fusion runner and let it know that we will be providing user input
    //     _runner = gameObject.AddComponent<NetworkRunner>();
    //     _runner.ProvideInput = true;

    //     // Create the NetworkSceneInfo from the current scene
    //     var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
    //     var sceneInfo = new NetworkSceneInfo();
    //     if (scene.IsValid)
    //     {
    //         sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
    //     }

    //     // Start or join (depends on gamemode) a session with a specific name
    //     await _runner.StartGame(new StartGameArgs()
    //     {
    //         GameMode = mode,
    //         SessionName = "TestRoom",
    //         Scene = scene,
    //         SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
    //     });
    // }
    async void StartGame(GameMode mode, string roomName, string playerName)
    {
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;

        var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        var sceneInfo = new NetworkSceneInfo();
        if (scene.IsValid)
        {
            sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
        }

        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = roomName,
            Scene = scene,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    // private void OnGUI()
    // {
    //     if (_runner == null)
    //     {
    //         if (GUI.Button(new Rect(0, 0, 200, 40), "Host"))
    //         {
    //             StartGame(GameMode.Host);
    //         }
    //         if (GUI.Button(new Rect(0, 40, 200, 40), "Join"))
    //         {
    //             StartGame(GameMode.Client);
    //         }
    //     }
    // }
}

// Define a class for the player input data
public struct NetworkInputData : INetworkInput
{
    public const byte BUTTON_FORWARD = 0x01;
    public const byte BUTTON_BACKWARD = 0x02;
    public const byte BUTTON_LEFT = 0x04;
    public const byte BUTTON_RIGHT = 0x08;

    public byte buttons;
}
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using Fusion;
// using Fusion.Sockets;
// using System;

// public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks
// {
//     private NetworkRunner _runner;

//     // public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
//     // public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
//     [SerializeField] private NetworkPrefabRef _playerPrefab;
//     private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

//     public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
//     {
//         if (runner.IsServer)
//         {
//             // Create a unique position for the player
//             Vector3 spawnPosition = new Vector3((player.RawEncoded % runner.Config.Simulation.PlayerCount) * 3, 1, 0);
//             NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
//             // Keep track of the player avatars for easy access
//             _spawnedCharacters.Add(player, networkPlayerObject);
//         }
//     }

//     public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
//     {
//         if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
//         {
//             runner.Despawn(networkObject);
//             _spawnedCharacters.Remove(player);
//         }
//     }
//     public void OnInput(NetworkRunner runner, NetworkInput input)
//     {
//         var data = new NetworkInputData();

//         if (Input.GetKey(KeyCode.W))
//             data.direction += Vector3.forward;

//         if (Input.GetKey(KeyCode.S))
//             data.direction += Vector3.back;

//         if (Input.GetKey(KeyCode.A))
//             data.direction += Vector3.left;

//         if (Input.GetKey(KeyCode.D))
//             data.direction += Vector3.right;

//         input.Set(data);
//     }
//     public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
//     public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
//     public void OnConnectedToServer(NetworkRunner runner) { }
//     // public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
//     public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
//     {
//         throw new NotImplementedException();
//     }
//     public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
//     // public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
//     public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
//     {
//         throw new NotImplementedException();
//     }
//     public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
//     public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
//     public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
//     public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
//     public void OnSceneLoadDone(NetworkRunner runner) { }
//     public void OnSceneLoadStart(NetworkRunner runner) { }
//     public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
//     public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
//     // public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
//     // public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
//     public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
//     {
//         throw new NotImplementedException();
//     }

//     public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
//     {
//         throw new NotImplementedException();
//     }

//     // private NetworkRunner _runner;

//     async void StartGame(GameMode mode)
//     {
//         // Create the Fusion runner and let it know that we will be providing user input
//         _runner = gameObject.AddComponent<NetworkRunner>();
//         _runner.ProvideInput = true;

//         // Create the NetworkSceneInfo from the current scene
//         var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
//         var sceneInfo = new NetworkSceneInfo();
//         if (scene.IsValid)
//         {
//             sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
//         }

//         // Start or join (depends on gamemode) a session with a specific name
//         await _runner.StartGame(new StartGameArgs()
//         {
//             GameMode = mode,
//             SessionName = "TestRoom",
//             Scene = scene,
//             SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
//         });
//     }

//     private void OnGUI()
//     {
//         if (_runner == null)
//         {
//             if (GUI.Button(new Rect(0, 0, 200, 40), "Host"))
//             {
//                 StartGame(GameMode.Host);
//             }
//             if (GUI.Button(new Rect(0, 40, 200, 40), "Join"))
//             {
//                 StartGame(GameMode.Client);
//             }
//         }
//     }
// }