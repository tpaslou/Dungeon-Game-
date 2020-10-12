﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  //What level the game is currently in
  //Methods to load and unload game levels
  //keep track of the game state
  //generate other persistent systems

  private string _currentLevelName = string.Empty;

  private List<AsyncOperation> _loadOperations;
  private void Start()
  {
      DontDestroyOnLoad(gameObject);
      _loadOperations=new List<AsyncOperation>();
      LoadLevel("Main");
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
}
