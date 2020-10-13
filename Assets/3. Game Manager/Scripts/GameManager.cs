using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
  //What level the game is currently in
  //Methods to load and unload game levels
  //keep track of the game state
  //generate other persistent systems
  public GameObject[] SystemPrefabs;

  
  private string _currentLevelName = string.Empty;
  
  private List<GameObject> _instancedSystemPrefabs;
  private List<AsyncOperation> _loadOperations;


  private void Start()
  {
      
      DontDestroyOnLoad(gameObject);
      _loadOperations=new List<AsyncOperation>();
      
      InstantiateSystemPrefabs();
      
      LoadLevel("Main");
      
  }

  void InstantiateSystemPrefabs()
  {
      GameObject prefabInstance;
      for (int i = 0; i < SystemPrefabs.Length; i++)
      {
          prefabInstance = Instantiate(SystemPrefabs[i]);
          _instancedSystemPrefabs.Add(prefabInstance);
      }
  }

  void OnLoadOperationComplete(AsyncOperation ao)
  {
      Debug.Log("Load Complete.");
  } 
  void OnUnloadOperationComplete(AsyncOperation ao)
  {
      if (_loadOperations.Contains(ao))
      {
          _loadOperations.Remove(ao);
          //dispatch message
          //tranasition between scenes
      }
      Debug.Log("Unload Complete.");
      
  }
  public void LoadLevel(string levelName)
  {
      AsyncOperation ao=SceneManager.LoadSceneAsync(levelName,LoadSceneMode.Additive);
      if (ao == null)
      {
          Debug.LogError("[GameManager]Unable to load level"+levelName);
          return;
      }
      ao.completed += OnLoadOperationComplete;
      _loadOperations.Add(ao);
      _currentLevelName = levelName;
  }

  public void UnloadLevel(string levelName)
  {
      AsyncOperation ao=SceneManager.LoadSceneAsync(levelName);
      if (ao == null)
      {
          Debug.LogError("[GameManager]Unable to unload level"+levelName);
          return;
      }
      ao.completed += OnUnloadOperationComplete;
  }

  protected override void OnDestroy()
  {
      base.OnDestroy();
      for (int i = 0; i < _instancedSystemPrefabs.Count; i++)
      {
          //Cleanup
          Destroy(_instancedSystemPrefabs[i]);
      }
      //clean references on garbage collector
      _instancedSystemPrefabs.Clear();
  }

}
